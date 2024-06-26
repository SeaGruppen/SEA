namespace Model.Answer;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;

// Answer is the given options to a question, by the survey creater.
// When an experimentee takes the survey, they will give a Result
public class Answer : IModifyAnswer, IReadOnlyAnswer {


    [JsonInclude]
    private List<string> modifyAnswers = new List<string>();

    [JsonInclude]
    private AnswerType answerType = AnswerType.Text;
    
    public ReadOnlyCollection<string> ModifyAnswers => modifyAnswers.AsReadOnly();

    public AnswerType ModifyAnswerType { get => answerType; set => answerType = value; }

    public AnswerType ReadOnlyAnswerType {get => answerType;}

    public ReadOnlyCollection<string> ReadOnlyAnswers => modifyAnswers.AsReadOnly();

    public Answer() { }

    public void AddAnswerOption(string answer) {
        modifyAnswers.Add(answer);
    }
    
    public void AddAnswerOption(string answer, int index) {
        if (index >= modifyAnswers.Count)
            modifyAnswers.Insert(modifyAnswers.Count, answer);
        else
            modifyAnswers.Insert(index, answer);
    }
    
    public bool TryDeleteAnswerOption(int index) {
        if (0 <= index && index < modifyAnswers.Count()) {
            modifyAnswers.RemoveAt(index);
            return true;
        }
        return false;
    }
}