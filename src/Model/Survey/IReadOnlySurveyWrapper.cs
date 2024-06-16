namespace Model.Survey;

public interface IReadOnlySurveyWrapper {

    /// <summary>
    /// Gets a read-only Survey from the index of a surveyWrapper
    /// </summary>
    /// <param name="index">Index of survey version to get</param>
    /// <returns>Returns survey version if index is valid, otherwise null</returns>
    IReadOnlySurvey? TryGetReadOnlySurveyVersion(int index); // Return survey index'

    /// <summary>
    /// Get the number of survey versions that this surveyWrapper has
    /// </summary>
    /// <returns>Number of survey versions</returns>
    int GetVersionCount(); // Return number of versions

    string SurveyWrapperName { get; }
    int SurveyWrapperId { get; }

}