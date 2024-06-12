namespace Model.Question;

using System.Collections.Generic;
using System.Text.Json.Serialization;


public interface IMultiQuestion<T> : IEnumerable<T> {
    [JsonInclude]
    string MultiQuestionId {get;}
    T? AddQuestion();
    void DeleteQuestion(int i);
    void InsertQuestion(int i);
}