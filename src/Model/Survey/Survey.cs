namespace Model.Survey;

using System.Collections.Generic;
using System.Text.Json.Serialization;
using Question;

internal class Survey : IReadOnlySurvey, IModifySurvey {
    public string SurveyId {get; private set;}

    public string SurveyName {get; set;}

    [JsonInclude]
    private List<MultiQuestion> surveyQuestions = new List<MultiQuestion>();

    public IReadOnlyList<MultiQuestion> SurveyQuestions => surveyQuestions;

    [JsonInclude]
    private int nextMultiQuestionId = 0;

    private int current = -1;

    public Survey(string surveyId) {
        SurveyId = surveyId;
        SurveyName = string.Empty;
    }

    public bool PreviousQuestionExist() => current > 0;
    public bool NextQuestionExist() => current + 1 < surveyQuestions.Count;

    public IEnumerable<IReadOnlyQuestion>? TryGetNextReadOnlyQuestion()
    {
        return NextQuestionExist()
            ? surveyQuestions[++current]
            : null;
    }

    public IEnumerable<IReadOnlyQuestion>? TryGetPreviousReadOnlyQuestion()
    {
        return PreviousQuestionExist()
            ? surveyQuestions[--current]
            : null;
    }

    public void ResetCounter()
    {
        current = -1;
    }

    public IMultiQuestion<IModifyQuestion>? TryGetModifyMultiQuestion(int index) {
        if(0 <= index && index < surveyQuestions.Count) {
            current = index;
            return (IMultiQuestion<IModifyQuestion>) surveyQuestions[index];
        } else {
            return null;
        }
    }
    public IMultiQuestion<IModifyQuestion>? TryGetNextModifyMultiQuestion()
    {
        if(-1 <= current && NextQuestionExist()) {
            current++;
            return (IMultiQuestion<IModifyQuestion>) surveyQuestions[current];
        } else {
            return null;
        }
    }

    public IMultiQuestion<IModifyQuestion>? TryGetPreviousModifyMultiQuestion() {
        if(PreviousQuestionExist() && current < surveyQuestions.Count) {
            current--;
            return (IMultiQuestion<IModifyQuestion>) surveyQuestions[current];
        } else {
            return null;
        }
    }

    public void DeleteMultiQuestion(int index) {
        if(0 < current && current < surveyQuestions.Count) {
            surveyQuestions.RemoveAt(index);
        }
    }

    public IMultiQuestion<IModifyQuestion> AddNewMultiQuestion() {
        string multiQuestionId = (nextMultiQuestionId++).ToString();
        MultiQuestion result = new MultiQuestion(string.Concat( SurveyId, ".", multiQuestionId));
        surveyQuestions.Add(result);
        return (IMultiQuestion<IModifyQuestion>) result;
    }

    public IMultiQuestion<IModifyQuestion> InsertNewMultiQuestion(int index) {
        string multiQuestionId = (nextMultiQuestionId++).ToString();
        MultiQuestion result = new MultiQuestion(string.Concat( SurveyId, ".", multiQuestionId));
        surveyQuestions.Insert(index, result);
        return (IMultiQuestion<IModifyQuestion>) result;
    }

    internal void UpdateId(string newSurveyId) {
        SurveyId = newSurveyId;
        foreach (MultiQuestion mq in surveyQuestions) {
            string currentMqId = mq.MultiQuestionId;
            string[] parts = currentMqId.Split(".");
            string newMqId = SurveyId + "." + parts.Last();
            mq.UpdateId(newMqId);
        }
    }
}
