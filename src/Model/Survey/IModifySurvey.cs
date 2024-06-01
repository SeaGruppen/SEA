namespace Model.Survey;
using Model.Question;
using Model.Answer;

public interface IModifySurvey {

    int SurveyId {get;}
    string SurveyName {get; set;}
    IMultiQuestion<IModifyQuestion>? TryGetModifyQuestion(int index);
    IMultiQuestion<IModifyQuestion>? TryGetNextModifyQuestion();
    IMultiQuestion<IModifyQuestion>? TryGetPreviousModifyQuestion();
    void DeleteQuestion(int index);
    IMultiQuestion<IModifyQuestion> AddNewQuestion(); // Add new question at the end of the Enumerable
    IMultiQuestion<IModifyQuestion> InsertNewQuestion(int index); // Add new question at position 'index'

}