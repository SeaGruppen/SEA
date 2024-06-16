namespace Tests.Backend.Database;

using Avalonia.Win32.DirectX;
using DatabaseClass = Model.DatabaseModule.Database;
using Model.DatabaseModule;
using Model.Survey;
using System.Text.Json;


[TestFixture]
internal class TestDatabaseStoreAndGet
{

    string testDB = ("testDB");
    IDatabase db;

    int id1 = 1;
    int id2 = 2;
    int id3 = 3;
    SurveyWrapper sw1;
    SurveyWrapper sw2;

    string sw1Path;
    string sw2Path;
    string sw1File;
    string sw2File;
    
    [SetUp]
    public void SetUp() {
        db = new DatabaseClass(testDB);
        sw1 = new SurveyWrapper(id1);
        sw2 = new SurveyWrapper(id2);
        sw1Path = Path.Combine(testDB, id1.ToString());
        sw2Path = Path.Combine(testDB, id2.ToString());
        sw1File = Path.Combine(sw1Path, id1 + ".json");
        sw2File = Path.Combine(sw2Path, id2 + ".json");
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
    public void TestDatabaseConstructor()
    {
        string resultsPath = Path.Combine(testDB, "results.csv");
        string creatorDictPath = Path.Combine(testDB, "creatorDict.json"); 

        Assert.Multiple(() =>
        {
            Assert.That(Path.Exists(testDB), Is.True);
            Assert.That(File.Exists(resultsPath), Is.True);
            Assert.That(File.Exists(creatorDictPath), Is.True);
        });
    }


    [Test]
    public void TestStoreSurveyWrapper()
    {

        // arrange
        Assert.That(Path.Exists(sw1Path), Is.False);
        Assert.That(Path.Exists(sw2Path), Is.False);


        // act 
        db.StoreSurveyWrapper(sw1);
        db.StoreSurveyWrapper(sw2);
        

        // assert
        Assert.Multiple(() =>
        {
            Assert.That(Path.Exists(sw1Path), Is.True);
            Assert.That(File.Exists(sw1File), Is.True);
            Assert.That(Path.Exists(sw2Path), Is.True);
            Assert.That(File.Exists(sw2File), Is.True);
        });
    }


    [Test]
    public void TestGetSurveyWrapper()
    {
        
        // arrange
         Assert.Multiple(() =>
        {
            Assert.That(Path.Exists(sw1Path), Is.False);
            Assert.That(Path.Exists(sw2Path), Is.False);
        });

        db.StoreSurveyWrapper(sw1);
        db.StoreSurveyWrapper(sw2);

        Assert.Multiple(() =>
        {
            Assert.That(Path.Exists(sw1Path), Is.True);
            Assert.That(File.Exists(sw1File), Is.True);
            Assert.That(Path.Exists(sw2Path), Is.True);
            Assert.That(File.Exists(sw2File), Is.True);
        });


        // act
        SurveyWrapper? loadedSW1 = db.GetSurveyWrapper(sw1.SurveyWrapperId);
        SurveyWrapper? loadedSW2 = db.GetSurveyWrapper(sw2.SurveyWrapperId);
        SurveyWrapper? nullSurvey = db.GetSurveyWrapper(10);


        // assert
        Assert.Multiple(() =>
        {
            Assert.That(JsonSerializer.Serialize(loadedSW1), Is.EqualTo(JsonSerializer.Serialize(sw1)));
            Assert.That(JsonSerializer.Serialize(loadedSW1), Is.EqualTo(JsonSerializer.Serialize(sw1)));
            Assert.That(nullSurvey, Is.EqualTo(null));
        });
    }
}