namespace Model.FrontEndAPI;
using Model.Survey;

public interface IFrontEndSuperUser {
    
    IModifySurveyWrapper CreateSurvey();
    IModifySurveyWrapper ModifySurvey(int surveyWrapperId); // Possibly (SuperUserId, SurveyId)?

    void StoreSurveyInDatabase (IModifySurveyWrapper surveyWrapper);

    bool ExportSurveyFromDatabase(int SurveyWrapperId, string folderPath);

    string StorePicture(int SurveyWrapperId, string filePath);
    void StorePicture(int SurveyWrapperId, string filePath, string optionalPrefix); //filename prefix: eg version_A_fbpic1, version_B_fbpic1
}