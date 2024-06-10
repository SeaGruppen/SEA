namespace Model.FrontEndAPI;
using Model.Database;
using Model.Survey;
using Model.UserValidation;

internal class FrontEndMainMenu : IFrontEndMainMenu {

    private IDatabase db;
    private ISuperUserValidator superUserValidator;

    internal FrontEndMainMenu(IDatabase database, ISuperUserValidator superUserValidator) {
        db = database;
        this.superUserValidator = superUserValidator;
    }

    public bool AddSuperUser(string username, string password) {
        return superUserValidator.AddSuperUserCredentials(username, password);
    }

    public IReadOnlySurveyWrapper? GetSurveyWrapper(int surveyId) {
        return db.GetSurveyWrapper(surveyId);
    }

    public bool ImportSurveyWrapper(string filePath) {
        return db.ImportSurveyWrapper(filePath);        
    }

    public List<IModifySurveyWrapper>? ValidateSuperUser(string username, string password) {
        //Validate superuser against Hashfunction first. If true, then return the list of surveys
        var superUserValidator = new SuperUserValidator();

        if (superUserValidator.ValidateSuperUser(username, password))
        {
            List<SurveyWrapper> surveyWrappers = db.GetSurveyWrapperForSuperUser(username);
            List<IModifySurveyWrapper> result = new List<IModifySurveyWrapper>(surveyWrappers.Cast<IModifySurveyWrapper>().ToList());
            return result;
        }
        return null;
    }
}  