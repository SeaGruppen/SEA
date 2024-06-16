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
    /// Stores a SurveyWrapper in the database. 
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
    /// Loads and returns a list of all the SWs created by the user
    /// </summary>
    /// <remarks>
    /// If no SWs have been registered as belonging to the user, null is returned.
    /// </remarks>
    /// <param name="username">the id of the user requesting all their SWs.</param>
    /// <returns>List<SurveyWrapper></returns>
    List<SurveyWrapper>? GetSurveyWrapperForSuperUser(string username);

    /// <summary>
    /// Zips the SW database folder (including its .json file and its assets) 
    /// indicated by the id and saves the .zip file to the provided path. 
    /// </summary>
    /// <remarks>
    /// If the SW doesn't exist or if the .zip file already exists, it returns 
    ///false. Otherwise, it returns true.
    /// </remarks>
    /// <param name="surveyWrapperId">The id of the SW to export.</param>
    /// <param name="path">The path to export the SW to.</param>
    /// <returns>bool</returns>
    bool ExportSurveyWrapper(int surveyWrapperid,string path);

    /// <summary>
    /// Imports a zipped SurveyWrapper (the SW folder containing its .json file 
    /// and its assets) into the database and returns true upon succes.
    /// </summary>
    /// <remarks>
    /// If a SurveyWrapper with that id already exists in the database, it will
    /// not be overwritten and false is returned.
    /// </remarks>
    /// <param name="path">The path to the SW zip file.</param>
    /// <returns>bool</returns>
    bool ImportSurveyWrapper(string path);

    /// <summary>
    /// Copies the picture on the given path into the assets folder of the SW 
    /// with the given id in the database and returns a relative path to the 
    /// picture now stored in assets.  
    /// </summary>
    /// <remarks>
    /// If a picture of that filename already exists in the assets folder, it 
    /// is not overwritten and an exception is thrown.
    /// </remarks>
    /// <param name="surveyWrapperId">The id of the SW that should store the 
    /// picture</param>
    /// <param name="path">The path to the picture to be stored </param>
    /// <returns>string</returns>
    string TryStorePicture(int surveyWrapperId, string path);

    /// <summary>
    /// Copies the picture on the given path into the assets folder of the SW 
    /// with the given id in the database and returns a relative path to the 
    /// picture now stored in assets.  
    /// </summary>
    /// <remarks>
    /// If a picture of that filename already exists in the assets folder, 
    /// it is overwritten.
    /// </remarks>
    /// <param name="surveyWrapperId">The id of the SW that should store 
    /// the picture</param>
    /// <param name="path">The path to the picture to be stored </param>
    /// <returns>string</returns>
    string StorePictureOverwrite(int surveyWrapperId, string path);

    /// <summary>
    /// Goes through all results for a SurveyWrapper and returns the final ones in a list.  
    /// </summary>
    /// <remarks>
    /// If the same user has answered a Survey more than once, only the latest 
    /// (ie final) results are returned in the list. The intermediate results 
    //continue to  exist in the database. 
    /// </remarks>
    /// <param name="id">The id of the SW for which the results are returned</param>
    /// <returns>List<Result></returns>
    List<Result> GetSurveyWrapperResults(int id);

    /// <summary>
    /// Takes a list of Results, stores them in the database and returns true on success.
    /// </summary>
    /// <param name="results"></param>
    /// <returns>bool</returns>
    bool StoreResults(List<Result> results);

    /// <summary>
    /// Take a Result, stores it in the database and returns true on success.
    /// </summary>
    /// <param name="result"></param>
    /// <returns>bool</returns>
    bool StoreResult(IResult result);


    /// <summary>
    /// Returns a list of all the ids for which there are SurveyWrappers 
    /// stored in the database.
    /// </summary>
    /// <param name></param>
    /// <returns>bool</returns>
    List<int> GetAllSurveyWrapperIds();

    /// <summary>
    /// Returns a hash of a GUID
    /// </summary>
    /// <returns>int</returns>
    int GetNextUserId();
}
