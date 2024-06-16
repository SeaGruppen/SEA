namespace Model.Survey;

public interface IReadOnlySurveyWrapper {

    /// <summary>
    /// Gets survey version based on index
    /// </summary>
    /// <param name="index">Index of survey version to get</param>
    /// <returns>Returns survey version if index is valid, otherwise null</returns>
    IReadOnlySurvey? TryGetReadOnlySurveyVersion(int index);

    /// <summary>
    /// Returns the number of versions
    /// </summary>
    int GetVersionCount();

    /// <summary>
    /// Returns the survey wrapper name
    /// </summary>
    string SurveyWrapperName { get; }

    /// <summary>
    /// Returns the nsurvey wrapper id
    /// </summary>
    int SurveyWrapperId { get; }
}
