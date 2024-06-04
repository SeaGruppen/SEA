namespace Model.Survey;
using System.Text.Json;
using System.Text.Json.Serialization;



internal class SurveyWrapper : IReadOnlySurveyWrapper, IModifySurveyWrapper {

    private int surveyWrapperId;

    private string surveyWrapperName;

    [JsonInclude]
    private string[] surveyAssests;

    public int SurveyWrapperId { get => surveyWrapperId;}
    public string SurveyWrapperName { get => surveyWrapperName; set => surveyWrapperName = value;}

    private int current = 0;
    [JsonInclude]
    private int nextSurveyId  = 0;

    [JsonInclude]
    private List<Survey> surveyVersions = new List<Survey>();

    // Created for testing purposes.
    public IReadOnlyList<Survey> SurveyVersions => surveyVersions;



    public SurveyWrapper (int surveyWrapperId) {
        this.surveyWrapperId = surveyWrapperId;
        // SurveyWrapperName = string.Empty;
        surveyAssests = [];
        surveyWrapperName = string.Empty;
    }

    public IModifySurvey CopyVersion(int index) {
        Survey surveyToCopy = surveyVersions[index];
        Survey copiedSurvey = CopySurvey(surveyToCopy);
        string newSurveyId = SurveyWrapperId.ToString() + "." + nextSurveyId++;
        copiedSurvey.UpdateId(newSurveyId);
        surveyVersions.Add(copiedSurvey);
        return copiedSurvey;
    }

    public IModifySurvey AddNewVersion() {
        var survey = new Survey(surveyWrapperId + "." + nextSurveyId++); // 3797.0
        //nextSurveyId++;
        surveyVersions.Add(survey);
        return survey;
    }

    public void DeleteVersion(int index) {
        try
        {
            surveyVersions.RemoveAt(index);
        } 
        catch (Exception e)
        {
            // Caught exception index out of range.
        }
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

    private Survey CopySurvey(Survey surveyToCopy) {
        // var options = new JsonSerializerOptions { WriteIndented = true };
        // options.Converters.Add(new Model.Question.MultiQuestionConverter());
        string jsonString = JsonSerializer.Serialize(surveyToCopy, Globals.OPTIONS);
        return JsonSerializer.Deserialize<Survey>(jsonString, Globals.OPTIONS)!;
    }
}