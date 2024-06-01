namespace Model.Question;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.Json.Serialization;


internal class MultiQuestion : IMultiQuestion<IModifyQuestion>, IMultiQuestion<IReadOnlyQuestion> {

    [JsonInclude]
    private string multiquestionId;
    [JsonInclude]
    internal List<Question> questions;
    
    [JsonInclude]
    private int nextQuestionId = 0;
    internal int NextQuestionId {get => nextQuestionId; set => nextQuestionId = value;}
    [JsonInclude]    
    public string MultiQuestionId => multiquestionId;
    internal MultiQuestion(string multiquestionId) {
        this.multiquestionId = multiquestionId;
        questions = new List<Question>();
    }

    public void DeleteQuestion(int i) {
        if (i >= 0 && i < questions.Count) {
            questions.RemoveAt(i);
        }
    }

    public void InsertQuestion(int i) {
        if (i >= 0 && i < questions.Count) {
            string questionId = string.Concat(multiquestionId, "-", nextQuestionId++);
            questions.Insert(i, new Question(questionId));
        }
    }

    IEnumerator IEnumerable.GetEnumerator() {
        return questions.GetEnumerator();
    }

    IModifyQuestion? IMultiQuestion<IModifyQuestion>.AddQuestion() {
        string questionId = string.Concat(multiquestionId, ".", nextQuestionId++);
        Question result = new Question(questionId);
        questions.Add(result);
        return result;
    }

    IEnumerator<IModifyQuestion> IEnumerable<IModifyQuestion>.GetEnumerator() {
        return questions.GetEnumerator();
    }

    IReadOnlyQuestion? IMultiQuestion<IReadOnlyQuestion>.AddQuestion() {
        return null;
    }

    IEnumerator<IReadOnlyQuestion> IEnumerable<IReadOnlyQuestion>.GetEnumerator() {
        return questions.GetEnumerator();
    }
}