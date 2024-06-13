namespace Model.Question;
using Answer;

public interface IReadOnlyQuestion {
    string QuestionId {get;}
    string ReadOnlyCaption {get;}
    string ReadOnlyPicture {get;}
    string ReadOnlyText{get;}
    IReadOnlyAnswer ReadOnlyAnswer {get;}
}
