namespace Model.Survey;

public interface IModifySurveyWrapper {

    /// <summary>
    /// Gets a read-only list of the survey versions
    /// </summary>
    IReadOnlyList<IReadOnlySurvey> SurveyVersions {get;}

    /// <summary>
    /// Gets survey version based on index
    /// </summary>
    /// <param name="index">Index of survey version to get</param>
    /// <returns>Returns survey version if index is valid, otherwise null</returns>
    IModifySurvey? TryGetModifySurveyVersion(int index);

    /// <summary>
    /// Adds new empty survey version
    /// </summary>
    /// <returns>The newly added survey</returns>
    IModifySurvey AddNewVersion();

    /// <summary>
    /// Returns the number of versions
    /// </summary>
    int GetVersionCount();

    /// <summary>
    /// Copies a survey version based  on given index
    /// </summary>
    /// <param name="index">Index of survey version to copy</param>
    /// <returns>Copy of the version</returns>
    IModifySurvey CopyVersion(int index);

    /// <summary>
    /// Deletes a survey version based  on given index
    /// </summary>
    /// <param name="index">Index of survey version to delete</param>
    void DeleteVersion(int index);

    /// <summary>
    /// Copies a survey version based  on given index
    /// </summary>
    /// <param name="index">Index of survey version to copy</param>
    string[] GetSurveyAssets(); // Get pictures from the survey

    /// <summary>
    /// Gets and sets the name of the survey wrapper
    /// </summary>
    string SurveyWrapperName {get; set;}

    /// <summary>
    /// Gets the id of the survey wrapper
    /// </summary>
    int SurveyWrapperId {get;}
}