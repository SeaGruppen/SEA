namespace Model.Question;
using Answer;

public interface IReadOnlyQuestion {
    /// <summary>
    /// Id of question
    /// </summary>
    string QuestionId {get;}

    /// <summary>
    /// Read-only caption text of question
    /// </summary>
    string ReadOnlyCaption {get;}

    /// <summary>
    /// Read-only picture of question
    /// </summary>
    string ReadOnlyPicture {get;}

    /// <summary>
    /// Read-only question text
    /// </summary>
    string ReadOnlyText {get;}

    /// <summary>
    /// Get the read-only question
    /// </summary>
    IReadOnlyAnswer ReadOnlyAnswer {get;}
}
