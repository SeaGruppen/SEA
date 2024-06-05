namespace Model.Survey;
using Model.Question;
using Model.Answer;

public interface IModifySurvey {

    string SurveyId {get;}
    string SurveyName {get; set;}
    IMultiQuestion<IModifyQuestion>? TryGetModifyMultiQuestion(int index);
    IMultiQuestion<IModifyQuestion>? TryGetNextModifyMultiQuestion();
    IMultiQuestion<IModifyQuestion>? TryGetPreviousModifyMultiQuestion();
    void DeleteMultiQuestion(int index);
    IMultiQuestion<IModifyQuestion> AddNewMultiQuestion(); // Add new question at the end of the Enumerable
    IMultiQuestion<IModifyQuestion> InsertNewMultiQuestion(int index); // Add new question at position 'index'

}