namespace Model.StatisticsModule;

using System;
using System.Collections.Generic;
using Model.Database;
using Model.Result;
using Model.Survey;
using Model.Utilities;
using Model.Question;
internal class Statistics : IStatistics{

    private IDatabase databaseServices;

    internal Statistics(IDatabase databaseServices) {
        this.databaseServices = databaseServices;
    }

    public int NumberOfQuestionsInSurvey(string surveyId) {
        int surveyWrapperId;
        try {
            surveyWrapperId = ExtractSurveyDetails.TryGetSurveyWrapperId(surveyId);
        } catch (ArgumentException) {
            return 0; // Invalid surveyId
        }
        SurveyWrapper? surveyWrapper = databaseServices.GetSurveyWrapper(surveyWrapperId);
        if (surveyWrapper == null) return 0; // SurveyWrapper not found
        Survey? survey = GetSurveyFromSurveyWrapper(surveyWrapper, surveyId);
        // To be implemented
        int result = 0;

        while (survey.NextQuestionExist()) {
            IEnumerable<IReadOnlyQuestion>? multiQuestion = survey.TryGetNextReadOnlyQuestion();
            if (multiQuestion != null) {
                foreach (IReadOnlyQuestion question in multiQuestion) {
                    result++;
                }
            }
        }
        return result;
    }


    public int StartedSurveysInWrapper(int surveyWrapperId) {
        int result = 0;
        List<Survey> surveys = GetSurveysInSurveyWrapper(surveyWrapperId);
        if (surveys == null) return 0; // SurveyWrapper not found
        foreach (Survey survey in surveys) {
            result += StartedSurveys(survey.SurveyId);
        }
        return result;
    }

    public int FinishedSurveysInWrapper(int surveyWrapperId) {
        int result = 0;
        List<Survey> surveys = GetSurveysInSurveyWrapper(surveyWrapperId);
        if (surveys == null) return 0; // SurveyWrapper not found
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
        int completedSurveys = FinishedSurveysInWrapper(surveyWrapperId);
        // Calculate completion rate
        if (startedSurveys == 0) {
            return 0;
        } else {
            return completedSurveys * 100 / startedSurveys;
        }
    }

    public double CompletionRateSurvey(string surveyId) {
        int startedSurveys = StartedSurveys(surveyId);
        if (startedSurveys == 0) return 0;
        int completedSurveys = FinishedSurveys(surveyId);
        double result = completedSurveys * 100 / startedSurveys;
        return result;
    }


    // It aggregates the average of the average, I dont know if that is the desired 'average', but it will give a result for now.
    public double AverageCompletionRateCombined() {
        // Create dictionary of all folder names in the database
        List<int> surveyWrappersInDB =  databaseServices.GetAllSurveyWrapperIds();

        Dictionary<string, double> surveyWrapperCompletionRate = new Dictionary<string, double>();
        for (int i = 0; i < surveyWrappersInDB.Count; i++) {
            surveyWrapperCompletionRate[surveyWrappersInDB[i].ToString()] = AverageCompletionRateSurveyWrapper(surveyWrappersInDB[i]);
        }
        double result = surveyWrapperCompletionRate.Values.Average();
        return result;
    }

    public double AverageCompletionRateSurveyWrapper(int surveyWrapperId) {
        SurveyWrapper? surveyWrapper = databaseServices.GetSurveyWrapper(surveyWrapperId);
        if (surveyWrapper == null) return 0; // SurveyWrapper not found

        Dictionary<string, double> surveyCompletionRate = new Dictionary<string, double>();
        for (int i = 0; i < surveyWrapper.GetVersionCount(); i++) {
            string surveyId = surveyWrapper.TryGetModifySurveyVersion(i).SurveyId;
            var tmpresult = AverageCompletionRateSurvey(surveyId);
            surveyCompletionRate[surveyId] = tmpresult;
        }
        double result = surveyCompletionRate.Values.Average();
        return result;
    }

    // Helper function to get AverageCompletion for a single survey
    private double AverageCompletionRateSurvey(string surveyId) {
        int totalQuestions = NumberOfQuestionsInSurvey(surveyId);
        List<Result> resultsFromSurvey = GetSurveyResultsFromDatabase(surveyId);
        Dictionary<int, int> questionsAnsweredPrUser = QuestionsAnsweredPrUser(resultsFromSurvey);
        foreach (KeyValuePair<int, int> entry in questionsAnsweredPrUser) {
        }
        return AverageCompletionRate(totalQuestions, questionsAnsweredPrUser);
    }

    private double AverageCompletionRate(int numberOfQuestions, Dictionary<int, int> questionsAnsweredPrUser) {
        if (questionsAnsweredPrUser.Count == 0) return 0;
        Dictionary<int, double> completionRatePrUser = new Dictionary<int, double>();
        // Count number of users who have answered all questions
        foreach (KeyValuePair<int, int> entry in questionsAnsweredPrUser) {
            completionRatePrUser[entry.Key] = (double)entry.Value / (double)numberOfQuestions;
        }
        double result = completionRatePrUser.Values.Average() * 100;
        return result;
    }

    // OBS: SurveyId, not SurveyWrapperId.
    private List<Result>? GetSurveyResultsFromDatabase(string surveyId) {
        
        int surveyWrapperId;
        try {
            surveyWrapperId = ExtractSurveyDetails.TryGetSurveyWrapperId(surveyId);
        } catch (ArgumentException) {
            return null; // Invalid SurveyWrapperId
        }
        List<Result> allResults =  databaseServices.GetSurveyWrapperResults(surveyWrapperId);
        List<Result> results = new List<Result>();
        foreach (Result result in allResults) {
            try {
                string surveyIdFromResult = ExtractSurveyDetails.TryGetSurveyId(result.QuestionId);
                if (surveyIdFromResult == surveyId) {
                    results.Add(result);
                } 
            } catch (ArgumentException) {
                // QuestinId is invalid, it doesn't contain a valid surveyId
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

    private List<Survey>? GetSurveysInSurveyWrapper(int surveyWrapperId) {
        SurveyWrapper? surveyWrapper = databaseServices.GetSurveyWrapper(surveyWrapperId);   
        if (surveyWrapper == null) return null; // SurveyWrapper not found
        List<Survey> result = new List<Survey>();
        for (int i = 0; i < surveyWrapper.GetVersionCount(); i++) {
            result.Add((Survey) surveyWrapper.TryGetModifySurveyVersion(i));
        }
        return result;
    }

    /// Helper function to get surveys started for a single survey, inside a SurveyWrapper
    private int StartedSurveys(string surveyId) {
        List<Result>? surveyResults = GetSurveyResultsFromDatabase(surveyId);
        if (surveyResults == null) return 0; // Survey not found
        List<int> userIds =[];
        for (int i = 0; i < surveyResults.Count; i++) {
            if (!userIds.Contains(surveyResults[i].UserId)) {
                userIds.Add(surveyResults[i].UserId);
            }
        }
        return userIds.Count;
    }
    
    private Survey? GetSurveyFromSurveyWrapper(SurveyWrapper surveyWrapper, string surveyId) {
        for (int i = 0; i < surveyWrapper.GetVersionCount(); i++) {
            Survey survey = (Survey) surveyWrapper.TryGetModifySurveyVersion(i);
            if (survey.SurveyId == surveyId) {
                return survey;
            }
        }
        return null;
    }
}