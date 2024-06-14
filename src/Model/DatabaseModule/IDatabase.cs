namespace Model.DatabaseModule;

using System.Collections.Generic;
using Result;
using Survey;

internal interface IDatabase {

    /// <summary>
    /// Creates and returns a random 6 digit number not already in use by any 
    /// existing SurveyWrappers in the DB to be used as the id for a new 
    /// SurveyWrapper. It also records in a json file in the main database 
    /// folder the creator of the new SW. The recording is in the form of a 
    /// dict with usernames as keys and the list of SWs they have created as values.
    /// </summary>
    /// <param name="superUserName">The username of the superuser creating a 
    /// new SurveyWrapper.</param>
    /// <returns>int</returns>
    int GetNextSurveyWrapperID(string superUserName);
    
    /// <summary>
    ///  Stores a SurveyWrapper in the database. 
    /// </summary>
    /// <remarks>
    /// It uses the SW id to find the correct dir to store it in. 
    /// If the SW was not already stored, it creates a new dir named after its 
    /// id to store it in. The SW is serialized to JSON, and then saved as a .json file 
    /// named after its id. 
    /// The database is structured as a main database folder containing 
    /// subfolders for each SW and its assets to be stored in. 
    /// </remarks>
    /// <param name="surveyWrapper">The SurveyWrapper to be stored in 
    /// the database</param>
    /// <returns>bool</returns>
    bool StoreSurveyWrapper(SurveyWrapper surveyWrapper);

    /// <summary>
    /// Deletes a SurveyWrapper from the database after validating the user 
    /// trying to delete as the creator of the SurveyWrapper.
    /// </summary>
    /// <remarks>
    /// If the user does not have the SW recorded as being created by them or 
    /// does not exist in the creatorDict, it returns false. Otherwise, the 
    /// creatorDict is updated to reflect the deletion of the SW and the SW is 
    /// deleted. If the SW does not exist in the database, it returns false. 
    /// Otherwise, it returns true.
    /// </remarks>
    /// <param name="surveyWrapperId">the id of the SW to be deleted from the
    ///  database.</param>
    /// <param name="username">the id of the user requesting to delete 
    /// the SW.</param>
    /// <returns>bool</returns>
    bool DeleteSurveyWrapper(int surveyWrapperId, string username);

    /// <summary>
    /// Loads and returns a SurveyWrapper from the database if it exists
    /// </summary>
    /// <remarks>
    /// It uses the SW id to locate the correct SW .json file in the database,
    /// reads it in and deserializes it to a SurveyWrapper object before 
    /// returning it.  
    /// </remarks>
    /// <param name="surveyWrapperId">the id of the SW to be loaded from the
    ///  database.</param>
    /// <returns>SurveyWrapper</returns>
    SurveyWrapper? GetSurveyWrapper(int surveyWrapperId);

    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// 
    /// </remarks>
    /// <param name="username">the id of the user requesting all their SWs.</param>
    /// <returns>List<SurveyWrapper></returns>
    List<SurveyWrapper>? GetSurveyWrapperForSuperUser(string username);
    bool ExportSurveyWrapper(int surveyWrapperid,string path);
    bool ImportSurveyWrapper(string path);
    string TryStorePicture(int surveyWrapperId, string path);
    string StorePictureOverwrite(int surveyWrapperId, string path);
    List<Result> GetSurveyWrapperResults(int id);
    bool StoreResults(List<Result> results);
    bool StoreResult(IResult result);
    List<int> GetAllSurveyWrapperIds();
    int GetNextUserId();
}
