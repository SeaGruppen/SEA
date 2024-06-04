namespace Model.StatisticsModule;

using System;
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

    public int NumberOfQuestionsInSurvey(string surveyId) {
        // To be implemented
        return 10;
    }    
    public int StartedSurveysInWrapper(int surveyWrapperId) {
        List<Result> surveyWrapperResults = databaseServices.GetSurveyWrapperResults(surveyWrapperId);
        List<int> userIds =[];
        for (int i = 0; i < surveyWrapperResults.Count; i++) {
            if (userIds.Contains(surveyWrapperResults[i].UserId)) {
                userIds.Add(surveyWrapperResults[i].UserId);
            }
        }
        return userIds.Count;
    }

    public int FinishedSurveysInWrappers(int surveyWrapperId) {
        int result = 0;
        List<Survey> surveys = GetSurveysInSurveyWrapper(surveyWrapperId);
        foreach (Survey survey in surveys) {
            result += FinishedSurveys(survey.SurveyId);
        }
        return result;
    }

    private int FinishedSurveys(string surveyId) {
        List<Result> surveyReults = GetSurveyResultsFromDatabase(surveyId);
        Dictionary<int, int> questionsAnsweredPrUser = QuestionsAnsweredPrUser(surveyReults);
        int totalQuestions = NumberOfQuestionsInSurvey(surveyId);
        int result = 0;
        foreach (KeyValuePair<int, int> entry in questionsAnsweredPrUser) {
            if (entry.Value >= totalQuestions) {
                result++;
            }
        }
        return result;
    }
    public double CompletionRateSurveyWrapper(int surveyWrapperId) {
        int startedSurveys = StartedSurveysInWrapper(surveyWrapperId);
        int completedSurveys = FinishedSurveysInWrappers(surveyWrapperId);
        // Calculate completion rate
        double result = completedSurveys * 100 / startedSurveys;
        return result;
    }

    public double CompletionRateSurvey(string surveyId) {
        int startedSurveys = StartedSurveys(surveyId);
        int completedSurveys = FinishedSurveys(surveyId);
        double result = completedSurveys * 100 / startedSurveys;
        return result;
    }


    // Needs to be changed, it aggregates the average of the average, but it will give a result for now.
    public double AverageCompletionRateCombined() {
        // Create dictionary of all folder names in the database
        List<int> surveyWrappersInDB =  databaseServices.GetAllSurveyWrapperIds();

        Dictionary<string, double> surveyWrapperCompletionRate = new Dictionary<string, double>();
        for (int i = 0; i < surveyWrappersInDB.Count; i++) {
            surveyWrapperCompletionRate[surveyWrappersInDB[i].ToString()] = CompletionRateSurveyWrapper(surveyWrappersInDB[i]);
        }
        double result = surveyWrapperCompletionRate.Values.Average();
        return result;
    }

    public double AverageCompletionRateSurveyWrapper(int surveyWrapperId) {
        SurveyWrapper? surveyWrapper = databaseServices.GetSurveyWrapper(surveyWrapperId);
        if (surveyWrapper == null) return 0; // SurveyWrapper not found

        Dictionary<string, double> surveyCompletionRate = new Dictionary<string, double>();
        for (int i = 0; i < surveyWrapper.GetVersionCount(); i++) {
            string surveyId = surveyWrapper.TryGetModifySurveyVersion(i).SurveyId.ToString();
            surveyCompletionRate[surveyId] = AverageCompletionRateSurvey(surveyId);
        }
        double result = surveyCompletionRate.Values.Average();
        return result;
    }

    // Helper function to get AverageCompletion for a single survey
    private double AverageCompletionRateSurvey(string surveyId) {
        int totalQuestions = NumberOfQuestionsInSurvey(surveyId);
        List<Result> resultsFromSurvey = GetSurveyResultsFromDatabase(surveyId);
        Dictionary<int, int> questionsAnsweredPrUser = QuestionsAnsweredPrUser(resultsFromSurvey);
        return AverageCompletionRate(totalQuestions, questionsAnsweredPrUser);
    }

    private double AverageCompletionRate(int numberOfQuestions, Dictionary<int, int> questionsAnsweredPrUser) {
        Dictionary<int, double> completionRatePrUser = new Dictionary<int, double>();
        // Count number of users who have answered all questions
        foreach (KeyValuePair<int, int> entry in questionsAnsweredPrUser) {
            completionRatePrUser[entry.Key] = entry.Value / numberOfQuestions;
        }
        double result = completionRatePrUser.Values.Average() * 100;
        return result;
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

    private static Dictionary<int, int> QuestionsAnsweredPrUser(List<Result> surveyWrapperResults) {
        Dictionary<int, int> questionsAnsweredPrUser = new Dictionary<int, int>();
        // Create Dictionary of userIds and number of questions answered
        for (int i = 0; i < surveyWrapperResults.Count; i++) {
            if (questionsAnsweredPrUser.ContainsKey(surveyWrapperResults[i].UserId)) {
                questionsAnsweredPrUser[surveyWrapperResults[i].UserId]++;
            } else {
                questionsAnsweredPrUser[surveyWrapperResults[i].UserId] = 1;
            }
        }

        return questionsAnsweredPrUser;
    }

    private List<Survey> GetSurveysInSurveyWrapper(int surveyWrapperId) {
        SurveyWrapper? surveyWrapper = databaseServices.GetSurveyWrapper(surveyWrapperId);   
        List<Survey> result = new List<Survey>();
        for (int i = 0; i < surveyWrapper.GetVersionCount(); i++) {
            result.Add((Survey) surveyWrapper.TryGetModifySurveyVersion(i));
        }
        return result;
    }

    /// Helper function to get surveys started for a single survey, inside a SurveyWrapper
    private int StartedSurveys(string surveyId) {
        List<Result> surveyWrapperResults = GetSurveyResultsFromDatabase(surveyId);
        List<int> userIds =[];
        for (int i = 0; i < surveyWrapperResults.Count; i++) {
            if (userIds.Contains(surveyWrapperResults[i].UserId)) {
                userIds.Add(surveyWrapperResults[i].UserId);
            }
        }
        return userIds.Count;
    }
}