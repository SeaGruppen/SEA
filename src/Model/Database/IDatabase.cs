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
    string GetNextSurveyWrapperID();
    bool StoreSurvey(Survey survey);
    // Survey GetSurvey(string surveyId);
    SurveyWrapper GetSurveyWrapper(string surveyId);
    List<SurveyWrapper> GetSurveyWrapperForSuperUser(string username);
    bool ExportSurvey(string id,string path);
    bool ImportSurvey(string path);
    bool TryStorePicture(string path, string surveyId);
    void StorePictureOverwrite(string path, string surveyId);
    List<Result> GetResults(string id);
    bool StoreResults(List<Result> results);
    bool StoreResult(IResult result);
}
