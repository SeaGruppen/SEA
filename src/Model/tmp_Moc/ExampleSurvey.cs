using Model.Answer;
using Model.Survey;

namespace Model.FrontEndAPI;

using Question = Model.Question.Question;
public static class ExampleSurvey
{
    private static int _questionId = 0;
    private static Question GetScaleQuestion(string caption, string image, string text, int min, int max)
    {
        var question = new Question((_questionId++).ToString());
        question.ModifyCaption = caption;
        question.ModifyPicture = image;
        question.ModifyText = text;
        var answer = question.ModifyAnswer;
        answer.AddAnswerOption(min.ToString());
        answer.AddAnswerOption(max.ToString());
        answer.ModifyAnswerType = AnswerType.Scale;
        return question;
    }

    private static Question GetMultiQuestion(string caption, string image, string text, string[] options)
    {
        var question = new Question((_questionId++).ToString());
        question.ModifyCaption = caption;
        question.ModifyPicture = image;
        question.ModifyText = text;
        var answer = question.ModifyAnswer;
        foreach (var option in options)
        {
            answer.AddAnswerOption(option);
        }
        answer.ModifyAnswerType = AnswerType.MultipleChoice;
        return question;
    }

    private static Question GetTextQuestion(string caption, string image, string text)
    {
        var question = new Question((_questionId++).ToString());
        question.ModifyCaption = caption;
        question.ModifyPicture = image;
        question.ModifyText = text;
        var answer = question.ModifyAnswer;
        answer.ModifyAnswerType = AnswerType.Text;
        return question;
    }

    internal static SurveyWrapper GetSurvey()
    {
        var fullPathToWorkingDirectory = Directory.GetCurrentDirectory();
        var projectRoot = Directory.GetParent(fullPathToWorkingDirectory).Parent.Parent.Parent.Parent.Parent.FullName;
        var q1 = GetScaleQuestion(
            "Question 1",
            string.Empty,
            "How much do you like this survey example?",
            1,
            6);
        var q2 = GetScaleQuestion(
            "Question 2",
            string.Concat(projectRoot, "\\assets\\dog.jpg"),
            "How much do you think this is a dog?",
            1, 5);
        var q3 = GetScaleQuestion(
            string.Empty,
            string.Empty,
            "How much are you enjoying this survey so far?",
            1, 5);
        var q4 = GetMultiQuestion(
            "Question 3",
            string.Concat(projectRoot, "\\assets\\dog2.png"),
            "What animal is this?",
            ["Dog", "Shell dog", "Water dog"]);
        var q5 = GetMultiQuestion(
            "Question 4",
            string.Empty,
            "Did you like dog?",
            ["Yes"]);
        var q6 = GetTextQuestion(
            "Question 5",
            string.Empty,
            "Please elaborate on why you like dog");
        var q7 = GetTextQuestion(
            "Question 6",
            string.Empty,
            "Do you have any other reason for why you like dog?");
        var q8 = GetMultiQuestion(
            "Question 7",
            string.Concat(projectRoot, "\\assets\\dog3.jpg"),
            "Is this a good boy?",
            ["Yes", "Yes"]);




        var surveyWrap = new SurveyWrapper(123456);
        var survey = surveyWrap.AddNewVersion();
        var page1 = survey.AddNewQuestion();
        ((List<Question>)page1).Add(q1);
        ((List<Question>)page1).Add(q2);
        ((List<Question>)page1).Add(q3);

        var page2 = survey.AddNewQuestion();
        ((List<Question>)page2).Add(q4);
        ((List<Question>)page2).Add(q5);
        ((List<Question>)page2).Add(q6);
        ((List<Question>)page2).Add(q7);
        ((List<Question>)page2).Add(q8);

        return surveyWrap;
    }
}