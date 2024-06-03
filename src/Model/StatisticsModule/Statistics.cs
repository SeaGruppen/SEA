namespace Model.StatisticsModule;

using System.Collections.Generic;
using Model.Database;
using Model.Result;
using Model.Survey;
using Model.Utilities;
public class Statistics : IStatistics{

    private IDatabase databaseServices;

    internal Statistics(DatabaseServices databaseServices) {
        this.databaseServices = databaseServices;
    }
    public int StartedSurveys(int surveyWrapperId) {
        List<Result> surveyWrapperResults = GetSurveyWrapperResultsFromDatabase(surveyWrapperId);
        List<int> userIds =[];
        for (int i = 0; i < surveyWrapperResults.Count; i++) {
            if (userIds.Contains(surveyWrapperResults[i].UserId)) {
                userIds.Add(surveyWrapperResults[i].UserId);
            }
        }
        return userIds.Count;
    }

    public int FinishedSurveys(int surveyWrapperId) {
        //Need to create NumberOfQuestionsInSurvey() method to match against it.
        return 0;
    }

    public double CompletionRateSurveyWrapper(int surveyWrapperId) {
        return 0;
    }

    public double CompletionRateSurvey(string surveyId) {
        return 0;
    }

    public double AverageCompletionRateCombined() {
        return 0;
    }

    public double AverageCompletionRateSurveyWrapper(int surveyWrapperId) {
        List<Result> surveyWrapperResults = GetSurveyWrapperResultsFromDatabase(surveyWrapperId);
        Dictionary<int,int> questionsAnsweredPrUser = new Dictionary<int, int>();
        // Create Dictionary of userIds and number of questions answered
        for (int i = 0; i < surveyWrapperResults.Count; i++) {
            if (questionsAnsweredPrUser.ContainsKey(surveyWrapperResults[i].UserId)) {
                questionsAnsweredPrUser[surveyWrapperResults[i].UserId]++;
            } else {
                questionsAnsweredPrUser[surveyWrapperResults[i].UserId] = 1;
            }
        }
        int totalQuestions = NumberOfQuestionsInSurvey(surveyWrapperId.ToString());
        int completedSurveys = 0;
        // Count number of users who have answered all questions
        foreach (KeyValuePair<int, int> entry in questionsAnsweredPrUser) {
            if (entry.Value >= totalQuestions) {
                completedSurveys++;
            }
        }
        // Calculate completion rate
        double result = (completedSurveys * 100) / questionsAnsweredPrUser.Count;
        return result;
    }

    public int NumberOfQuestionsInSurvey(string surveyId) {
        return 10;
    }

    private List<Result> GetSurveyWrapperResultsFromDatabase(int surveyWrapperId) {
        return databaseServices.GetSurveyWrapperResults(surveyWrapperId);
    }

    // OBS: SurveyId, not SurveyWrapperId.
    private List<Result> GetSurveyResultsFromDatabase(string surveyId) {
        int surveyWrapperId = ExtractSurveyDetails.GetSurveyWrapperId(surveyId);
        List<Result> allResults =  databaseServices.GetSurveyWrapperResults(surveyWrapperId);
        List<Result> results = new List<Result>();
        foreach (Result result in allResults) {
            string surveyIdFromResult = ExtractSurveyDetails.GetSurveyId(result.QuestionId);
            if (surveyIdFromResult == surveyId) {
                results.Add(result);
            }
        }
        return results;
    }    
}