namespace Tests.Backend.Factory;
using Model.Factory;
using Model.Answer;
using Model.FrontEndAPI;

internal class TestFactory {

    [TestCase("1", "1.1", Model.Answer.AnswerType.Text, 1, new[] { "A", "B", "C" })]
    [TestCase("1", "2.2", Model.Answer.AnswerType.Scale, 1, new string[] { })]
    [TestCase("4", "2.2", Model.Answer.AnswerType.MultipleChoice, 3, new[] { "ABCD" })]
    public void TestCreateResult(string surveyId, string QuestionId, AnswerType answerType, int userId, string[] resultsArray) {
        // Arrange
        // Convert resultsArray to List<string>
        List<string> results = new List<string>(resultsArray);

        // Act
        // Create result using the factory
        var result = FrontEndFactory.CreateResult(surveyId, QuestionId, answerType, userId, results);

        // Assert that the result was created correctly
        Assert.AreEqual(surveyId, result.SurveyId);
        Assert.AreEqual(QuestionId, result.QuestionId);
        Assert.AreEqual(answerType, result.AnswerType);
        Assert.AreEqual(userId, result.UserId);
        Assert.AreEqual(results, result.QuestionResult);
    }

    [Test]
    public void TestCreaMainMenu() {
        // Arrange
        
        // Act
        // Create main menu using the factory
        IFrontEndMainMenu mainMenu = FrontEndFactory.CreateMainMenu();

        // Assert that the main menu was created correctly
        Assert.IsNotNull(mainMenu);
    }
}