namespace Model.tmp_Moc;
using Survey;
using Question;
using Answer;
using Factory;
using FrontEndAPI;
using Model.Utilities;
    public class CreateExampleSurvey {

        private static IFrontEndSuperUser superuserMenu = FrontEndFactory.CreateSuperUserMenu();
        // private string projectPath => FileIO.GetProjectPath();
        public static int CreateSurveyWrapper(string superUser) {
            string projectPath = FileIO.GetProjectPath();
            if (projectPath == null) {
                throw new System.IO.DirectoryNotFoundException("Project path not found");
            }
            // Create SurveyWrapper
            IModifySurveyWrapper surveyWrapper =  superuserMenu.CreateSurveyWrapper(superUser);
            
            // Store pictures needed in database
            string dogPicture1 = superuserMenu.StorePicture(surveyWrapper.SurveyWrapperId, Path.Combine(FileIO.GetProjectPath(), "..", "assets","dog_1.jpeg"));
            string dnPicture3 = superuserMenu.StorePicture(surveyWrapper.SurveyWrapperId, Path.Combine(FileIO.GetProjectPath(), "..", "assets","dangernoodle.jpg"));
            string catPicture1 = superuserMenu.StorePicture(surveyWrapper.SurveyWrapperId, Path.Combine(FileIO.GetProjectPath(), "..", "assets","cat.jpg"));
            string turtlePicture = superuserMenu.StorePicture(surveyWrapper.SurveyWrapperId, Path.Combine(FileIO.GetProjectPath(), "..", "assets","turtle.png"));

            // Add a versions to SurveyWrapper (Cat version of the survey)
            IModifySurvey survey1 = surveyWrapper.AddNewVersion();


            // Add first MultiQuestion to survey1
            IMultiQuestion<IModifyQuestion> multiQuestion10 = survey1.AddNewMultiQuestion();
            // Add a single question to MultiQuestion10
            IModifyQuestion question100 =  multiQuestion10.AddQuestion();
            question100.ModifyCaption = "Animal prioritazion Questionaire";
            question100.ModifyText = "Which animal do you prefer?";
            question100.ModifyAnswer.ModifyAnswerType = AnswerType.MultipleChoice;
            question100.ModifyAnswer.AddAnswerOption("Dog");
            question100.ModifyAnswer.AddAnswerOption("Cat");

            // Add second MultiQuestion to survey1
            IMultiQuestion<IModifyQuestion> multiQuestion11 = survey1.AddNewMultiQuestion();
            
            // Add 2 questions to MultiQuestion 2 in Survey 2
            IModifyQuestion question110 =  multiQuestion11.AddQuestion();
            IModifyQuestion question111 =  multiQuestion11.AddQuestion();
            
            question110.ModifyCaption = "Rate this animal";
            question110.ModifyText = "Which animal do you see in this picture?";
            question110.ModifyPicture = catPicture1;
            question110.ModifyAnswer.ModifyAnswerType = AnswerType.MultipleChoice;
            question110.ModifyAnswer.AddAnswerOption("Dog");
            question110.ModifyAnswer.AddAnswerOption("Cat");
            question110.ModifyAnswer.AddAnswerOption("Dangernoodle");
            question111.ModifyText = "How cute is this animal?";
            question111.ModifyAnswer.ModifyAnswerType = AnswerType.Scale;
            question111.ModifyAnswer.AddAnswerOption("1");
            question111.ModifyAnswer.AddAnswerOption("7");


            IMultiQuestion<IModifyQuestion> multiQuestion12 = survey1.AddNewMultiQuestion();
            IModifyQuestion question120 =  multiQuestion12.AddQuestion();
            question120.ModifyCaption = "New breed of cat";
            question120.ModifyPicture = turtlePicture;
            question120.ModifyText = "How likely is this animal to be accepted as a new cat breed?";





            // Create version 2 of the survey (dog version of the survey)
            IModifySurvey survey2 = surveyWrapper.AddNewVersion();
            // Add 3 MultiQuestion to survey2
            IMultiQuestion<IModifyQuestion> multiQuestion20 = survey2.AddNewMultiQuestion();

            // Modify Multiquestion 1, to just contain a single question
            IModifyQuestion question200 =  multiQuestion20.AddQuestion();
            question200.ModifyCaption = "Animal prioritazion Questionaire";
            question200.ModifyText = "Which animal do you prefer?";
            question200.ModifyAnswer.ModifyAnswerType = AnswerType.MultipleChoice;
            question200.ModifyAnswer.AddAnswerOption("Dog");
            question200.ModifyAnswer.AddAnswerOption("Cat");

            IMultiQuestion<IModifyQuestion> multiQuestion21 = survey2.AddNewMultiQuestion();
            
            // Add 2 questions to MultiQuestion 2 in Survey 2
            IModifyQuestion question210 =  multiQuestion21.AddQuestion();
            IModifyQuestion question211 =  multiQuestion21.AddQuestion();
            
            question210.ModifyCaption = "Rate this animal";
            question210.ModifyText = "Which animal do you see in this picture?";
            question210.ModifyPicture = dogPicture1;
            question210.ModifyAnswer.ModifyAnswerType = AnswerType.MultipleChoice;
            question210.ModifyAnswer.AddAnswerOption("Dog");
            question210.ModifyAnswer.AddAnswerOption("Cat");
            question210.ModifyAnswer.AddAnswerOption("Dangernoodle");
            question211.ModifyText = "How cute is this animal?";
            question211.ModifyAnswer.ModifyAnswerType = AnswerType.Scale;
            question211.ModifyAnswer.AddAnswerOption("1");
            question211.ModifyAnswer.AddAnswerOption("7");

            IMultiQuestion<IModifyQuestion> multiQuestion22 = survey2.AddNewMultiQuestion();
            IModifyQuestion question220 =  multiQuestion22.AddQuestion();
            question220.ModifyCaption = "New breed of dog";
            question220.ModifyPicture = dnPicture3;
            question220.ModifyText = "How likely is this animal to be accepted as a new dog breed?";

            // Store the survey in the database
            superuserMenu.StoreSurveyWrapperInDatabase(surveyWrapper);
            return surveyWrapper.SurveyWrapperId;
        }
    }