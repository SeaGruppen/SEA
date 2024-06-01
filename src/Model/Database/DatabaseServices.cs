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
using Model.Question;
internal class DatabaseServices : IDatabase {
    
    private string databasePath;
    private readonly string resultsPath;
    internal DatabaseServices() {
        string? projectPath = FileIO.GetProjectPath();
        if (projectPath != null)
        {
            databasePath = Path.Combine(projectPath, "surveyDatabase");
        }
        else
        {
            databasePath = "surveyDatabase";
        }
        Directory.CreateDirectory(databasePath); //is only created if not exists
        resultsPath = Path.Combine(databasePath, "results.csv");
        CreateResultsFileIfNotExisting(resultsPath);
    }

    //overloading constructor for testing purposes
    internal DatabaseServices(string dataBasePath) {
        this.databasePath = dataBasePath;
        Directory.CreateDirectory(databasePath); //is only created if not exists
        resultsPath = Path.Combine(databasePath, "results.csv");
        CreateResultsFileIfNotExisting(resultsPath);
    }

    


    private static void CreateResultsFileIfNotExisting(string resultsPath) {
        if (!File.Exists(resultsPath)) {
            File.Create(resultsPath).Dispose();
        }
    }

    public bool StoreSurveyWrapper(SurveyWrapper surveyWrapper) {
        string surveyWrapperPath = GetSurveyWrapperPath(surveyWrapper.SurveyWrapperId);
        Directory.CreateDirectory(surveyWrapperPath);
        string surveyWrapperFilePath = Path.Combine(surveyWrapperPath, surveyWrapper.SurveyWrapperId + ".json");
        SaveSurveyWrapperToFile(surveyWrapperFilePath, surveyWrapper);
        return true;
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
        var options = new JsonSerializerOptions { WriteIndented = true }; //pretty printing of json strings
        options.Converters.Add(new Model.Question.MultiQuestionConverter()); //necessary to serialize the fields of MultiQuestion (since it is an IEnumerable)
        string jsonString = JsonSerializer.Serialize(surveyWrapper, options);
        Console.WriteLine(jsonString);
        File.WriteAllText(surveyWrapperFilePath, jsonString);
    }

    private string GetSurveyWrapperPath(int surveyWrapperId) {
        return Path.Combine(databasePath, surveyWrapperId.ToString());
    }

    private static SurveyWrapper LoadSurveyWrapperFromFile(string surveyWrapperPath) {
        string jsonString = File.ReadAllText(surveyWrapperPath);
        return JsonSerializer.Deserialize<SurveyWrapper>(jsonString)!;
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

    // Tmp int used to increment to get unique IDs, must be received from db.
    private int tmpId = 0;
    public int GetNextSurveyID() {
        return tmpId++;
    }

    public bool ExportSurvey(int id, string path) {
        return true;
    }

    public bool ImportSurvey(string path) {
        return false;
    }

    public List<Result> GetResults(int id) {
        throw new NotImplementedException();
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

    public List<SurveyWrapper> GetSurveyWrapperForSuperUser(string username){
        return new List<SurveyWrapper>();
    }
}

