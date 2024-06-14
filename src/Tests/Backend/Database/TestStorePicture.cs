using Model.Database;
using Model.Survey;
using Model.Answer;
using System.Text.Json;
using Model.Utilities;


[TestFixture]
internal class TestStorePicture {
    string testDB = ("testDB");
    Database db;
    SurveyWrapper surveyWrapper;

    int id = 222222;

    string fileName = "dog.jpg";

    string src;
    string relDest;

    [SetUp]
    public void Setup() {
        db = new Database(testDB);
        src = Path.Combine(FileIO.GetProjectPath(), "..", "assets", fileName);
        relDest = Path.Combine(testDB, id.ToString(), "assets", fileName);
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
    public void TestTryStorePictureWhenPictureNotAlreadyExists() {
        
        // arrange 
        surveyWrapper = new SurveyWrapper(id);
        db.StoreSurveyWrapper(surveyWrapper);
        Assert.That(File.Exists(relDest), Is.False);

        // act
        string actualDest = db.TryStorePicture(surveyWrapper.SurveyWrapperId, src);
        
        // assert
        Assert.Multiple(() => {
            Assert.That(File.Exists(relDest), Is.True);  
            Assert.That(actualDest, Is.EqualTo(relDest));
        });
    }

    [Test]
    public void TestTryStorePictureWhenPictureAlreadyExists() {
        
        // arrange 
        surveyWrapper = new SurveyWrapper(id);
        db.StoreSurveyWrapper(surveyWrapper);
        db.TryStorePicture(surveyWrapper.SurveyWrapperId, src);
        Assert.That(File.Exists(relDest), Is.True);
        DateTime creationTime = File.GetCreationTime(relDest);

        // action & assert
        Exception ex = Assert.Throws<Exception>(() => db.TryStorePicture(surveyWrapper.SurveyWrapperId, src));
        Assert.That("A file with this name already exists for this surveyWrapper", Is.EqualTo(ex.Message));
        Assert.That(creationTime, Is.EqualTo(File.GetCreationTime(relDest)));
    }
}