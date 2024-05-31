namespace Model.Survey;
using System;

using Model.Question;
using Model.Answer;
using System.Collections.Generic;

internal class Survey : IReadOnlySurvey, IModifySurvey {
    public string SurveyId {get;}

    public string SurveyName {get; set;}

    private List<MultiQuestion> surveyQuestions = new List<MultiQuestion>();

    private int current = -1;
    private int nextMultiQuestionId = 0;

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

    public IEnumerable<IModifyQuestion>? TryGetModifyQuestion(int index) {
        if(0 <= index && index < surveyQuestions.Count) {
            current = index;
            return surveyQuestions[index];
        } else {
            return null;
        }
    }
    public IEnumerable<IModifyQuestion>? TryGetNextModifyQuestion()
    {
        if(0 <= current && NextQuestionExist()) {
            current++;
            return surveyQuestions[current];
        } else {
            return null;
        }
    }

    public IEnumerable<IModifyQuestion>? TryGetPreviousModifyQuestion() {
        if(PreviousQuestionExist() && current < surveyQuestions.Count) {
            current--;
            return surveyQuestions[current];
        } else {
            return null;
        }
    }

    public void DeleteQuestion(int index) {
        if(0 < current && current < surveyQuestions.Count) {
            surveyQuestions.RemoveAt(index);
        }
    }

    public IEnumerable<Question> AddNewQuestion() {
        MultiQuestion result = new MultiQuestion(GetNextMultiQuestionId());
        surveyQuestions.Add(result);
        return result;
    }


    public IEnumerable<Question> InsertNewQuestion(int index) {
        MultiQuestion result = new MultiQuestion(GetNextMultiQuestionId());
        surveyQuestions.Insert(index, result);
        return result;
    }
    private string GetNextMultiQuestionId() {
        string multiquestionId = (nextMultiQuestionId++).ToString();
        return multiquestionId;
    }
}
