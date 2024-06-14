using Model.DatabaseModule;
using Model.Survey;
using Model.Answer;
using System.Text.Json;


[TestFixture]
internal class TestGetSurveyWrapperForSuperUser {
    string testDB = ("testDB");
    string creatorDictPath = Path.Combine("testDB", "creatorDict.json");
    IDatabase database;
    SurveyWrapper surveyWrapper;

    [SetUp]
    public void Setup() {
        database = new Database(testDB);
    }

    [TearDown]
    public  void TearDown() {
        try {
            if (Directory.Exists(testDB)) {
                Directory.Delete(testDB, true);
            }
        }
        catch (Exception e) {
            Console.WriteLine($"An error occurred: {e.Message}");
        }

    }


    [Test]
    public void TestGetSurveyWrapperForTwoSuperUsers() {
       string user1 = "sippo";
       string user2 = "oscar";

       int surveyWrapperId1 = database.GetNextSurveyWrapperID(user1);
       surveyWrapper = new SurveyWrapper(surveyWrapperId1);
       database.StoreSurveyWrapper(surveyWrapper);

       int surveyWrapperId2 = database.GetNextSurveyWrapperID(user2);
       surveyWrapper = new SurveyWrapper(surveyWrapperId2);
       database.StoreSurveyWrapper(surveyWrapper);

       var surveyWrappers = database.GetSurveyWrapperForSuperUser(user1);
       Assert.That(surveyWrappers[0].SurveyWrapperId, Is.EqualTo(surveyWrapperId1));

       surveyWrappers = database.GetSurveyWrapperForSuperUser(user2);
       Assert.That(surveyWrappers[0].SurveyWrapperId, Is.EqualTo(surveyWrapperId2));
    }

}