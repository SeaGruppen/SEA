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
using System.Threading;

namespace scivu.ViewModels;





public class VersionViewModel : ViewModelBase
{
    private IReadOnlySurvey _survey;
    private readonly Action<string, object> _handleCommand;
    public VersionViewModel(IReadOnlySurvey survey, Action<string, object> handleCommand)
    {
        _survey = survey;
        _handleCommand = handleCommand;

    }


    public string SurveyName => _survey.SurveyName;
    public string SurveyID => _survey.SurveyId;

    public IReadOnlySurvey Survey => _survey;

    public void ModifyCommand(){
        _handleCommand("modify", Survey);
    }

    public void CopyCommand(){
        _handleCommand("copy",Survey);
    }

    public void DeleteCommand() {
        _handleCommand("delete", Survey);
    }

}






