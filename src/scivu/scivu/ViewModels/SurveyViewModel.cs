using Avalonia.Controls;
using System;
using Model.FrontEndAPI;
using Model.Survey;
using ReactiveUI;
using System.Reactive.Linq;
using System.Reactive;


using System;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Input;
using Model.Factory;
using Model.FrontEndAPI;
using Model.Question;
using Model.Result;
using Model.Survey;
using ReactiveUI;
using System.Windows.Input;

namespace scivu.ViewModels;





public class SurveyViewModel : ViewModelBase
{
    private IModifySurveyWrapper _surveyWrapper;
    private readonly Action<string, object> _handleCommand;

    
    public ICommand DeleteCommand { get; }


    public SurveyViewModel(IModifySurveyWrapper survey, Action<string, object> handleCommand)
    {
        _surveyWrapper = survey;
        _handleCommand = handleCommand;
        ShowDialog = new Interaction<ExitSurveyViewModel, bool>();
        DeleteCommand = ReactiveCommand.CreateFromTask(async () =>
        {
            var dialog= new ExitSurveyViewModel();
            var result = await ShowDialog.Handle(dialog);
            if (result)
            {
                _handleCommand("delete", this);
            }
        });

    }


    public string SurveyName => _surveyWrapper.SurveyWrapperName;
    public int SurveyID => _surveyWrapper.SurveyWrapperId;
    public Interaction<ExitSurveyViewModel, bool> ShowDialog { get; }

    public IModifySurveyWrapper SurveyWrapper => _surveyWrapper;

    public void SelectCommand(){
        _handleCommand("select", SurveyWrapper);
    }

    public void CopyCommand(){
        _handleCommand("copy",SurveyWrapper);
    }

    public void ExportCommand() {
        _handleCommand("export",SurveyWrapper);
    }

}






