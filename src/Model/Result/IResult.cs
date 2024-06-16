namespace Model.Result;
using Answer;
public interface IResult
{
    /// <summary>
    /// Get the type of the answer
    /// </summary>
    AnswerType AnswerType {get;}

    /// <summary>
    /// Gets and sets the question result input by the user
    /// </summary>
    List<string> QuestionResult {get; set;}

    /// <summary>
    /// Id of user answering the question
    /// </summary>
    int UserId {get;}

    /// <summary>
    /// Question id
    /// </summary>
    string QuestionId {get;}

    /// <summary>
    /// Survey id
    /// </summary>
    string SurveyId {get;}
}