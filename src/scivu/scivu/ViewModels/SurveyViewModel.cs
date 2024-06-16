using System;
using Model.Survey;

namespace scivu.ViewModels;

public class SurveyViewModel : ViewModelBase
{
    private IModifySurveyWrapper _surveyWrapper;
    private readonly Action<string, object> _handleCommand;

    public SurveyViewModel(IModifySurveyWrapper survey, Action<string, object> handleCommand)
    {
        _surveyWrapper = survey;
        _handleCommand = handleCommand;

    }


    public string SurveyName => _surveyWrapper.SurveyWrapperName;
    public int SurveyID => _surveyWrapper.SurveyWrapperId;

    public IModifySurveyWrapper SurveyWrapper => _surveyWrapper;

    public void SelectCommand()
    {
        _handleCommand("select", SurveyWrapper);
    }

    public void ExportCommand()
    {
        _handleCommand("export", SurveyWrapper);
    }

    public void DeleteCommand()
    {
        _handleCommand("delete", this);
    }

}






