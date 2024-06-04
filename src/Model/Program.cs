// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello again, World!");

using Model;
using Model.Survey;
using Model.Question;
using Model.Answer;
using Model.Result;
using Model.Factory;
using Model.FrontEndAPI;
using Model.StatisticsModule;

// lav SurveyWrapper
SurveyWrapper surveyWrapper = new SurveyWrapper(3797);
System.Console.WriteLine($"SuveyWrapperId = {surveyWrapper.SurveyWrapperId}");
    // AddSurvey

IModifySurvey version = surveyWrapper.AddNewVersion();

    // LoadSurvey[1]


IModifySurvey version_test = surveyWrapper.TryGetModifySurveyVersion(0);
System.Console.WriteLine($"SurveyId[0] = {version_test.SurveyId}");    
        // Add 3 questions to MultiQuestion[0]
    // AddMultiQuestion       (multiquestion 2)
    //Add MultiQuestion[1]
IMultiQuestion<IModifyQuestion> multiQuestion1 = version_test.AddNewMultiQuestion();
System.Console.WriteLine($"MultiQuestionId1 = {multiQuestion1.MultiQuestionId}");
// Add question to Multiquestion[0]
IModifyQuestion question1 = multiQuestion1.AddQuestion();

System.Console.WriteLine($"QuestionId1 = {question1.QuestionId}");
question1.ModifyCaption = "Hvilket dyr tror du det er?";
question1.ModifyText = "Dette er spørgsmål 1's tekst";
question1.ModifyAnswer.ModifyAnswerType = AnswerType.MultipleChoice;
question1.ModifyAnswer.AddAnswerOption("Hund");
question1.ModifyAnswer.AddAnswerOption("Kat");
        // Add 3 questions to MultiQuestion[1]
    // Edit MultiQuestion[1]
        // Add 2 questions to MultiQuestion1
IMultiQuestion<IModifyQuestion> multiQuestion2 = version_test.AddNewMultiQuestion();
System.Console.WriteLine($"MultiQuestionId2 = {multiQuestion1.MultiQuestionId}");

IModifyQuestion question2 = multiQuestion1.AddQuestion();
System.Console.WriteLine($"QuestionId2 = {question2.QuestionId}");
question2.ModifyCaption = "Dette er spørgsmål 2's caption";
question2.ModifyText = "Dette er spørgsmål 2's tekst";
question2.ModifyAnswer.ModifyAnswerType = AnswerType.MultipleChoice;
question2.ModifyAnswer.AddAnswerOption("Hund");
question2.ModifyAnswer.AddAnswerOption("Kat");

//Create new servey version
surveyWrapper.AddNewVersion();
    // AddSurvey[2]
IModifySurvey version_test2 = surveyWrapper.TryGetModifySurveyVersion(1);
System.Console.WriteLine($"SurveyId[1] = {version_test2.SurveyId}");    
        // Add 3 questions to MultiQuestion[1]
    // AddMultiQuestion       (multiquestion 2)
    //Add MultiQuestion[1]
IMultiQuestion<IModifyQuestion> multiQuestion3 = version_test2.AddNewMultiQuestion();
System.Console.WriteLine($"MultiQuestionId3 = {multiQuestion3.MultiQuestionId}");
        // Add 3 questions to MultiQuestion[1]
    // Edit MultiQuestion[1]
        // Add 2 questions to MultiQuestion2
IModifyQuestion question3 = multiQuestion3.AddQuestion();
System.Console.WriteLine($"QuestionId3 = {question3.QuestionId}");
question3.ModifyCaption = "Hvilket dyr tror du det er?";
question3.ModifyText = $"Dette er spørgsmål 3's tekst{question3.QuestionId}";
question3.ModifyAnswer.ModifyAnswerType = AnswerType.MultipleChoice;
question3.ModifyAnswer.AddAnswerOption("Hund");
question3.ModifyAnswer.AddAnswerOption("Kat");

IMultiQuestion<IModifyQuestion> multiQuestion4 = version_test2.AddNewMultiQuestion();
System.Console.WriteLine($"MultiQuestionId4 = {multiQuestion4.MultiQuestionId}");

IModifyQuestion question4 = multiQuestion4.AddQuestion();
System.Console.WriteLine($"QuestionId4 = {question4.QuestionId}");
question4.ModifyCaption = "Hvilket dyr tror du det er?";
question4.ModifyText = "Dette er spørgsmål 4's tekst";
question4.ModifyAnswer.AddAnswerOption("Hund");
question4.ModifyAnswer.AddAnswerOption("Kat");

    
        // Edit MultiQuestion[1]
            // Add 3 questions to MultiQuestion[1]    
        // AddMultiQuestion       (multiquestion 2)
            // Add 2 questions to MultiQuestion2

//StoreSurvey

IFrontEndSuperUser SUMenu = FrontEndFactory.CreateSuperUserMenu();
SUMenu.StoreSurveyWrapperInDatabase(surveyWrapper);

//Loading stored surveywrapper
IModifySurveyWrapper storedSurveyWrapper = SUMenu.ModifySurveyWrapper(3797);

Console.WriteLine($"storedSurveyWrapper's version count = {storedSurveyWrapper.GetVersionCount()}");

//adding another version to the stored surveyWrapper after loading it
IModifySurvey new_version = storedSurveyWrapper.AddNewVersion();
IMultiQuestion<IModifyQuestion> new_multiQuestion = new_version.AddNewMultiQuestion();
IModifyQuestion new_question = new_multiQuestion.AddQuestion();
new_question.ModifyCaption = "What's up?";
new_question.ModifyAnswer.AddAnswerOption("Nothing");
new_question.ModifyAnswer.AddAnswerOption("Not much");

Console.WriteLine($"storedSurveyWrapper's version count after adding new version = {storedSurveyWrapper.GetVersionCount()}");

//storing the loaded and modified survey again.
SUMenu.StoreSurveyWrapperInDatabase(storedSurveyWrapper);

//example of survey export and import 
System.Console.WriteLine(SUMenu.ExportSurveyWrapperFromDatabase(surveyWrapper.SurveyWrapperId, Path.Combine( Model.Utilities.FileIO.GetProjectPath(), ".."))); 
IFrontEndMainMenu mainMenu = FrontEndFactory.CreateMainMenu();
System.Console.WriteLine($"Path to import from: {Path.Combine( Model.Utilities.FileIO.GetProjectPath(), "..", "3799.zip")}");
System.Console.WriteLine( mainMenu.ImportSurveyWrapper(Path.Combine( Model.Utilities.FileIO.GetProjectPath(), "..", "3799.zip")));

IStatistics statistics = FrontEndFactory.CreateStatistics();
System.Console.WriteLine(statistics.NumberOfQuestionsInSurvey("3797.0"));

List<string> resultAnswered = new List<string>();
resultAnswered.Add("Hund");
resultAnswered.Add("Kat");

IFrontEndExperimenter experimenter = FrontEndFactory.CreateExperimenterMenu();

Random random = new Random();

for (int k = 0; k < 50; k++) {
    for (int i = 0; i < 5; i++) {
        for (int j = 0; j < random.Next(30); j++) {
            IResult result = FrontEndFactory.CreateResult("3797." + i.ToString(), $"3797.0.{i}.{j}", AnswerType.Text, k, resultAnswered);
            experimenter.StoreResultFromQuestion(result);
        }
    }
}

System.Console.WriteLine($"Number of started surveys in surveywrapper 3797: {statistics.StartedSurveysInWrapper(3797)}");
System.Console.WriteLine($"Completionrate of SurveyWrapper 3797: {statistics.CompletionRateSurveyWrapper(3797)}");
System.Console.WriteLine($"AverageCompletionrate of SurveyWrapper 3797: {statistics.AverageCompletionRateSurveyWrapper(3797)}");
System.Console.WriteLine($"AverageCompletionRate overall: {statistics.AverageCompletionRateCombined()}");
