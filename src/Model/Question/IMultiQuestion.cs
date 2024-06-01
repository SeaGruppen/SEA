namespace Model.Question;

using System;
using System.Collections;
using System.Collections.Generic;


public interface IMultiQuestion<T> : IEnumerable<T> {
    string MultiQuestionId {get;}
    T? AddQuestion();
    void DeleteQuestion(int i);
    void InsertQuestion(int i);
}