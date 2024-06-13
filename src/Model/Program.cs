namespace Model;
using Survey;
using Factory;
using FrontEndAPI;
using tmp_Moc;
using UserValidation;
using Result;
using Answer;
using StatisticsModule;
using Model.Database;

internal class Program {
    private static void Main(string[] args) {
        AskIfDeleteCurrentDatabase();

        ISuperUserValidator superUserValidator = new SuperUserValidator();
        IFrontEndSuperUser SUMenu = FrontEndFactory.CreateSuperUserMenu();
        IFrontEndMainMenu mainMenu = FrontEndFactory.CreateMainMenu();
        IStatistics statisticsModule = FrontEndFactory.CreateStatistics();

        AddSippoToUserCredentials(superUserValidator, mainMenu);
        
        CheckHowManySurveysSippoHave(SUMenu);

        // System.Console.WriteLine($"Sippo currently has ");
        int TestSurvey = CreateExampleSurvey.CreateSurveyWrapper("Sippo", "Test survey for SEA!");

        CheckHowManySurveyWrappersSippoHasNow(SUMenu, TestSurvey);
        
        AddResultsToSurveyWrapper(statisticsModule, TestSurvey, 0);

    }

    private static void CheckHowManySurveyWrappersSippoHasNow(IFrontEndSuperUser SUMenu, int TestSurvey)
    {
        System.Console.WriteLine($"New survey created for Sippo to test on!: SurveyWrapperId = {TestSurvey}");
        List<IModifySurveyWrapper>? randomUserSurveyWrappers1 = SUMenu.GetSurveyWrappersFromSuperUser("Sippo", "123456");
        if (randomUserSurveyWrappers1 != null)
            System.Console.WriteLine($"Sippo now has {randomUserSurveyWrappers1.Count()} surveyWrappers");
        else
            System.Console.WriteLine("Sippo still have no surveyWrappers");

        // Add results to the TestSurvey
    }

    private static void CheckHowManySurveysSippoHave(IFrontEndSuperUser SUMenu)
    {
        // Check how many surveys Sippo has currently
        System.Console.WriteLine("Current number of SurveyWrappers that Sippo has:");
        List<IModifySurveyWrapper>? randomUserSurveyWrappers = SUMenu.GetSurveyWrappersFromSuperUser("Sippo", "123456");
        if (randomUserSurveyWrappers != null)
            System.Console.WriteLine($"Sippo has {randomUserSurveyWrappers.Count()} surveyWrappers");
        else
            System.Console.WriteLine("Sippo has no surveyWrappers");
    }

    private static void AddSippoToUserCredentials(ISuperUserValidator superUserValidator, IFrontEndMainMenu mainMenu)
    {
        // Check if Sippo exist as super user
        if (!superUserValidator.ValidateSuperUser("Sippo", "123456"))
            // Add Sippo as super user
            System.Console.WriteLine($"Adding Sippo to SuperUsers went well: {mainMenu.AddSuperUser("Sippo", "123456")}");
        else {
            System.Console.WriteLine("sippo already exists as superuser");
        }
        // Get surveys from Sippo
        // Validate that logging in to Sippo works
        List<IModifySurveyWrapper>? sipposSurveys = mainMenu.ValidateSuperUser("Sippo", "123456");
        if (sipposSurveys == null)
        {
            System.Console.WriteLine("Validation of Sippo as superuser failed");
        }
        else
        {
            System.Console.WriteLine($"Validate Sippo as SuperUser, Sippo has: {sipposSurveys.Count()} survey");
        }
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

            System.IO.DirectoryInfo di2 = new System.IO.DirectoryInfo(System.IO.Path.Combine(projectPath, "UserCredentials"));
            foreach (System.IO.FileInfo file in di2.GetFiles()) {
                file.Delete();
            }

            Console.WriteLine("Old database and usercredentials deleted deleted.");
        }
        else {
            Console.WriteLine("Old database not deleted.");
        }
    }

    private static void AddResultsToSurveyWrapper(IStatistics statisticsModule, int surveyWrapper, int surveyId) {
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
        // Check if the random generated results are not messed up
        System.Console.WriteLine($"Number of questions in TestSurvey {surveyWrapper}: {statisticsModule.NumberOfQuestionsInSurvey(surveyWrapper.ToString() + ".0")}");
        System.Console.WriteLine($"Number of started surveys in TestSurvey {surveyWrapper}: {statisticsModule.StartedSurveysInWrapper(surveyWrapper)}");
        System.Console.WriteLine($"Number of finished surveys in TestSurvey {surveyWrapper}: {statisticsModule.FinishedSurveysInWrapper(surveyWrapper)}");
        System.Console.WriteLine($"AverageCompletionRate in TestSurvey {surveyWrapper}: {statisticsModule.AverageCompletionRateSurveyWrapper(surveyWrapper)}");
    }
}