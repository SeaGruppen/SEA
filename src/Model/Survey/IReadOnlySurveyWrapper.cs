namespace Model.Survey;

public interface IReadOnlySurveyWrapper {

    /// <summary>
    /// Returns the survey wrapper name
    /// </summary>
    string SurveyWrapperName { get; }

    /// <summary>
    /// Returns the nsurvey wrapper id
    /// </summary>
    int SurveyWrapperId { get; }
}
