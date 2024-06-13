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

    string username;

    string otherUser;

    int id;
    SurveyWrapper surveyWrapper;

    List<SurveyWrapper> surveyWrappers;

    bool res;
    
    [SetUp]
    public void SetUp() {
        database = new DatabaseServices(testDB);
        username = "sippo";
        otherUser = "notsippo";
        id = database.GetNextSurveyWrapperID(username);
        surveyWrapper = new SurveyWrapper(id);
        database.StoreSurveyWrapper(surveyWrapper);
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
    public void TestDeleteSurveyWrapperThatExistsAndIsOwnedByUser() {
        // arrange
        Assert.That(database.GetSurveyWrapper(id), Is.Not.Null);

        // act
        res = database.DeleteSurveyWrapper(id, username);

        // assert
        Assert.That(res, Is.True);
        Assert.That(database.GetSurveyWrapper(id), Is.Null);

    }

    [Test]
    public void TestDeleteSurveyWrapperThatExistsByNonOwnerNotInCreatorDict() {
        // arrange
        surveyWrappers = database.GetSurveyWrapperForSuperUser(otherUser);
        Assert.That(surveyWrappers == null, Is.True);

        // act
        res = database.DeleteSurveyWrapper(id, otherUser);

        // assert
        Assert.That(res, Is.False);
        Assert.That(database.GetSurveyWrapper(id), Is.Not.Null);
    }

    [Test]
    public void TestDeleteSurveyWrapperThatExistsByNonOwnerInCreatorDict() {
        // arrange
        int newid = database.GetNextSurveyWrapperID(otherUser);
        SurveyWrapper newSurveyWrapper = new SurveyWrapper(newid);
        database.StoreSurveyWrapper(newSurveyWrapper);
        surveyWrappers = database.GetSurveyWrapperForSuperUser(otherUser);
        Assert.That(surveyWrappers.Count == 1, Is.True);
        Assert.That(surveyWrappers[0].SurveyWrapperId == newid, Is.True);

        // act
        res = database.DeleteSurveyWrapper(id, otherUser);

        // assert
        Assert.That(res, Is.False);
        surveyWrappers = database.GetSurveyWrapperForSuperUser(otherUser);
        Assert.That(surveyWrappers.Count == 1, Is.True);
        Assert.That(surveyWrappers[0].SurveyWrapperId == newid, Is.True);
        Assert.That(database.GetSurveyWrapper(id), Is.Not.Null);
        Assert.That(database.GetSurveyWrapper(newid), Is.Not.Null);
    }

    
    [Test]
    public void TestDeleteSurveyWrapperThatDoesNotExists() {
        // arrange
        int newid = 1234567;
        Assert.That(database.GetSurveyWrapper(newid), Is.Null);

        // act
        res = database.DeleteSurveyWrapper(newid, username);

        // assert
        Assert.That(res, Is.False);
        Assert.That(database.GetSurveyWrapper(newid), Is.Null);
    }

    [Test]
    public void TestDeleteSurveyWrapperCorrectlyUpdatesCreatorDictWhenOwnedByUser() {
        // arrange
        surveyWrappers = database.GetSurveyWrapperForSuperUser(username);
        Assert.That(surveyWrappers.Count == 1, Is.True);
        Assert.That(surveyWrappers[0].SurveyWrapperId == id, Is.True);

        // act
        res = database.DeleteSurveyWrapper(id, username);
        
        // assert
        Assert.That(res, Is.True);
        surveyWrappers = database.GetSurveyWrapperForSuperUser(username);
        Assert.That(surveyWrappers.Count == 0, Is.True);
    }

    [Test]
    public void TestDeleteSurveyWrapperNotUpdatesOwnersCreatorDictWhenNotOwnedByUser() {
        // arrange 
        surveyWrappers = database.GetSurveyWrapperForSuperUser(username);
        Assert.That(surveyWrappers.Count == 1, Is.True);
        Assert.That(surveyWrappers[0].SurveyWrapperId == id, Is.True);

        // act 
        res = database.DeleteSurveyWrapper(id, otherUser);
        
        // assert
        Assert.That(res, Is.False);
        surveyWrappers = database.GetSurveyWrapperForSuperUser(username);
        Assert.That(surveyWrappers.Count == 1, Is.True);
    }


    // [Test]
    // public void TestDeleteSurveyWrapperThatExists() {
    //     // Arrange
    //     surveyWrapper = new SurveyWrapper(3);
    //     database.StoreSurveyWrapper(surveyWrapper);
    //     Assert.That(database.GetSurveyWrapper(3), Is.Not.Null);
    //     // Act
    //     database.DeleteSurveyWrapper(3);
    //     // Assert
    //     Assert.That(database.GetSurveyWrapper(3), Is.Null);
    // }

    // [Test]
    // public void TestDeleteSurveyWrapperThatDoesNotExist() {
    //     // Arrange
    //     Assert.That(database.GetSurveyWrapper(3), Is.Null);
    //     // Act
    //     database.DeleteSurveyWrapper(3);
    //     // Assert
    //     Assert.That(database.GetSurveyWrapper(3), Is.Null);
    // }
}