using Avalonia.Controls;
using System;
using Model.FrontEndAPI;
using Model.Survey;
using System.String;
namespace scivu.ViewModels;


private SurveyWrapper _surveyWrapper;


public class SurveyViewModel (SurveyWrapper survey) : ViewModelBase
{
    _surveyWrapper = survey;
}

public string SurveyName => _surveyWrapper.SurveyWrapperName;
public int SuveryID => _surveyWrapper.surveyWrapperId;

public SurveyWrapper SurveyWrapper => _surveyWrapper;




