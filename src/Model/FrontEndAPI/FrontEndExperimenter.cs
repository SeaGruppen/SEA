namespace Model.FrontEndAPI;
using System.Text;
using Result;
using DatabaseModule;
internal class FrontEndExperimenter : IFrontEndExperimenter {

    private IDatabase databaseService;
    internal FrontEndExperimenter(IDatabase database) {
        databaseService = database;
    }
    
    public void StoreResultFromQuestion(IResult answer) {
        databaseService.StoreResult(answer);   
    }
    
    public uint GetNextUserId() {
        return databaseService.GetNextUserId();
    }
    
    public bool ExportResults(int surveyWrapperId, string folderPath) {
        List<Result> results = databaseService.GetSurveyWrapperResults(surveyWrapperId);
        string path = Path.Combine(folderPath, $"{surveyWrapperId}.csv");
        try {
            using (StreamWriter writer = new StreamWriter(path, false, Encoding.UTF8)) {
                foreach (var result in results) {
                    writer.WriteLine(result.ToString());
                }
            }
            return true;
        }
        catch (Exception) {
            return false;
        } 
    }

}