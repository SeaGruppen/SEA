namespace Model.Question;

using System.Collections;
using System.Collections.Generic;
using System.Text.Json.Serialization;


internal class MultiQuestion : IMultiQuestion<IModifyQuestion>, IMultiQuestion<IReadOnlyQuestion> {

    private string multiquestionId;

    // Added for testing purposes.
    public IReadOnlyList<Question> Questions => questions;

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
        if (i >= 0 && i < questions.Count + 1) {
            string questionId = string.Concat(multiquestionId, ".", nextQuestionId++);
            questions.Insert(i, new Question(questionId));
        }
    }

    IEnumerator IEnumerable.GetEnumerator() {
        return questions.GetEnumerator();
    }

    IModifyQuestion IMultiQuestion<IModifyQuestion>.AddQuestion() {
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

    internal void UpdateId(string newMqId) {
        multiquestionId = newMqId;
        foreach (Question question in questions) {
            string currentQuestionId = question.QuestionId;
            string[] parts = currentQuestionId.Split(".");
            string newQuestionId = multiquestionId + "." + parts.Last();
            question.UpdateId(newQuestionId);
        }
    }

    public IModifyQuestion? ModifyQuestion(int index) {
        if (0 <= index && index < questions.Count) {
            return questions[index];
        } else {
            return null;
        }
    }
    IReadOnlyQuestion? IMultiQuestion<IReadOnlyQuestion>.ModifyQuestion(int i) {
        return null;
    }

}