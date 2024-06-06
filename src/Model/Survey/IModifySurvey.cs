namespace Model.Survey;
using Model.Question;
public interface IModifySurvey {

    string SurveyId {get;}
    string SurveyName {get; set;}

    /// <summary>
    /// Get MultiQuestion at the specified index, in editable form
    /// </summary>
    IMultiQuestion<IModifyQuestion>? TryGetModifyMultiQuestion(int index);

    /// <summary>
    /// Move to Next MultiQuestion, and get it in editable form
    /// </summary>
    IMultiQuestion<IModifyQuestion>? TryGetNextModifyMultiQuestion();

    /// <summary>
    /// Move to Previous MultiQuestion, and get it in editable form
    /// </summary>
    IMultiQuestion<IModifyQuestion>? TryGetPreviousModifyMultiQuestion();

    /// <summary>
    /// Delete MultiQuestion at the specified index
    /// </summary>
    void DeleteMultiQuestion(int index);

    /// <summary>
    ///Add new question at the end of the Enumerable
    /// </summary>
    IMultiQuestion<IModifyQuestion> AddNewMultiQuestion(); 

    /// <summary>
    /// Insert new question at the specified index
    /// </summary>
    IMultiQuestion<IModifyQuestion> InsertNewMultiQuestion(int index); 

}