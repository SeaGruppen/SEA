namespace Model.Survey;
using System;

using Model.Question;
using Model.Answer;
using System.Collections.Generic;
using System.Text.Json.Serialization;

internal class Survey : IReadOnlySurvey, IModifySurvey {
    public string SurveyId {get;}

    public string SurveyName {get; set;}

    [JsonInclude]
    private List<MultiQuestion> surveyQuestions = new List<MultiQuestion>();

    [JsonInclude]
    int nextMultiQuestionId = 0;

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
        if(0 <= current && NextQuestionExist()) {
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

    public void DeleteQuestion(int index) {
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
}
