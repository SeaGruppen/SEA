namespace Model.Question;

using System;
using System.Collections;
using System.Collections.Generic;


public interface IMultiQuestion<T> : IEnumerable<T> {
    void AddQuestion();
    void DeleteQuestion(int i);
    void InsertQuestion(int i);
}

