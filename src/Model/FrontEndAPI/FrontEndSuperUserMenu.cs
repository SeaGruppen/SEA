namespace Model.FrontEndAPI;
using Model.Survey;
using Model.Database;
internal class FrontEndSuperUserMenu : IFrontEndSuperUser {

    private  IDatabase db = new DatabaseServices();

    internal FrontEndSuperUserMenu(IDatabase databaseServices) {
        this.db = databaseServices;
    }

    public IModifySurveyWrapper CreateSurveyWrapper() {
        int surveyId = db.GetNextSurveyWrapperID();
        return new SurveyWrapper(surveyId);
    }

    public bool DeleteSurveyWrapper(int surveyId) {
        if (db.DeleteSurveyWrapper(surveyId)) {
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
}