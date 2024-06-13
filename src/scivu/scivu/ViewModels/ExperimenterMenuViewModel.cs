using System;
using Model.FrontEndAPI;
using Model.Survey;
using Model.StatisticsModule;
using Model.Factory;
using scivu.Model;
using System.Diagnostics;

namespace scivu.ViewModels;

public class ExperimenterMenuViewModel : ViewModelBase
{
    private readonly IReadOnlySurveyWrapper _survey;
    private readonly Action<string, object> _changeViewCommand;
    private IStatistics _statistics;
    // The following are placeholder, should be dynamically pulled from the survey object.
    private readonly IFrontEndExperimenter _client;
    public string SurveyWrapperName { get; }
    public int SurveyWrapperId { get; }
    public int StartedSurveys { get; }
    public int FinishedSurveys { get; }
    public double CompletionRate { get; }
    public double AverageCompletionRate { get; }
    public string ErrorMessage { get; private set; } = string.Empty;

    public ExperimenterMenuViewModel(IFrontEndExperimenter client, IReadOnlySurveyWrapper survey, Action<string, object> changeViewCommand)
    {
        _statistics = FrontEndFactory.CreateStatistics();
        _client = client;
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

    public async void ExportResults()
    {
        var folder = await FileExplorer.OpenFolderAsync();
        if (folder != null)
        {
            var path = folder.Path.AbsolutePath.ToString();
            if (_client.ExportResults(SurveyWrapperId, path))
            {
                return;
            }
            Console.WriteLine(path);
            var stdmsg = ErrorDiagnostics.GetErrorMessage(ErrorDiagnosticsID.WAR_CouldNotImportSurvey);
            ErrorMessage = $"{stdmsg}: '{path}'";
            Debug.Assert(false);
            return;
        }

    }






}