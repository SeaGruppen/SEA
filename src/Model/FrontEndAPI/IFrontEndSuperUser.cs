namespace Model.FrontEndAPI;
using Model.Survey;

public interface IFrontEndSuperUser {
    
    IModifySurveyWrapper CreateSurvey();
    IModifySurveyWrapper ModifySurvey(string surveyId); // Possibly (SuperUserId, SurveyId)?

    void StoreSurveyInDatabase (IModifySurvey survey);

    bool ExportSurveyFromDatabase(string surveyId, string folderPath);

    string StorePicture(string surveyId, string filePath);
    void StorePicture(string surveyId, string filePath, string optionalPrefix); //filename prefix: eg version_A_fbpic1, version_B_fbpic1
}