// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello again, World!");

using Model.Database;
using Model.Result;
using Model.Answer;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello again, World!");

DatabaseServices db = new DatabaseServices();
List<Result> results = new List<Result>();
for (int i = 0; i < 10; i++) {
    results.Add(new Result(003797, 0037970001 + i, AnswerType.Text , 0001, "Answer" + i.ToString()));
    }

db.StoreResults(results);