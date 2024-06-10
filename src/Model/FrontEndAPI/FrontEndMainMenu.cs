namespace Model.FrontEndAPI;
using Model.Database;
using Model.Survey;


internal class FrontEndMainMenu : IFrontEndMainMenu {

    private IDatabase db;

    internal FrontEndMainMenu(IDatabase database) {
        db = database;
    }


    public IReadOnlySurveyWrapper? GetSurveyWrapper(int surveyId) {
        return db.GetSurveyWrapper(surveyId);
    }

    public bool ImportSurveyWrapper(string filePath) {
        return db.ImportSurveyWrapper(filePath);        
    }

    public List<IModifySurveyWrapper>? ValidateSuperUser(string username, string password) {
        //Validate superuser against Hashfunction first. If true, then return the list of surveys
        List<SurveyWrapper> surveyWrappers = db.GetSurveyWrapperForSuperUser(username);
        List<IModifySurveyWrapper> result = new List<IModifySurveyWrapper>(surveyWrappers.Cast<IModifySurveyWrapper>().ToList());
        return result;
    }
}  