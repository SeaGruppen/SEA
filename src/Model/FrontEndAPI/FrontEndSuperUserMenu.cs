namespace Model.FrontEndAPI;
using Model.Survey;
using Model.Database;
internal class FrontEndSuperUserMenu : IFrontEndSuperUser {

    private  IDatabase db = new DatabaseServices();

    internal FrontEndSuperUserMenu(DatabaseServices databaseServices) {
        this.db = databaseServices;
    }

    public IModifySurveyWrapper CreateSurvey() {
        int surveyId = db.GetNextSurveyID();
        return new SurveyWrapper(surveyId);
    }

    public bool ExportSurveyFromDatabase(int surveyId, string folderPath) {
        if (db.ExportSurvey(surveyId, folderPath)) {
            return true;
        } else {
            return false;
        }
    }

    public IModifySurveyWrapper ModifySurvey(int surveyId) {
        return db.GetSurveyWrapper(surveyId);
    }

    public string StorePicture(int surveyId, string filePath) {
        return db.TryStorePicture(surveyId, filePath);
    }

    public void StorePicture(int surveyId, string filePath, string optionalPrefix) {
        //To be implemented
    }

    public void StoreSurveyInDatabase(IModifySurveyWrapper modifySurveyWrapper) {
        //Try downcasting to SurveyWrapper
        if (modifySurveyWrapper is SurveyWrapper surveyWrapper)
            db.StoreSurveyWrapper(surveyWrapper as SurveyWrapper);
        else {
            // Downcast failed, handle accordingly
            throw new InvalidCastException("The provided surveyWrapper is not of type SurveyWrapper, Have you gotten hacked?");
        }
    }
}