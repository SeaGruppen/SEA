namespace Tests.Backend.Database;

using Model.StatisticsModule;
using Model.Result;
using Model.Database;
using Model.Factory;
using Model.Answer;
using Model.Survey;
using Model.Question;

[TestFixture]
internal class TestDeleteSurveyWrapper {   
   
   string testDB = ("testDB");
    DatabaseServices database;
    SurveyWrapper surveyWrapper;
    [SetUp]
    public void SetUp() {
        database = new DatabaseServices(testDB);


    }

    [TearDown]
    public  void TearDown() {
        try {
            if (Directory.Exists(testDB)) {
                Directory.Delete(testDB, true);
            }
            else {
            }
        }
        catch (Exception e) {
            Console.WriteLine($"An error occurred: {e.Message}");
        }

    }

    [Test]
    public void TestDeleteSurveyWrapperThatExists() {
        // Arrange
        surveyWrapper = new SurveyWrapper(3);
        database.StoreSurveyWrapper(surveyWrapper);
        Assert.That(database.GetSurveyWrapper(3), Is.Not.Null);
        // Act
        database.DeleteSurveyWrapper(3);
        // Assert
        Assert.That(database.GetSurveyWrapper(3), Is.Null);
    }

    [Test]
    public void TestDeleteSurveyWrapperThatDoesNotExist() {
        // Arrange
        Assert.That(database.GetSurveyWrapper(3), Is.Null);
        // Act
        database.DeleteSurveyWrapper(3);
        // Assert
        Assert.That(database.GetSurveyWrapper(3), Is.Null);
    }
}