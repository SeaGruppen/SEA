namespace Model.FrontEndAPI;
using Model.Survey;

public interface IFrontEndMainMenu {
    List<IModifySurveyWrapper>? ValidateSuperUser(string username, string password);
    bool ImportSurvey(string filePath);

    bool ExportResults(string surveyId, string folderPath);
    IReadOnlySurveyWrapper? GetSurvey(string pincode);

}