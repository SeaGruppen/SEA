namespace Model.Result;
using Answer;
public interface IResult
{
    AnswerType AnswerType {get;}
    List<string> QuestionResult {get; set;}
    int UserId {get;}
    string QuestionId {get;}
    string SurveyId {get;}
}