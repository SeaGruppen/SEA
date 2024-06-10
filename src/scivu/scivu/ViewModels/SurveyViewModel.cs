using Avalonia.Controls;
using System;
using Model.FrontEndAPI;
using Model.Survey;


namespace scivu.ViewModels;





public class SurveyViewModel : ViewModelBase
{
    private IModifySurveyWrapper _surveyWrapper;


    public SurveyViewModel(IModifySurveyWrapper survey)
    {
        _surveyWrapper = survey;
    }


    public string SurveyName => _surveyWrapper.SurveyWrapperName;
    public int SurveyID => _surveyWrapper.SurveyWrapperId;

    public IModifySurveyWrapper SurveyWrapper => _surveyWrapper;
}






