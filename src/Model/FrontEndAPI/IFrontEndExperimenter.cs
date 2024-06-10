namespace Model.FrontEndAPI;
using Model.Result;

public interface IFrontEndExperimenter {
    
    void StoreResultFromQuestion(IResult answer);
    int GetNextUserId();
    bool ExportResults(int surveyWrapperId, string folderPath);
}