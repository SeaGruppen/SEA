using Avalonia.Win32.DirectX;
using DatabaseClass = Model.DatabaseModule.Database;
using Model.DatabaseModule;
using Model.Survey;
using System.Text.Json;

namespace Tests.Backend.Database;


public class TestDatabaseServices
{

    [Test]
    public void TestDatabaseServicesConstructor()
    {
        string testDbPath = "surveyDatabase/";
        string resultsPath = Path.Combine(testDbPath, "./results.csv");
        IDatabase db = new DatabaseClass(testDbPath); 
        

        Assert.Multiple(() =>
        {
            Assert.That(Path.Exists(testDbPath), Is.True);
            Assert.That(File.Exists(resultsPath), Is.True);
        });
        
        // Directory.Delete(testDbPath, true);

        // Assert.That(Path.Exists(testDbPath), Is.False);
    }


    [Test]
    public void TestStoreSurveyWrapper()
    {
        string testDbPath = "surveyDatabase/";
        IDatabase db = new DatabaseClass(testDbPath); 

        int id1 = 1111;
        int id2 = 2222;
        SurveyWrapper surveyWrapper1 = new(id1);
        string surveyWrapper1Path = Path.Combine(testDbPath, id1.ToString());
        string surveyWrapper1File = Path.Combine(surveyWrapper1Path, id1 + ".json");
       
        SurveyWrapper surveyWrapper2 = new(id2);
        string surveyWrapper2Path = Path.Combine(testDbPath, id2.ToString());
        string surveyWrapper2File = Path.Combine(surveyWrapper2Path, id2 + ".json");

        Assert.That(Path.Exists(surveyWrapper1Path), Is.False);
        Assert.That(Path.Exists(surveyWrapper2Path), Is.False);

        db.StoreSurveyWrapper(surveyWrapper1);
        db.StoreSurveyWrapper(surveyWrapper2);
        

        Assert.Multiple(() =>
        {
            Assert.That(Path.Exists(surveyWrapper1Path), Is.True);
            Assert.That(File.Exists(surveyWrapper1File), Is.True);
            Assert.That(Path.Exists(surveyWrapper2Path), Is.True);
            Assert.That(File.Exists(surveyWrapper2File), Is.True);
        });
        
        Directory.Delete(testDbPath, true);
        Assert.That(Path.Exists(testDbPath), Is.False);
    }


    [Test]
    public void TestGetSurveyWrapper()
    {
        string testDbPath = "surveyDatabase/";
        IDatabase db = new DatabaseClass(testDbPath); 

        int id3 = 3333;
        int id4 = 4444;
        int id5 = 5555;
        SurveyWrapper surveyWrapper3 = new(id3);
        string surveyWrapper3Path = Path.Combine(testDbPath, id3.ToString());
        string surveyWrapper3File = Path.Combine(surveyWrapper3Path, id3 +".json");
       
        SurveyWrapper surveyWrapper4 = new(id4);
        string surveyWrapper4Path = Path.Combine(testDbPath, id4.ToString());
        string surveyWrapper4File = Path.Combine(surveyWrapper4Path, id4 + ".json");

         Assert.Multiple(() =>
        {
            Assert.That(Path.Exists(surveyWrapper3Path), Is.False);
            Assert.That(Path.Exists(surveyWrapper4Path), Is.False);
        });
        db.StoreSurveyWrapper(surveyWrapper3);
        db.StoreSurveyWrapper(surveyWrapper4);

        Assert.Multiple(() =>
        {
            Assert.That(Path.Exists(surveyWrapper3Path), Is.True);
            Assert.That(File.Exists(surveyWrapper3File), Is.True);
            Assert.That(Path.Exists(surveyWrapper4Path), Is.True);
            Assert.That(File.Exists(surveyWrapper4File), Is.True);
        });

        SurveyWrapper? loadedSurveyWrapper3 = db.GetSurveyWrapper(surveyWrapper3.SurveyWrapperId);
        SurveyWrapper? loadedSurveyWrapper4 = db.GetSurveyWrapper(surveyWrapper4.SurveyWrapperId);
        SurveyWrapper? nullSurvey = db.GetSurveyWrapper(id5);

        Assert.Multiple(() =>
        {
            Assert.That(JsonSerializer.Serialize(loadedSurveyWrapper3), Is.EqualTo(JsonSerializer.Serialize(surveyWrapper3)));
            Assert.That(JsonSerializer.Serialize(loadedSurveyWrapper4), Is.EqualTo(JsonSerializer.Serialize(surveyWrapper4)));
            Assert.That(nullSurvey, Is.EqualTo(null));
        });
        
        Directory.Delete(testDbPath, true);
        Assert.That(Path.Exists(testDbPath), Is.False);
    }

    [Test]
    public void TestStorePicture() {

        string testDbPath = "surveyDatabase";
        IDatabase db = new DatabaseClass(testDbPath); 
        
        int id1 = 6666;
        SurveyWrapper surveyWrapper1 = new(id1);

        string fileName = "dog.jpg";
        string projectPath =  Model.Utilities.FileIO.GetProjectPath() ;
        string src = Path.Combine(projectPath, "..", "assets", fileName);
        string dest = Path.Combine("surveyDatabase", id1.ToString(), "assets", fileName);   

        db.StoreSurveyWrapper(surveyWrapper1);
        Assert.That(File.Exists(dest), Is.False);  
        string returnedDest = db.TryStorePicture(surveyWrapper1.SurveyWrapperId, src);
        
        Assert.Multiple(() => {
            Assert.That(File.Exists(dest), Is.True);  
            Assert.That(returnedDest, Is.EqualTo(dest));
        });
        
        Directory.Delete(testDbPath, true);
        Assert.That(Path.Exists(testDbPath), Is.False);

    }
}