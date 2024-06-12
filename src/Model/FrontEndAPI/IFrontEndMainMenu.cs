namespace Model.FrontEndAPI;
using Model.Survey;

public interface IFrontEndMainMenu {
    List<IModifySurveyWrapper>? ValidateSuperUser(string username, string password);
    bool ImportSurveyWrapper(string filePath);
    bool AddSuperUser(string username, string password);
    IReadOnlySurveyWrapper? GetSurveyWrapper(int pincode);

}