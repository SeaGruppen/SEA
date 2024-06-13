namespace Model.Question;
using Answer;

public interface IModifyQuestion {
    string QuestionId {get;}    
    string ModifyCaption {get; set;}
    string ModifyPicture {get; set;}
    string ModifyText {get; set;}
    IModifyAnswer ModifyAnswer {get;}
}