namespace Model.Question;

using System;
using System.Collections;
using System.Collections.Generic;

internal class MultiQuestion : IMultiQuestion<Question> {
    readonly string multiquestionId;
    private List<Question> questions;
    private int nextQuestionId = 0;
    internal MultiQuestion(string multiquestionId) {
        this.multiquestionId = multiquestionId;
        questions = new List<Question>();
        AddQuestion();
    }
    
    public void AddQuestion() {
        string questionId = string.Concat(multiquestionId, ".", nextQuestionId);
        questions.Add(new Question(questionId));
    }

    public void DeleteQuestion(int i) {
        if (i >= 0 && i < questions.Count) {
            questions.RemoveAt(i);
        }
    }

    public void InsertQuestion(int i) {
        if (i >= 0 && i < questions.Count) {
            string questionId = string.Concat(multiquestionId, "-", nextQuestionId);
            questions.Insert(i, new Question(questionId));
        }
    }

    public IEnumerator<Question> GetEnumerator() {
        return questions.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator() {
        return questions.GetEnumerator();
    }
}