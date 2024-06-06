namespace Model.Database;
// using FrontEndAPI;
// using Survey;
// using Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Survey = Model.Survey.Survey;
using Result = Model.Result.Result;
using Model.Result;
using Model.Survey;

internal interface IDatabase {
    int GetNextSurveyWrapperID(string superUserName);
    bool StoreSurveyWrapper(SurveyWrapper surveyWrapper);
    bool DeleteSurveyWrapper(int surveyWrapperId);
    SurveyWrapper? GetSurveyWrapper(int surveyWrapperId);
    List<SurveyWrapper> GetSurveyWrapperForSuperUser(string username);
    bool ExportSurveyWrapper(int surveyWrapperid,string path);
    bool ImportSurveyWrapper(string path);
    string TryStorePicture(int surveyWrapperId, string path);
    string StorePictureOverwrite(int surveyWrapperId, string path);
    List<Result> GetSurveyWrapperResults(int id);
    bool StoreResults(List<Result> results);
    bool StoreResult(IResult result);
    List<int> GetAllSurveyWrapperIds();
}
