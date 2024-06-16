namespace Model.Survey;

public interface IReadOnlySurveyWrapper {

    IReadOnlySurvey? TryGetReadOnlySurveyVersion(int index); // Return survey index'

    int GetVersionCount(); // Return number of versions

    string SurveyWrapperName { get; }
    int SurveyWrapperId { get; }

}
