namespace Model.Survey;
using Question;

public interface IReadOnlySurvey {
    string SurveyId {get;}
    string SurveyName {get;}
    bool PreviousQuestionExist();
    bool NextQuestionExist();
    IEnumerable<IReadOnlyQuestion>? TryGetNextReadOnlyQuestion();
    IEnumerable<IReadOnlyQuestion>? TryGetPreviousReadOnlyQuestion();
    void ResetCounter();
}