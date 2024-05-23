namespace Model.Question;
using Model.Answer;

public interface IModifyQuestion {
    int QuestionId {get;}    
    string ModifyCaption {get; set;}
    string ModifyPicture {get; set;}
    string ModifyText {get; set;}
    IModifyAnswer ModifyAnswer {get;}
}