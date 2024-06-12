namespace Model.Database;
using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using FrontEndAPI;
using SurveyWrapper = Model.Survey.SurveyWrapper;
using Survey = Model.Survey.Survey;
using Result = Model.Result.Result;
using Model.Result;
using System.Collections.Generic;
using Model.Utilities;
using Model.Question;using System.IO.Compression;

internal class DatabaseServices : IDatabase {
    
    private string databasePath;
    private readonly string resultsPath;
    private readonly string creatorDictPath;
    private Random random = new Random();
    internal DatabaseServices() {
        string? projectPath = FileIO.GetProjectPath();
        if (projectPath != null)
        {
            databasePath = Path.Combine(projectPath, "..", "surveyDatabase");
        }
        else
        {
            databasePath = "surveyDatabase";
        }
        Directory.CreateDirectory(databasePath); //is only created if not exists
        resultsPath = Path.Combine(databasePath, "results.csv");
        CreateResultsFileIfNotExisting(resultsPath);
        creatorDictPath = Path.Combine(databasePath, "creatorDict.json");
        CreateCreatorDictFileIfNotExisting(creatorDictPath);
    }

    //overloading constructor for testing purposes
    internal DatabaseServices(string dataBasePath) {
        this.databasePath = dataBasePath;
        Directory.CreateDirectory(databasePath); //is only created if not exists
        resultsPath = Path.Combine(databasePath, "results.csv");
        CreateResultsFileIfNotExisting(resultsPath);
        creatorDictPath = Path.Combine(databasePath, "creatorDict.json");
        CreateCreatorDictFileIfNotExisting(creatorDictPath);
    }

    private static void CreateResultsFileIfNotExisting(string resultsPath) {
        if (!File.Exists(resultsPath)) {
            File.Create(resultsPath).Dispose();
        }
    }

    private static void CreateCreatorDictFileIfNotExisting(string creatorDictPath) {
        if (!File.Exists(creatorDictPath)) {
            File.Create(creatorDictPath).Dispose();
            var creatorDict = new Dictionary<string, List<int>>();
            string jsonString = JsonSerializer.Serialize(creatorDict, Globals.OPTIONS);
            File.WriteAllText(creatorDictPath, jsonString);
        }
    }

    public bool StoreSurveyWrapper(SurveyWrapper surveyWrapper) {
        string surveyWrapperPath = GetSurveyWrapperPath(surveyWrapper.SurveyWrapperId);
        Directory.CreateDirectory(surveyWrapperPath);
        string surveyWrapperFilePath = Path.Combine(surveyWrapperPath, surveyWrapper.SurveyWrapperId + ".json");
        SaveSurveyWrapperToFile(surveyWrapperFilePath, surveyWrapper);
        return true;
    }

    public bool DeleteSurveyWrapper(int surveyWrapperId) {
        string surveyWrapperPath = GetSurveyWrapperPath(surveyWrapperId);
        if (Directory.Exists(surveyWrapperPath)) {
            Directory.Delete(surveyWrapperPath, true);
            return true;
        } else {
            return false;
        }
    }

    public SurveyWrapper? GetSurveyWrapper(int surveyWrapperId) {
        string surveyWrapperPath = GetSurveyWrapperPath(surveyWrapperId);
        string surveyWrapperFile = Path.Combine(surveyWrapperPath, surveyWrapperId + ".json");
        if (!File.Exists(surveyWrapperFile)) {
            return null;
        } else {
            return LoadSurveyWrapperFromFile(surveyWrapperFile);
        }
    }
    
    
    private static void SaveSurveyWrapperToFile(string surveyWrapperFilePath, SurveyWrapper surveyWrapper) {
        string jsonString = JsonSerializer.Serialize(surveyWrapper, Globals.OPTIONS);
        File.WriteAllText(surveyWrapperFilePath, jsonString);
    }

    private string GetSurveyWrapperPath(int surveyWrapperId) {
        return Path.Combine(databasePath, surveyWrapperId.ToString());
    }

    private static SurveyWrapper LoadSurveyWrapperFromFile(string surveyWrapperPath) {
        string jsonString = File.ReadAllText(surveyWrapperPath);
        return JsonSerializer.Deserialize<SurveyWrapper>(jsonString, Globals.OPTIONS)!;
    }

    public string StorePictureOverwrite(int surveyWrapperId, string src) {
        string surveyAssetsPath = GetSurveyWrapperAssetsPath(surveyWrapperId); 
        string dest = Path.Combine(surveyAssetsPath, Path.GetFileName(src));
        Directory.CreateDirectory(surveyAssetsPath);
        File.Copy(src, dest, true); //true -> overwrites automatically if dest already exists
        return dest;
    }

    public string TryStorePicture(int surveyWrapperId, string src) {
        string surveyAssetsPath = GetSurveyWrapperAssetsPath(surveyWrapperId); 
        string dest = Path.Combine(surveyAssetsPath, Path.GetFileName(src));
        Directory.CreateDirectory(surveyAssetsPath);
        if (!File.Exists(dest)) {
            File.Copy(src, dest);
            return dest;
        } else {
            throw new Exception("A file with this name already exists for this surveyWrapper");
        }
    }

    private string GetSurveyWrapperAssetsPath(int surveyWrapperId) {
        return Path.Combine( GetSurveyWrapperPath(surveyWrapperId), "assets");
    }

    public int GetNextSurveyWrapperID(string superUserName) {
        int result = random.Next(100000);
        // Ensure that Id isn't used already.
        while (Directory.Exists(GetSurveyWrapperPath(result))) {
            result = random.Next();
        }
        StoreCreatorEntry(superUserName, result);
        return result;
    }

    private void StoreCreatorEntry(string superUserName, int surveyWrapperId) {
        Dictionary<string, List<int>> creatorDict = GetCreatorDict();
        if (!creatorDict.ContainsKey(superUserName)) {
            List<int> idList = new List<int>{surveyWrapperId};
            creatorDict.Add(superUserName, idList);
        } else {
            creatorDict[superUserName].Add(surveyWrapperId);
        }
        StoreCreatorDict(creatorDict);
    }

    private Dictionary<string, List<int>> GetCreatorDict() {
        string jsonString = File.ReadAllText(creatorDictPath);
        return JsonSerializer.Deserialize<Dictionary<string, List<int>>>(jsonString, Globals.OPTIONS)!;
    }

    private void StoreCreatorDict(Dictionary<string, List<int>> creatorDict) {
        string jsonString = JsonSerializer.Serialize(creatorDict, Globals.OPTIONS);
        File.WriteAllText(creatorDictPath, jsonString);
    }

    public bool ExportSurveyWrapper(int id, string path) {
        string surveyWrapperPath = GetSurveyWrapperPath(id);
        string zipFilePath = Path.Combine(path, $"{id}.zip");
        try {
            ZipFile.CreateFromDirectory(surveyWrapperPath, zipFilePath);
            return true;
        } catch (Exception) {
            // Survey doesn't exist, or zipfile already exists
            return false;
        }
    }

    public bool ImportSurveyWrapper(string filePathAndName) {
        try {
            ZipFile.ExtractToDirectory(filePathAndName, Path.Combine(databasePath, Path.GetFileNameWithoutExtension(filePathAndName)));
            return true;
        }
        catch (Exception)
        {
            // Handle exceptions if needed
            return false;
        }
    }

/// <summary>
/// Get all final results for a SurveyWrapper, this does not return intermediate results, they still exist in the database
/// </summary>
    public List<Result> GetSurveyWrapperResults(int surveyWrapperId) {
        List<Result> results = new List<Result>();
        try {
            using (StreamReader reader = new StreamReader(resultsPath, Encoding.UTF8)) {
                string line;
                while ((line = reader.ReadLine()) != null) {
                    Result result;
                    try {
                        result = Result.FromString(line);
                    } catch (Exception) {
                        // Skip line if it can't be parsed
                        continue;
                    }
                    bool newestResult = true;
                    // Check if the result is for the correct surveyWrapper, and is newest
                    if (ExtractSurveyDetails.TryGetSurveyWrapperId(result.QuestionId) == surveyWrapperId) {
                        // Go through all results and remove intermediate results
                        foreach (Result r in results) {
                            if (r.UserId == result.UserId && r.QuestionId == result.QuestionId) {
                                // If r is older than result, remove r
                                if (r.CreationTime <= result.CreationTime) {
                                    results.Remove(r);
                                    break;
                                } else {
                                    newestResult = false;
                                    break;
                                }
                            }
                        }
                        if (newestResult) {
                            results.Add(result);
                        }
                    }
                }
            }
            return results;
        }
        catch (Exception ex) {
            // Handle exceptions if needed
            System.Console.WriteLine($"Error in GetSurveyWrapperResults {ex.Message}");
            return results;
        }
    }

    public bool StoreResult (IResult result) {
        try {
            using (StreamWriter writer = new StreamWriter(resultsPath, true, Encoding.UTF8)) {
                    writer.WriteLine(result.ToString());
            }
            return true;
        }
        catch (Exception) {
            // Write failed, return false
            return false;
        }
    }

    public bool StoreResults(List<Result> results) {
        try {
            using (StreamWriter writer = new StreamWriter(resultsPath, true, Encoding.UTF8)) {
                foreach (var result in results) {
                    writer.WriteLine(result.ToString());
                }
            }
            return true;
        }
        catch (Exception) {
            // Handle exceptions if needed
            return false;
        }
    }

    public List<SurveyWrapper>? GetSurveyWrapperForSuperUser(string superUserName){
        Dictionary<string, List<int>> creatorDict = GetCreatorDict();
        if (!creatorDict.ContainsKey(superUserName)) {
            return null;
        }
        List<SurveyWrapper> surveyWrapperList = new List<SurveyWrapper>();
        foreach (int surveyWrapperId in creatorDict[superUserName]) {
            surveyWrapperList.Add(GetSurveyWrapper(surveyWrapperId));
        }
        return surveyWrapperList;
    }

    public List<int> GetAllSurveyWrapperIds() {
        List<string> directories = Directory.GetDirectories(databasePath).ToList();
        List<int> result = new List<int>();
        // Validate that all folders are just integers
        foreach (var directory in directories) {
            try {
                result.Add(int.Parse(Path.GetFileName(directory)));
            } catch (Exception) {
                System.Console.WriteLine("Error parsing directory name to int in DatabaseService.GetAllSurveyWrapperIds()");
            }
        }
        return result;
    }

    public int GetNextUserId() {
        Guid guid = Guid.NewGuid();
        int result = guid.GetHashCode();
        return result;
    }
}

