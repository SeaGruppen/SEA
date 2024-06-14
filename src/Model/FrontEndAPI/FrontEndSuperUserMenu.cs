namespace Model.FrontEndAPI;
using Survey;
using DatabaseModule;
using UserValidation;

internal class FrontEndSuperUserMenu : IFrontEndSuperUser {

    private  IDatabase db = new Database();
    private ISuperUserValidator superUserValidator;

    internal FrontEndSuperUserMenu(IDatabase databaseServices, ISuperUserValidator superUserValidator) {
        this.db = databaseServices;
        this.superUserValidator = superUserValidator;
    }

    public List<IModifySurveyWrapper>? GetSurveyWrappersFromSuperUser(string username) {
        List<SurveyWrapper>? surveyWrappers = db.GetSurveyWrapperForSuperUser(username);
        if (surveyWrappers == null) {
            return null;
        }
        List<IModifySurveyWrapper> result = new List<IModifySurveyWrapper>(surveyWrappers.Cast<IModifySurveyWrapper>().ToList());
        return result;
    }

    public IModifySurveyWrapper CreateSurveyWrapper(string superUserName, string surveyWrapperName) {
        int surveyId = db.GetNextSurveyWrapperID(superUserName);
        SurveyWrapper newSurveyWrapper = new SurveyWrapper(surveyId);
        newSurveyWrapper.SurveyWrapperName = surveyWrapperName;
        db.StoreSurveyWrapper(newSurveyWrapper);
        return newSurveyWrapper;
    }

    public bool DeleteSurveyWrapper(int surveyId, string username) {
        if (db.DeleteSurveyWrapper(surveyId, username)) {
            return true;
        } else {
            return false;
        }
    }
    public bool ExportSurveyWrapperFromDatabase(int surveyId, string folderPath) {
        if (db.ExportSurveyWrapper(surveyId, folderPath)) {
            return true;
        } else {
            return false;
        }
    }

    public IModifySurveyWrapper? ModifySurveyWrapper(int surveyId) {
        return db.GetSurveyWrapper(surveyId);
    }

    public string StorePicture(int surveyId, string filePath) {
        return db.TryStorePicture(surveyId, filePath);
    }

    public void StorePicture(int surveyId, string filePath, string optionalPrefix) {
        //To be implemented
    }

    public void StoreSurveyWrapperInDatabase(IModifySurveyWrapper modifySurveyWrapper) {
        //Try downcasting to SurveyWrapper
        if (modifySurveyWrapper is SurveyWrapper surveyWrapper)
            db.StoreSurveyWrapper(surveyWrapper as SurveyWrapper);
        else {
            // Downcast failed, handle accordingly
            throw new InvalidCastException("The provided surveyWrapper is not of type SurveyWrapper, Have you gotten hacked?");
        }
    }

    public List<IModifySurveyWrapper>? GetSurveyWrappersFromSuperUser(string username, string password) {
        //Validate superuser against Hashfunction first. If true, then return the list of surveys
        if (superUserValidator.ValidateSuperUser(username, password)) {
            List<SurveyWrapper>? surveyWrappers = db.GetSurveyWrapperForSuperUser(username);
            if (surveyWrappers == null) {
                return null;
            }
            List<IModifySurveyWrapper> result = new List<IModifySurveyWrapper>(surveyWrappers.Cast<IModifySurveyWrapper>().ToList());
            return result;
        }
        return null;
    }
}