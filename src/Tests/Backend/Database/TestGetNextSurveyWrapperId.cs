namespace Tests.Backend.Database;
using Model.DatabaseModule;
using Model.Survey;
using Model.Answer;
using System.Text.Json;


[TestFixture]
internal class TestGetNextSurveyWrapperId {
    string testDB = ("testDB");
    string creatorDictPath = Path.Combine("testDB", "creatorDict.json");
    IDatabase database;
    SurveyWrapper surveyWrapper;

    [SetUp]
    public void Setup() {
        database = new Database(testDB);
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
    public void TestCreatorDictInitial() {
        Assert.That(File.Exists(creatorDictPath), Is.True);
        string jsonString = File.ReadAllText(creatorDictPath);
        var creatorDict = JsonSerializer.Deserialize<Dictionary<string, List<int>>>(jsonString, Globals.OPTIONS)!;
        Assert.That(creatorDict.Count==0, Is.True);
    }


    [Test]
    public void TestGetNextSurveyWrapperIdFirstTimeUpdate() {
        
        string userName = "sippo";

        int newSurveyWrapperId = database.GetNextSurveyWrapperID(userName);
        string jsonString = File.ReadAllText(creatorDictPath);
        var creatorDict = JsonSerializer.Deserialize<Dictionary<string, List<int>>>(jsonString, Globals.OPTIONS)!;
        Assert.That(creatorDict[userName].SequenceEqual(new List<int> {newSurveyWrapperId}), Is.True);
    }

    [Test]
    public void TestGetNextSurveyWrapperIdUpdatedTwice() {
        
        string userName = "sippo";

        int newSurveyWrapperId1 = database.GetNextSurveyWrapperID(userName);
        int newSurveyWrapperId2 = database.GetNextSurveyWrapperID(userName);
        string jsonString = File.ReadAllText(creatorDictPath);
        var creatorDict = JsonSerializer.Deserialize<Dictionary<string, List<int>>>(jsonString, Globals.OPTIONS)!;
        Assert.That(creatorDict[userName].SequenceEqual(new List<int> {newSurveyWrapperId1, newSurveyWrapperId2}), Is.True);
    }

}