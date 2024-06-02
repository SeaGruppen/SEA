namespace Model.Statistics;

public interface IStatistic {
    /// Number of surveys that have been started to answer but not finished.
    int StartedSurveys(int surveyWrapperId);

    /// Number of surveys that have been fully answered.
    int FinishedSurveys(int surveyWrapperId);

    /// Completion rate for all surveys in a specific SurveyWrapper.
    int CompletionRateSurveyWrapper(int surveyWrapperId);

    /// Completion rate for a specific version of a survey inside a SurveyWrapper.
    /// OBS: SurveyId, not SurveyWrapperId.
    int CompletionRateSurvey(string surveyId);
    /// Average cimpletion rate for all SurveyWrappers.

    ///Average percentage of how much of the survey has been answered
    /// for all SurveyWrappers in the database.
    int AverageCompletionRateCombined();

    ///Average percentage of how much of the survey has been answered for a specific SurveyWrapper.
    int AverageCompletionRateSurveyWrapper(int surveyWrapperId);

    /// Returns the total number of questions in this version of the survey.
    /// OBS: SurveyId, not SurveyWrapperId.
    int NumberOfQuestionsInSurvey(string surveyId);
}