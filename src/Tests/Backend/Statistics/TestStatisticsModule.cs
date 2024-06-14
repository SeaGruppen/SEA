namespace Tests.Backend.StatisticsModule;

using Model.StatisticsModule;
using Model.Result;
using Model.Database;
using Model.Factory;
using Model.Answer;
using Model.Survey;
using Model.Question;

[TestFixture]
internal class TestStatisticsModule {
    IStatistics statistics;
    string testDB = ("testDB");
    Database database;
    SurveyWrapper surveyWrapper;
    [SetUp]
    public void SetUp() {
        database = new Database(testDB);
        statistics = new Statistics(database);

        // Creating a base surveyWrapper with 1 survey and 2 questions in it.
        surveyWrapper = new SurveyWrapper(1);
        surveyWrapper.AddNewVersion();
        IModifySurvey survey = surveyWrapper.TryGetModifySurveyVersion(0);
        IMultiQuestion<IModifyQuestion>? mq1 = survey.AddNewMultiQuestion();
        mq1.AddQuestion();
        IMultiQuestion<IModifyQuestion>? mq2 = survey.AddNewMultiQuestion();
        mq2.AddQuestion();
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
    public void TestStartedSurveysInWrapper() {
        // PreArrange Assert
        Assert.AreEqual(0, statistics.StartedSurveysInWrapper(surveyWrapper.SurveyWrapperId));
        
        // Arrange
        for (int i = 0; i < 10; i++) {
            IResult result = FrontEndFactory.CreateResult("1.1", $"1.0.0", AnswerType.Text,i, ["Nothing"]);
            database.StoreResult(result);
        }
        int surveysStarted = statistics.StartedSurveysInWrapper(surveyWrapper.SurveyWrapperId);
        Assert.AreEqual(10, surveysStarted);
    }

    [Test]
    public void TestStartedFinishedInWrapper() {
        // PreArrange Assert
        Assert.AreEqual(0, statistics.FinishedSurveysInWrapper(surveyWrapper.SurveyWrapperId));
        
        // Arrange

        // Create results for 10 users for question 1 and 5 users for question 2
        for (int i = 0; i < 10; i++) {
            IResult result = FrontEndFactory.CreateResult("1.0", $"1.0.0", AnswerType.Text, i, ["Nothing"]);
            database.StoreResult(result);
        }
        for (int i = 0; i < 5; i++) {
            IResult result = FrontEndFactory.CreateResult("1.0", $"1.0.1", AnswerType.Text, i, ["Nothing"]);
            database.StoreResult(result);
        }        
        // Act
        int surveysFinished = statistics.FinishedSurveysInWrapper(surveyWrapper.SurveyWrapperId);

        Assert.AreEqual(5, surveysFinished);
    }   

    [Test]
    public void TestCompletionRateSurveyWrapper() {
        // PreArrange Assert
        Assert.AreEqual(0, statistics.CompletionRateSurveyWrapper(surveyWrapper.SurveyWrapperId));
        
        // Arrange

        // Create results for 10 users for question 1 and 5 users for question 2
        for (int i = 0; i < 10; i++) {
            IResult result = FrontEndFactory.CreateResult("1.0", $"1.0.0", AnswerType.Text, i, ["Nothing"]);
            database.StoreResult(result);
        }
        for (int i = 0; i < 5; i++) {
            IResult result = FrontEndFactory.CreateResult("1.0", $"1.0.1", AnswerType.Text, i, ["Nothing"]);
            database.StoreResult(result);
        }        
        // Act
        double completionRate = statistics.CompletionRateSurveyWrapper(surveyWrapper.SurveyWrapperId);

        Assert.AreEqual(50.0, completionRate);
    }

    [Test]
    public void TestCompletionRateSurvey() {
        // PreArrange Assert
        Assert.AreEqual(0, statistics.CompletionRateSurvey("1.0"));
        
        // Arrange

        // Create results for 10 users for question 1 and 5 users for question 2
        for (int i = 0; i < 10; i++) {
            IResult result = FrontEndFactory.CreateResult("1.0", $"1.0.0", AnswerType.Text, i, ["Nothing"]);
            database.StoreResult(result);
        }
        for (int i = 0; i < 5; i++) {
            IResult result = FrontEndFactory.CreateResult("1.0", $"1.0.1", AnswerType.Text, i, ["Nothing"]);
            database.StoreResult(result);
        }        
        // Act
        double completionRate = statistics.CompletionRateSurvey("1.0");

        Assert.AreEqual(50.0, completionRate);
    }
    [Test]
    public void TestAverageCompletionRateSurveyWrapper() {
        // PreArrange Assert
        Assert.AreEqual(0, statistics.AverageCompletionRateSurveyWrapper(surveyWrapper.SurveyWrapperId));
        
        // Arrange

        // Create results for 10 users for question 1 and 5 users for question 2
        for (int i = 0; i < 10; i++) {
            IResult result = FrontEndFactory.CreateResult("1.0", $"1.0.0", AnswerType.Text, i, ["Nothing"]);
            database.StoreResult(result);
        }
        for (int i = 0; i < 5; i++) {
            IResult result = FrontEndFactory.CreateResult("1.0", $"1.0.1", AnswerType.Text, i, ["Nothing"]);
            database.StoreResult(result);
        }        
        // Act
        double averageCompletionRate = statistics.AverageCompletionRateSurveyWrapper(surveyWrapper.SurveyWrapperId);

        Assert.AreEqual((((0.5 + 1.0) / 2.0) * 100.0), averageCompletionRate);
    }
    [Test]
    public void TestAverageCompletionRateCombined() {
        // PreArrange Assert
        Assert.AreEqual(0, statistics.AverageCompletionRateCombined());
        
        // Arrange

        // Create results for 10 users for question 1 and 5 users for question 2
        for (int i = 0; i < 10; i++) {
            IResult result = FrontEndFactory.CreateResult("1.0", $"1.0.0", AnswerType.Text, i, ["Nothing"]);
            database.StoreResult(result);
        }
        for (int i = 0; i < 5; i++) {
            IResult result = FrontEndFactory.CreateResult("1.0", $"1.0.1", AnswerType.Text, i, ["Nothing"]);
            database.StoreResult(result);
        }        
        // Act
        double averageCompletionRate = statistics.AverageCompletionRateCombined();

        Assert.AreEqual((((0.5 + 1.0) / 2.0) * 100.0), averageCompletionRate);
    }    
}
