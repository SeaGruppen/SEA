using Model.Result;
using Model.Answer;

namespace Tests.Backend.Result {
    internal class TestResult {
        [TestCase("1", "1.1", Model.Answer.AnswerType.Text, 1, new[] { "A", "B", "C" })]
        [TestCase("1", "2.2", Model.Answer.AnswerType.Scale, 1, new string[] { })]
        [TestCase("4", "2.2", Model.Answer.AnswerType.MultipleChoice, 3, new[] { "ABCD" })]

        public void TestAddQuestion(string surveyId, string QuestionId, AnswerType answerType, int userId, string[] resultsArray)
        {
            List<string> results = new List<string>(resultsArray);
            var result = new Model.Result.Result(surveyId, QuestionId, answerType, userId, results);

            Assert.AreEqual(surveyId, result.SurveyId);
            Assert.AreEqual(QuestionId, result.QuestionId);
            Assert.AreEqual(answerType, result.AnswerType);
            Assert.AreEqual(userId, result.UserId);
            Assert.AreEqual(results, result.QuestionResult);

        }
    }
}
