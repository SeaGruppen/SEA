namespace Model.FrontEndAPI;
using Model.Survey;

public interface IFrontEndMainMenu {
    List<IModifySurveyWrapper>? ValidateSuperUser(string username, string password);
    bool ImportSurveyWrapper(string filePath);

    bool ExportResults(int surveyWrapperId, string folderPath);
    IReadOnlySurveyWrapper? GetSurveyWrapper(int pincode);

}