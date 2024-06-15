using System;
using System.Reactive.Linq;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using Model.Survey;
using ReactiveUI;
using System.Reactive;
using scivu.Model;
using Model.FrontEndAPI;

namespace scivu.ViewModels;

public class SuperUserMenuViewModel : ViewModelBase
{
    private readonly Action<string, object> _changeViewCommand;
    private readonly IFrontEndSuperUser _client;

    private string? _searchText;
    private bool _isBusy;

    private bool _visibleCollection;

    private SurveyViewModel? _selectedSurvey;

    public string Username { get; private set; }

    private string _password;
    public ObservableCollection<SurveyViewModel> AvailableSurveys { get; } = new();
    public ObservableCollection<SurveyViewModel> SearchResults { get; } = new();

    public ReactiveCommand<string, Unit> Handle { get; }

    private string _errorMessage = string.Empty;

    public SurveyViewModel? SelectedSurvey
    {
        get => _selectedSurvey;
        set => this.RaiseAndSetIfChanged(ref _selectedSurvey, value);
    }
    public string? SearchText
    {
        get => _searchText;
        set => this.RaiseAndSetIfChanged(ref _searchText, value);
    }

    public bool IsBusy
    {
        get => _isBusy;
        set => this.RaiseAndSetIfChanged(ref _isBusy, value);
    }
    public string ErrorMessage
    {
        get => _errorMessage;
        private set => this.RaiseAndSetIfChanged(ref _errorMessage, value);
    }

    public bool VisibleCollection
    {
        get => _visibleCollection;
        set => this.RaiseAndSetIfChanged(ref _visibleCollection, value);
    }

    public SuperUserMenuViewModel(Action<string, object> changeViewCommand, IFrontEndSuperUser client,
    string username, string password)
    {
        this.WhenAnyValue(x => x.SearchText)
        .Throttle(TimeSpan.FromMilliseconds(400))
        .ObserveOn(RxApp.MainThreadScheduler)
        .Subscribe(SearchSurveys!);
        _changeViewCommand = changeViewCommand;
        _client = client;
        Username = username;
        _visibleCollection = true;
        AvailableSurveys.Clear();
        Handle = ReactiveCommand.Create<string>(HandleCommand);
        _password = password;
        GetSurveys();
    }


    public SuperUserMenuViewModel(Action<string, object> changeViewCommand, IFrontEndSuperUser client,
    string username, string password, List<IModifySurveyWrapper> surveys)
    {
        this.WhenAnyValue(x => x.SearchText)
        .Throttle(TimeSpan.FromMilliseconds(400))
        .ObserveOn(RxApp.MainThreadScheduler)
        .Subscribe(SearchSurveys!);
        _changeViewCommand = changeViewCommand;
        _client = client;
        Username = username;
        _visibleCollection = true;
        AvailableSurveys.Clear();
        Handle = ReactiveCommand.Create<string>(HandleCommand);
        _password = password;
        foreach (var survey in surveys)
        {
            AvailableSurveys.Add(new SurveyViewModel(survey, HandleCommand));
        }

    }

    private void GetSurveys()
    {
        List<IModifySurveyWrapper>? surveys = _client.GetSurveyWrappersFromSuperUser(Username, _password);
        AvailableSurveys.Clear();
        if (surveys != null)
        {
            foreach (var survey in surveys)
            {
                AvailableSurveys.Add(new SurveyViewModel(survey, HandleCommand));
            }
        }
    }




    public void HandleCommand(string cm) => HandleCommand(cm, null);
    public void HandleCommand(string cm, object? arg)
    {
        switch (cm)
        {
            case "select" when arg is IModifySurveyWrapper wrapper:
                Select(wrapper);
                break;
            case "delete" when arg is SurveyViewModel vm:
                Delete(vm);
                break;
            case "export" when arg is IModifySurveyWrapper wrapper:
                Export(wrapper);
                break;
            default:
                throw new ArgumentException($"indvalid com,mand '{cm}', with invalid argument '{arg}'");
        }
    }

    private async void SearchSurveys(string? s)
    {
        IsBusy = true;
        VisibleCollection = false;
        SearchResults.Clear();
        if (!string.IsNullOrWhiteSpace(s))
        {
            foreach (var survey in AvailableSurveys)
            {
                if (survey.SurveyName.StartsWith(s, StringComparison.InvariantCultureIgnoreCase))
                {
                    SearchResults.Add(survey);
                }
            }


        }
        IsBusy = false;
        if (SearchResults.Count == 0)
        {
            VisibleCollection = true;
        }
    }

    public void ChangeView(string view)
    {
        _changeViewCommand(view, null!);
    }

    public void CreateSurvey()
    {
        _client.CreateSurveyWrapper(Username, "temp_name");
        GetSurveys();
    }

    public void Delete(SurveyViewModel survey)
    {
        _client.DeleteSurveyWrapper(survey.SurveyID, Username);
        GetSurveys();
        VisibleCollection = true;
    }

    public void Select(IModifySurveyWrapper surveyWrapper)
    {
        _changeViewCommand("SurveySelectMenu", (surveyWrapper, Username, _password)); // this might be able to be changeVeiw
    }


    public async void Export(IModifySurveyWrapper survey)
    {
        var folder = await FileExplorer.OpenFolderAsync();
        if (folder != null)
        {
            var path = folder.Path.AbsolutePath.ToString();
            if (_client.ExportSurveyWrapperFromDatabase(survey.SurveyWrapperId, path))
            {
                return;
            }
            var stdmsg = ErrorDiagnostics.GetErrorMessage(ErrorDiagnosticsID.WAR_CouldNotExportSurvey);
            ErrorMessage = $"{stdmsg}: id: {survey.SurveyWrapperId}, path: '{path}'";
        }
        ErrorMessage = ErrorDiagnostics.GetErrorMessage(ErrorDiagnosticsID.WAR_InvalidSurveyFileType);
    }

}