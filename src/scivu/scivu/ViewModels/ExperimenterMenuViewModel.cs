using System;
using Model.FrontEndAPI;
using Model.Survey;
using Model.StatisticsModule;
using Model.Factory;

namespace scivu.ViewModels;

public class ExperimenterMenuViewModel : ViewModelBase
{
    private readonly IReadOnlySurveyWrapper _survey;
    private readonly Action<string, object> _changeViewCommand;
    private IStatistics _statistics;
    // The following are placeholder, should be dynamically pulled from the survey object.
    public string SurveyWrapperName { get; }
    public int SurveyWrapperId { get; }
    public int StartedSurveys { get; }
    public int FinishedSurveys { get; }
    public double CompletionRate { get; }
    public double AverageCompletionRate { get; }

    public ExperimenterMenuViewModel(IReadOnlySurveyWrapper survey, Action<string, object> changeViewCommand)
    {
        _statistics = FrontEndFactory.CreateStatistics();
        _survey = survey;
        _changeViewCommand = changeViewCommand;
        SurveyWrapperName = survey.SurveyWrapperName;
        SurveyWrapperId = survey.SurveyWrapperId;
        StartedSurveys = _statistics.StartedSurveysInWrapper(SurveyWrapperId);
        FinishedSurveys = _statistics.FinishedSurveysInWrapper(SurveyWrapperId);
        CompletionRate = _statistics.CompletionRateSurveyWrapper(SurveyWrapperId);
        AverageCompletionRate = _statistics.AverageCompletionRateSurveyWrapper(SurveyWrapperId);
    }


    public void ChangeView(string view)
    {
        _changeViewCommand(view, view == "MainMenu" ? null! : _survey);

    }

    public void StartSurveyCommand()
    {
        _changeViewCommand("TakeSurvey", _survey);
    }







}