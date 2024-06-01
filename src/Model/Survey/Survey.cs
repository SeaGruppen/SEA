namespace Model.Survey;
using System;

using Model.Question;
using Model.Answer;
using System.Collections.Generic;

internal class Survey : IReadOnlySurvey, IModifySurvey {
    public int SurveyId {get;}

    public string SurveyName {get; set;}

    private List<MultiQuestion> surveyQuestions = new List<MultiQuestion>();

    int nextMultiQuestionId = 0;

    private int current = -1;

    public Survey(int surveyId) {
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

    public IMultiQuestion<IModifyQuestion>? TryGetModifyQuestion(int index) {
        if(0 <= index && index < surveyQuestions.Count) {
            current = index;
            return (IMultiQuestion<IModifyQuestion>) surveyQuestions[index];
        } else {
            return null;
        }
    }
    public IMultiQuestion<IModifyQuestion>? TryGetNextModifyQuestion()
    {
        if(0 <= current && NextQuestionExist()) {
            current++;
            return (IMultiQuestion<IModifyQuestion>) surveyQuestions[current];
        } else {
            return null;
        }
    }

    public IMultiQuestion<IModifyQuestion>? TryGetPreviousModifyQuestion() {
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

    public IMultiQuestion<IModifyQuestion> AddNewQuestion() {
        string multiQuestionId = (nextMultiQuestionId++).ToString();
        MultiQuestion result = new MultiQuestion(string.Concat( SurveyId.ToString(), ".", multiQuestionId));
        surveyQuestions.Add(result);
        return (IMultiQuestion<IModifyQuestion>) result;
    }

    public IMultiQuestion<IModifyQuestion> InsertNewQuestion(int index) {
        string multiQuestionId = (nextMultiQuestionId++).ToString();
        MultiQuestion result = new MultiQuestion(string.Concat( SurveyId.ToString(), ".", multiQuestionId));
        surveyQuestions.Insert(index, result);
        return (IMultiQuestion<IModifyQuestion>) result;
    }
}
