namespace Model.DatabaseModule;

using System.Collections.Generic;
using Result;
using Survey;

internal interface IDatabase {
    int GetNextSurveyWrapperID(string superUserName);
    bool StoreSurveyWrapper(SurveyWrapper surveyWrapper);
    bool DeleteSurveyWrapper(int surveyWrapperId, string username);
    SurveyWrapper? GetSurveyWrapper(int surveyWrapperId);
    List<SurveyWrapper>? GetSurveyWrapperForSuperUser(string username);
    bool ExportSurveyWrapper(int surveyWrapperid,string path);
    bool ImportSurveyWrapper(string path);
    string TryStorePicture(int surveyWrapperId, string path);
    string StorePictureOverwrite(int surveyWrapperId, string path);
    List<Result> GetSurveyWrapperResults(int id);
    bool StoreResults(List<Result> results);
    bool StoreResult(IResult result);
    List<int> GetAllSurveyWrapperIds();
    int GetNextUserId();
}
