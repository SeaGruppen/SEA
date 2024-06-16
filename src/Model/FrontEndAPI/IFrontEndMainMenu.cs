namespace Model.FrontEndAPI;
using Survey;

public interface IFrontEndMainMenu {
    /// <summary>
    /// Validates user credentials and will fetch associated survey wrappers
    /// </summary>
    /// <param name="username">superuser username</param>
    /// <param name="password">superuser password</param>
    /// <returns>If credentials are valid, the list of modifiable surveywrappers assiociated with the user otherwise null</returns>
    List<IModifySurveyWrapper>? ValidateSuperUser(string username, string password);

    /// <summary>
    /// Import survey wrapper from given path
    /// </summary>
    /// <param name="filepath">Path for importing surveywrapper</param>
    /// <returns>True of surveywrapper was received otherwise false</returns>
    bool ImportSurveyWrapper(string filePath);

    /// <summary>
    /// Adds new superuser credentialo
    /// </summary>
    /// <param name="username">superuser username</param>
    /// <param name="password">superuser password</param>
    /// <returns>True if credentials are not already added, otherwise false</returns>
    bool AddSuperUser(string username, string password);

    /// <summary>
    /// Based on pincode will get the associated surveywrapper
    /// </summary>
    /// <param name="pincode">pincode to validate</param>
    /// <returns>ReadOnly surveywrapper if valid pincode, otherwise null</returns>
    IReadOnlySurveyWrapper? GetSurveyWrapper(int pincode);

}