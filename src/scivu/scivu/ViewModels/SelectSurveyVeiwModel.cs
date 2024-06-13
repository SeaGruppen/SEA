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
using scivu.Model;
using Model.FrontEndAPI;
using DynamicData.Kernel;
using System.Linq;


namespace scivu.ViewModels;

public class SelectSurveyViewModel : ViewModelBase
{
    private readonly Action<string, object> _changeViewCommand;


    private VersionViewModel? _selectedSurvey;

    public string Username {get; private set; }

    private string _password;
    public ObservableCollection<VersionViewModel> AvailableVersions {get;} = new();

    public ReactiveCommand<string, Unit> Handle {get;}

    private string _errorMessage = string.Empty;

    private IModifySurveyWrapper _surveyWrapper;

    public VersionViewModel? SelectedSurvey
    {
        get => _selectedSurvey;
        set => this.RaiseAndSetIfChanged(ref _selectedSurvey, value);
    }
        public string ErrorMessage
    {
        get => _errorMessage;
        private set => this.RaiseAndSetIfChanged(ref _errorMessage, value);
    }


    

    public SelectSurveyViewModel(Action<string,object> changeViewCommand, IModifySurveyWrapper surveyWrapper,
    string username, string password)
    {
        //SelectSurveyCommand = ReactiveCommand.Create(() => 
        //{
        //    //execute button code here
        //})
        _changeViewCommand = changeViewCommand;
        Username = username;
        AvailableVersions.Clear();
        Handle = ReactiveCommand.Create<string>(HandleCommand);
        _password = password;
        _surveyWrapper = surveyWrapper;
        foreach (var survey in surveyWrapper.SurveyVersions)
            {
                AvailableVersions.Add(new VersionViewModel(survey, HandleCommand));
            }

    }

    private void GetSurveys(){
        IReadOnlyList<IReadOnlySurvey>? surveys = _surveyWrapper.SurveyVersions;
        AvailableVersions.Clear();
        if (surveys != null)
        {
            foreach (var survey in surveys)
            {
                AvailableVersions.Add(new VersionViewModel(survey, HandleCommand));
            }
        }
    }

    private int FindIdx(IReadOnlySurvey survey){
        var idx = -1;
        for (int i = 0; i < _surveyWrapper.GetVersionCount(); i++){
            var _survey = _surveyWrapper.TryGetModifySurveyVersion(i);
            if (_survey != null){
                if (survey.SurveyId == _survey.SurveyId){
                    break;
                }
            }
        }
        return idx;
    }




    public void HandleCommand(string cm) => HandleCommand(cm, null);
    public void HandleCommand(string cm, object? arg) {
        switch(cm)
        {
            case "copy" when arg is IReadOnlySurvey rs:
                Copy(rs);
                break;

            case "delete" when arg is IReadOnlySurvey rs:
                Delete(rs);
                break;
            case "modify" when arg is IReadOnlySurvey rs:
                Modify(rs);
                break;
            default:
                throw new ArgumentException($"indvalid com,mand '{cm}', with invalid argument '{arg}'");
        }
    }

    public void ChangeView(string view){
        _changeViewCommand(view, null!);
    }

    public void CreateSurvey() {
        _surveyWrapper.AddNewVersion();
        GetSurveys();
    }

    public void Delete(IReadOnlySurvey survey){
        int idx = FindIdx(survey);
        _surveyWrapper.DeleteVersion(idx);
        GetSurveys();
    }

    public void Copy(IReadOnlySurvey survey) {
        int idx = FindIdx(survey);
        _surveyWrapper.CopyVersion(idx);
    }

    public void Modify (IReadOnlySurvey survey) {
        int idx = FindIdx(survey);
        IModifySurvey? ms = _surveyWrapper.TryGetModifySurveyVersion(idx);
        if (ms != null) {
            throw new NotImplementedException();
        }
        throw new NotImplementedException();
    }


}