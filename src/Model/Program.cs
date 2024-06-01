// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello again, World!");

using Model;
using Model.Survey;
using Model.Question;
using Model.Answer;
using Model.Result;
using Model.Factory;
using Model.FrontEndAPI;

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
question1.ModifyAnswer.AddAnswerOption("Hund");
question1.ModifyAnswer.AddAnswerOption("Kat");
        // Add 3 questions to MultiQuestion[1]
    // Edit MultiQuestion[1]
        // Add 2 questions to MultiQuestion1
IMultiQuestion<IModifyQuestion> multiQuestion2 = version_test.AddNewMultiQuestion();
System.Console.WriteLine($"MultiQuestionId2 = {multiQuestion2.MultiQuestionId}");

IModifyQuestion question2 = multiQuestion2.AddQuestion();
System.Console.WriteLine($"QuestionId2 = {question2.QuestionId}");
question2.ModifyCaption = "Hvilket dyr tror du det er?";
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
question3.ModifyAnswer.AddAnswerOption("Hund");
question3.ModifyAnswer.AddAnswerOption("Kat");

IMultiQuestion<IModifyQuestion> multiQuestion4 = version_test2.AddNewMultiQuestion();
System.Console.WriteLine($"MultiQuestionId4 = {multiQuestion4.MultiQuestionId}");

IModifyQuestion question4 = multiQuestion4.AddQuestion();
System.Console.WriteLine($"QuestionId4 = {question4.QuestionId}");
question4.ModifyCaption = "Hvilket dyr tror du det er?";
question4.ModifyAnswer.AddAnswerOption("Hund");
question4.ModifyAnswer.AddAnswerOption("Kat");

    
        // Edit MultiQuestion[1]
            // Add 3 questions to MultiQuestion[1]    
        // AddMultiQuestion       (multiquestion 2)
            // Add 2 questions to MultiQuestion2
//StoreSurvey

IFrontEndSuperUser db = FrontEndFactory.CreateSuperUserMenu();
db.StoreSurveyInDatabase(surveyWrapper);