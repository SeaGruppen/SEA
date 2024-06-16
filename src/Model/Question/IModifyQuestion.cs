namespace Model.Question;
using Answer;

public interface IModifyQuestion {
    /// <summary>
    /// Id for a question.
    /// </summary>
    string QuestionId {get;}

    /// <summary>
    /// Muteable caption text of question
    /// </summary>
    string ModifyCaption {get; set;}

    /// <summary>
    /// Muteable picture of question
    /// </summary>
    string ModifyPicture {get; set;}

    /// <summary>
    /// Muteable question text
    /// </summary>
    string ModifyText {get; set;}

    /// <summary>
    /// Gets answer of question
    /// </summary>
    IModifyAnswer ModifyAnswer {get;}
}