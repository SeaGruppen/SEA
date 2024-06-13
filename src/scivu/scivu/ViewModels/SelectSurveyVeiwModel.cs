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


    private VersionVeiwModel? _selectedSurvey;

    public string Username {get; private set; }

    private string _password;
    public ObservableCollection<VersionModel> AvailableVersions {get;} = new();

    public ReactiveCommand<string, Unit> Handle {get;}

    private string _errorMessage = string.Empty;

    private IModifySurveyWrapper _surveyWrapper;

    public SurveyViewModel? SelectedSurvey
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
        Username = username;
        AvailableVersions.Clear();
        Handle = ReactiveCommand.Create<string>(HandleCommand);
        _password = password;
        _surveyWrapper = surveyWrapper;
        foreach (var survey in surveyWrapper.)
            {
                AvailableSurveys.Add(new SurveyViewModel(survey, HandleCommand));
            }

    }

    private void GetSurveys(){
        List<IReadOnlySurvey>? surveys = 
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
    public void HandleCommand(string cm, object? arg) {
        switch(cm)
        {
            case "select" when arg is IModifySurveyWrapper wrapper:
                _changeViewCommand("SelectMenu", wrapper);
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

    public void ChangeView(string view){
        _changeViewCommand(view, null!);
    }

    public void CreateSurvey() {
        _surveyWrapper.(Username, "temp_name");
        GetSurveys();
    }

    public void Delete(SurveyViewModel survey){
        _client.DeleteSurveyWrapper(survey.SurveyID, Username);
        GetSurveys();
        VisibleCollection = true;
    }

    public void Select(SurveyViewModel survey){
       _changeViewCommand("SelectMenu", survey.SurveyWrapper); // this might be able to be changeVeiw
    }


    public async void Export (IModifySurveyWrapper survey){
        var folder = await FileExplorer.OpenFolderAsync();
        if (folder != null)
        {
            var path = folder.Path.AbsolutePath.ToString();
            if (_client.ExportSurveyWrapperFromDatabase(survey.SurveyWrapperId,path))
            {
                return;
            }
            var stdmsg = ErrorDiagnostics.GetErrorMessage(ErrorDiagnosticsID.WAR_CouldNotExportSurvey);
            ErrorMessage = $"{stdmsg}: id: {survey.SurveyWrapperId}, path: '{path}'";
        }
        ErrorMessage = ErrorDiagnostics.GetErrorMessage(ErrorDiagnosticsID.WAR_InvalidSurveyFileType);
    }

}