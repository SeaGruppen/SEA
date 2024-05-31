namespace Model.FrontEndAPI;
using Model.Survey;
using Model.Database;
internal class FrontEndSuperUserMenu : IFrontEndSuperUser {

    private  IDatabase db = new DatabaseServices();

    internal FrontEndSuperUserMenu(DatabaseServices databaseServices) {
        this.db = databaseServices;
    }

    public IModifySurveyWrapper CreateSurvey() {
        string surveyId = db.GetNextSurveyWrapperID();
        return new SurveyWrapper(surveyId.ToString());
    }

    public bool ExportSurveyFromDatabase(string surveyId, string folderPath) {
        if (db.ExportSurvey(surveyId, folderPath)) {
            return true;
        } else {
            return false;
        }
    }

    public IModifySurveyWrapper ModifySurvey(string surveyId) {
        return db.GetSurveyWrapper(surveyId);
    }

    public void StorePicture(string surveyId, string filePath) {
        //To be implemented
    }

    public void StorePicture(string surveyId, string filePath, string optionalPrefix) {
        //To be implemented
    }

    public void StoreSurveyInDatabase(IModifySurvey survey) {
        
    }

}