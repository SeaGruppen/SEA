namespace Model.FrontEndAPI;
using Model.Survey;
using Model.Database;
internal class FrontEndSuperUserMenu : IFrontEndSuperUser {

    private  IDatabase db = new DatabaseServices();

    internal FrontEndSuperUserMenu(DatabaseServices databaseServices) {
        this.db = databaseServices;
    }

    public List<IModifySurveyWrapper>? GetSurveyWrappersFromSuperUser(string username) {
        List<SurveyWrapper> surveyWrappers = db.GetSurveyWrapperForSuperUser(username);
        List<IModifySurveyWrapper> result = new List<IModifySurveyWrapper>(surveyWrappers.Cast<IModifySurveyWrapper>().ToList());
        return result;
    }

    public IModifySurveyWrapper CreateSurveyWrapper(string superUserName) {
        int surveyId = db.GetNextSurveyWrapperID(superUserName);
        SurveyWrapper newSurveyWrapper = new SurveyWrapper(surveyId);
        db.StoreSurveyWrapper(newSurveyWrapper);
        return newSurveyWrapper;
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
}