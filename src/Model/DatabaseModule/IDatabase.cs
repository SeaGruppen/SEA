namespace Model.DatabaseModule;

using System.Collections.Generic;
using Result;
using Survey;

internal interface IDatabase {
    /// <summary>
    /// Gets survey version based on index
    /// </summary>
    /// <param name="superUserName">name of user to get wrapper id for </param>
    /// <returns>Returns survey wrapper id as int</returns>
    int GetNextSurveyWrapperID(string superUserName);

    /// <summary>
    /// Stores a survey wrapper in the database
    /// </summary>
    /// <param name="surveyWrapper">Survey wrapper to store</param>
    /// <returns>Returns true if succesfully stored otherwise false</returns>
    bool StoreSurveyWrapper(SurveyWrapper surveyWrapper);

    /// <summary>
    /// Deletes a survey wrapper in the database
    /// </summary>
    /// <param name="surveyWrapperId">Id of survey wrapper to delete</param>
    /// <param name="username">Username of user associated with survey wrapper</param>
    /// <returns>Returns true if succesfully deleted otherwise false</returns>
    bool DeleteSurveyWrapper(int surveyWrapperId, string username);

    /// <summary>
    /// Gets a survey wrapper based on id
    /// </summary>
    /// <param name="surveyWrapperid">Id survey wrapper to get</param>
    /// <returns>Returns the surveyw wrapper if exists otherwise null</returns>
    SurveyWrapper? GetSurveyWrapper(int surveyWrapperId);

    /// <summary>
    /// Gets all survey wrapper of a super user
    /// </summary>
    /// <param name="username">Username of the user to get survvey wrappers for</param>
    /// <returns>Returns a list of the surveywrappers if username is valid otherwise null</returns>
    List<SurveyWrapper>? GetSurveyWrapperForSuperUser(string username);

    /// <summary>
    /// Exports a survey wrapper
    /// </summary>
    /// <param name="surveyWrapperId">Id of survey wrapper to export</param>
    /// <param name="path">Path of survey wrapper to export</param>
    /// <returns>Returns true if succesfully exported otherwise false</returns>
    bool ExportSurveyWrapper(int surveyWrapperid, string path);

    /// <summary>
    /// Imports a survey wrapper
    /// </summary>
    /// <param name="path">Path of location to import survey wrapper</param>
    /// <returns>Returns true if succesfully imported otherwise false</returns>
    bool ImportSurveyWrapper(string path);

    /// <summary>
    /// Store a picture for a survey wrapper
    /// </summary>
    /// <param name="surveyWrapperId">Id of survey wrapper to containing the picture</param>
    /// <param name="path">Path of directory to save picture</param>
    /// <returns>Path of picture file</returns>
    string TryStorePicture(int surveyWrapperId, string path);

    /// <summary>
    /// Overwrites an existing survey wrapper picture
    /// </summary>
    /// <param name="surveyWrapperId">Id of survey wrapper to containing the picture</param>
    /// <param name="path">Path of directory to save picture</param>
    /// <returns>Path of picture file</returns>
    string StorePictureOverwrite(int surveyWrapperId, string path);

    /// <summary>
    /// Get survey wrapper results based on wrapper id
    /// </summary>
    /// <param name="id">Id of survey wrapper to get results from</param>
    /// <returns>Returns a list of results</returns>
    List<Result> GetSurveyWrapperResults(int id);

    /// <summary>
    /// Stores a list of results
    /// </summary>
    /// <param name="id">Id of survey wrapper to get results from</param>
    /// <returns>Returns true if succesfully stored otherwise false</returns>
    bool StoreResults(List<Result> results);

    /// <summary>
    /// Stores a result
    /// </summary>
    /// <param name="result">Result to store</param>
    /// <returns>Returns true if succesfully stored otherwise false</returns>
    bool StoreResult(IResult result);

    /// <summary>
    /// Gets all survey wraooer ids
    /// </summary>
    /// <returns>Returns a list of wurvey wrapper ids</returns>
    List<int> GetAllSurveyWrapperIds();

    /// <summary>
    /// Gets next user id based on current user pointer
    /// </summary>
    /// <returns>The id as int</returns>
    int GetNextUserId();
}
