namespace Model.FrontEndAPI;
using Survey;

public interface IFrontEndSuperUser {

    /// <summary>
    /// Gets all survey wrappers of a user based on username
    /// </summary>
    /// <param name="username">Username of user to get surveywrappers for</param>
    /// <returns>A list of survey wrappers if username is valid otherwise false</returns>
    List<IModifySurveyWrapper>? GetSurveyWrappersFromSuperUser(string username);

    /// <summary>
    /// Creates and empty survey wrapper
    /// </summary>
    /// <param name="superUserName">Username of user to create survey wrapper for</param>
    /// <param name="surveyWrapperName">Name of the newly created survey wrapper</param>
    /// <returns>The newly created survey wrapper</returns>
    IModifySurveyWrapper CreateSurveyWrapper(string superUserName, string surveyWrapperName);

    /// <summary>
    /// Gets a muteable survey wrapper based on id
    /// </summary>
    /// <param name="surveyWrapperId">Id of survey wrapper to fetch</param>
    /// <returns>Returns the survey wrapper if id is valid otherwise null</returns>
    IModifySurveyWrapper? ModifySurveyWrapper(int surveyWrapperId);

    /// <summary>
    /// Deletes survey wrapper based on id
    /// </summary>
    /// <param name="surveyWrapperId">Id of survey wrapper to delete</param>
    /// <param name="username">Username of user associated with survey wrapper</param>
    /// <returns>True if survey wrapper is succesfully deleted otherwise false</returns>
    bool DeleteSurveyWrapper(int surveyWrapperId, string username);

    /// <summary>
    /// Stores a survey wrapper in the daatabse
    /// </summary>
    /// <param name="surveyWrapper">Survey wrapper to store</param>
    void StoreSurveyWrapperInDatabase (IModifySurveyWrapper surveyWrapper);

    /// <summary>
    /// Exports survey wrapper based on id and folder path
    /// </summary>
    /// <param name="SurveyWrapperId">Id of survey wrapper to export</param>
    /// <param name="folderPath">Folder path to export from</param>
    /// <returns>True if survey wrapper is succesfully exported otherwise false</returns>
    bool ExportSurveyWrapperFromDatabase(int SurveyWrapperId, string folderPath);

    /// <summary>
    /// Stores a picture based on id and file path
    /// </summary>
    /// <param name="SurveyWrapperId">Id of survey wrapper associated with picture</param>
    /// <param name="filePath">Filepath of file to store picture</param>
    /// <returns>Complete file path of picture</returns>
    string StorePicture(int SurveyWrapperId, string filePath);

    /// <summary>
    /// Stores a picture based on id and file path with optional prefix eg version_A_fbpic1, version_B_fbpic1
    /// </summary>
    /// <param name="SurveyWrapperId">Id of survey wrapper associated with
    /// <param name="filePath">Filepath of file to store picture</param>
    /// <param name="optionalPrefix">Optial prefix for file name</param>
    void StorePicture(int SurveyWrapperId, string filePath, string optionalPrefix)

    /// <summary>
    /// Validates super user credentials and gets their survey wrappers
    /// </summary>
    /// <param name="username">Username of user to get wrappers for</param>
    /// <param name="password">Password of user to get wrappers for</param>
    /// <returns>Returns a list of survey wrappers if exist and credentials are valid otherwise null</returns>
    List<IModifySurveyWrapper>? GetSurveyWrappersFromSuperUser(string username, string password);
}