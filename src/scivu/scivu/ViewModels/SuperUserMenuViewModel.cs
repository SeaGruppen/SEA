using System;
using System.Diagnostics;
using System.Reactive.Linq;
using System.Runtime.Versioning;
using System.Threading;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using Model.Survey;
using ReactiveUI;
using System.Windows.Input;
//using scivu.Models



//using frontEndAPI;
using scivu.Views;
using System.Reactive;

namespace scivu.ViewModels;

public class SuperUserMenuViewModel : ViewModelBase
{
    private readonly Action<string, object> _changeViewCommand;

    private string? _searchText;
    private bool _isBusy;

    private bool _visibleCollection;

    public ICommand? SelectSurveyCommand {get;}
    private SurveyViewModel? _selectedSurvey;

    public string? Username {get; private set; }
    public ObservableCollection<SurveyViewModel> AvailableSurveys {get;} = new();
    public ObservableCollection<SurveyViewModel> SearchResults {get;} = new();

    public ReactiveCommand<string, Unit> Handle {get;}

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

    public bool VisibleCollection
    {
        get => _visibleCollection;
        set => this.RaiseAndSetIfChanged(ref _isBusy, value);
    }

    public SuperUserMenuViewModel(Action<string,object> changeViewCommand, List<IModifySurveyWrapper> surveys, string Username)
    {
        //SelectSurveyCommand = ReactiveCommand.Create(() => 
        //{
        //    //execute button code here
        //})
        this.WhenAnyValue(x => x.SearchText)
        .Throttle(TimeSpan.FromMilliseconds(400))
        .ObserveOn(RxApp.MainThreadScheduler)
        .Subscribe(SearchSurveys!);
        _changeViewCommand = changeViewCommand;
        AvailableSurveys.Clear();
        Handle = ReactiveCommand.Create<string>(HandleCommand);
        foreach (var survey in surveys)
        {

            AvailableSurveys.Add(new SurveyViewModel(survey, HandleCommand));
        }
    }

    public void HandleCommand(string cm) => HandleCommand(cm, null);
    public void HandleCommand(string cm, object? arg) {
        switch(cm)
        {
            case "select" when arg is IModifySurveyWrapper wrapper:
                _changeViewCommand("SelectMenu", wrapper);
                break;
            case "delete" when arg is SurveyViewModel vm:
                Delete(vm);
                break;
            case "copy" when arg is SurveyViewModel vm:
                Copy(vm);
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
        if (!string.IsNullOrWhiteSpace(s)){
            foreach (var survey in AvailableSurveys)
            {
                if (survey.SurveyName.StartsWith(s,StringComparison.InvariantCultureIgnoreCase))
                {
                    SearchResults.Add(survey);
                }
            }


        }
        IsBusy = false;
        if(SearchResults.Count == 0) {
            VisibleCollection = true;
        }
    }

    public void ChangeView(string view){
        _changeViewCommand(view, view == "Mainmenu" ? null! : SelectedSurvey.SurveyWrapper);
    }

    public void Delete(SurveyViewModel survey){
        throw new NotImplementedException(); // Delete survey logic
    }

    public void Select(SurveyViewModel survey){
        throw new NotImplementedException(); // this might be able to be changeVeiw
    }

    public void Copy (SurveyViewModel survey){

    }

    public void Export (IModifySurveyWrapper survey){

    }

}