namespace Model.Survey;

public interface IModifySurveyWrapper {

    // Get a list of all surveys, for information, cannot multi modify surveys this way though.
    IReadOnlyList<IReadOnlySurvey> SurveyVersions {get;}
    IModifySurvey? TryGetModifySurveyVersion(int index); // Return survey index'

    IModifySurvey AddNewVersion();

    int GetVersionCount(); // Return number of versions

    IModifySurvey CopyVersion(int index);

    void DeleteVersion(int index);
    string[] GetSurveyAssets(); // Get pictures from the survey

    string SurveyWrapperName {get; set;}
    int SurveyWrapperId {get;}
}