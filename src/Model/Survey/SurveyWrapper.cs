
namespace Model.Survey;



internal class SurveyWrapper : IReadOnlySurveyWrapper, IModifySurveyWrapper {

    private string surveyWrapperId;

    private string surveyWrapperName;

    private string[] surveyAssests;

    public string SurveyWrapperId { get => surveyWrapperId;}
    public string SurveyWrapperName { get => surveyWrapperName; set => surveyWrapperName = value;}

    private int current = 0;
    private List<Survey> surveyVersions = new List<Survey>();
    private int nextSurveyId = 0;

    public SurveyWrapper (string surveyWrapperId) {
        this.surveyWrapperId = surveyWrapperId;
        SurveyWrapperName = string.Empty;
        surveyAssests = new string[] {};
        surveyWrapperName = string.Empty;
    }

    public void CopyVersion(int index) {
        // var copiedVersion = surveyVersions[index];
        // surveyVersions.Add(copiedVersion);
    }

    public IModifySurvey AddNewVersion() {
        string surveyId = GetNextSurveyId();
        var survey = new Survey(surveyId);
        surveyVersions.Add(survey);
        return survey;
    }

    private string GetNextSurveyId() {
        int surveyId =  nextSurveyId++;
        return string.Concat(surveyWrapperId, "-", surveyId);
    }

    public void DeleteVersion(int index) {
        surveyVersions.RemoveAt(index);
    }

    public string[] GetSurveyAssets() {
        return surveyAssests;
    }

    public int GetVersionCount() {
        return surveyVersions.Count();
    }

    public IModifySurvey? TryGetModifySurveyVersion(int index)
    {
        if(0 <= index && index < surveyVersions.Count) {
            current = index;
            return surveyVersions[index];
        } else {
            return null;
        }
    }

    public IReadOnlySurvey? TryGetReadOnlySurveyVersion(int index)
    {
        if(0 <= index && index < surveyVersions.Count) {
            current = index;
            return surveyVersions[index];
        } else {
            return null;
        }
    }
}