namespace Model.FrontEndAPI;
using Model.Survey;

public interface IFrontEndSuperUser {
    
    IModifySurveyWrapper CreateSurveyWrapper();
    IModifySurveyWrapper? ModifySurveyWrapper(int surveyWrapperId); // Possibly (SuperUserId, SurveyId)?
    bool DeleteSurveyWrapper(int surveyWrapperId);

    void StoreSurveyWrapperInDatabase (IModifySurveyWrapper surveyWrapper);

    bool ExportSurveyWrapperFromDatabase(int SurveyWrapperId, string folderPath);

    string StorePicture(int SurveyWrapperId, string filePath);
    void StorePicture(int SurveyWrapperId, string filePath, string optionalPrefix); //filename prefix: eg version_A_fbpic1, version_B_fbpic1
}