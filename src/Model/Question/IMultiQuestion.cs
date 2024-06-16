namespace Model.Question;
using System.Collections.Generic;
using System.Text.Json.Serialization;


public interface IMultiQuestion<T> : IEnumerable<T> {
    /// <summary>
    /// Id of question
    /// </summary>
    [JsonInclude]
    string MultiQuestionId {get;}

    /// <summary>
    /// Adds question of given type
    /// </summary>
    T AddQuestion();

    /// <summary>
    /// Deletes question with given index
    /// </summary>
    /// <param name="i"> Index of question to delete</param>
    void DeleteQuestion(int i);

    /// <summary>
    /// Inserts a question at the given index
    /// </summary>
    /// <param name="i"> Index to add question </param>
    void InsertQuestion(int i);
}