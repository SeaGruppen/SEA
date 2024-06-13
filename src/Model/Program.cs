namespace Model;
using Survey;
using Factory;
using FrontEndAPI;
using tmp_Moc;
using UserValidation;
using Result;
using Answer;
using StatisticsModule;

internal class Program {
    private static void Main(string[] args) {
        AskIfDeleteCurrentDatabase();

        ISuperUserValidator superUserValidator = new SuperUserValidator();
        IFrontEndSuperUser SUMenu = FrontEndFactory.CreateSuperUserMenu();
        IFrontEndMainMenu mainMenu = FrontEndFactory.CreateMainMenu();
        IStatistics statisticsModule = FrontEndFactory.CreateStatistics();

        // Check if Sippo exist as super user
        if (!superUserValidator.ValidateSuperUser("Sippo", "123456"))
            // Add Sippo as super user
            System.Console.WriteLine($"Adding Sippo to SuperUsers went well: {mainMenu.AddSuperUser("Sippo", "123456")}");

        // Get surveys from Sippo
        var sipposSurveys = mainMenu.ValidateSuperUser("Sippo", "123456");
        if (sipposSurveys != null) {
            System.Console.WriteLine($"Validate Sippo as SuperUser: {sipposSurveys.Count()}");
        }
        else {
            System.Console.WriteLine("Validation if Sippo as superuser failed");
        }

        // Check how many surveys Sippo has currently
        System.Console.WriteLine("Current number of SurveyWrappers that Sippo has:");
        List<IModifySurveyWrapper>? randomUserSurveyWrappers = SUMenu.GetSurveyWrappersFromSuperUser("Sippo");
        if (randomUserSurveyWrappers != null)
            System.Console.WriteLine($"Sippo has {randomUserSurveyWrappers.Count()} surveyWrappers");
        else
            System.Console.WriteLine("Sippo has no surveyWrappers");

        // System.Console.WriteLine($"Sippo currently has ");
        int TestSurvey = CreateExampleSurvey.CreateSurveyWrapper("Sippo", "Test survey for SEA!");
        System.Console.WriteLine($"New survey created for Sippo to test on!: SurveyWrapperId = {TestSurvey}");
        List<IModifySurveyWrapper>? randomUserSurveyWrappers1 = SUMenu.GetSurveyWrappersFromSuperUser("Sippo");
        if (randomUserSurveyWrappers1 != null)
            System.Console.WriteLine($"Sippo now has {randomUserSurveyWrappers1.Count()} surveyWrappers");
        else
            System.Console.WriteLine("Sippo still have no surveyWrappers");

        // Add results to the TestSurvey
        AddResultsToSurveyWrapper(TestSurvey, 0);
        // Check if the random generated results are not messed up
        System.Console.WriteLine($"Number of questions in TestSurvey {TestSurvey}: {statisticsModule.NumberOfQuestionsInSurvey(TestSurvey.ToString() + ".0")}");
        System.Console.WriteLine($"Number of started surveys in TestSurvey {TestSurvey}: {statisticsModule.StartedSurveysInWrapper(TestSurvey)}");
        System.Console.WriteLine($"Number of finished surveys in TestSurvey {TestSurvey}: {statisticsModule.FinishedSurveysInWrapper(TestSurvey)}");
        System.Console.WriteLine($"AverageCompletionRate in TestSurvey {TestSurvey}: {statisticsModule.AverageCompletionRateSurveyWrapper(TestSurvey)}");
    }

    private static void AskIfDeleteCurrentDatabase() {
        Console.WriteLine("Do you want to delete the old database? (Y)es, (N)o");
        string input = Console.ReadLine();

        if (input.Equals("Y", StringComparison.OrdinalIgnoreCase)) {
            // Clear old database, to avoid conflicts
            string projectPath = Model.Utilities.FileIO.GetProjectPath();

            // delete src\surveyDatabase\ folder
            System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(System.IO.Path.Combine(projectPath, "surveyDatabase"));
            foreach (System.IO.FileInfo file in di.GetFiles()) {
                file.Delete();
            }
            foreach (System.IO.DirectoryInfo dir in di.GetDirectories()) {
                dir.Delete(true);
            }

            Console.WriteLine("Old database deleted.");
        }
        else {
            Console.WriteLine("Old database not deleted.");
        }
    }

    private static void AddResultsToSurveyWrapper(int surveyWrapper, int surveyId) {
        System.Console.WriteLine("Adding results to the TestSurvey");
        IFrontEndExperimenter experimenter = FrontEndFactory.CreateExperimenterMenu();

        Random random = new Random();

        List<string> resultAnswered = ["Hund", "Kat"]; // Random answer to the questions
        for (int k = 0; k < 50; k++) {
            for (int surveyVersion = 0; surveyVersion < 2; surveyVersion++) {
                for (int i = 0; i < 2; i++) {
                    int jtop = random.Next(4);
                    for (int j = 0; j < jtop; j++) {
                        IResult result = FrontEndFactory.CreateResult(surveyWrapper.ToString() + "." + surveyVersion.ToString(), $"{surveyWrapper}.{surveyVersion}.{i}.{j}", AnswerType.Text, k, resultAnswered);
                        experimenter.StoreResultFromQuestion(result);
                    }
                }
            }
        }
    }
}