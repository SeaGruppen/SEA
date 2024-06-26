﻿using System;
using System.Collections.Generic;
using System.Reactive;
using ReactiveUI;
using Model.FrontEndAPI;
using Model.Survey;
using Model.Factory;

namespace scivu.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    internal ViewModelBase _contentViewModel;
    internal SurveyTakeViewModel _surveyTaker;
    private readonly IFrontEndExperimenter _experimenterClient;

    private readonly IFrontEndMainMenu _mainMenuClient;

    private readonly IFrontEndSuperUser _superUserClient;

    public ReactiveCommand<string, Unit> Change { get; }

    public MainWindowViewModel()
    {
        _experimenterClient = FrontEndFactory.CreateExperimenterMenu();

        // We cache our survey taker both to avoid creating a new version
        // at each survey start, but also for the workaround with opening
        // a dialog option
        _surveyTaker = new SurveyTakeViewModel(_experimenterClient, ChangeViewTo);

        //Surveys = new SurveyViewModel();
        Change = ReactiveCommand.Create<string>(ChangeViewTo);

        _mainMenuClient = FrontEndFactory.CreateMainMenu();

        _contentViewModel = new MainMenuViewModel(ChangeViewTo, _mainMenuClient);

        _superUserClient = FrontEndFactory.CreateSuperUserMenu();
    }

    public ViewModelBase ContentViewModel
    {
        get => _contentViewModel;
        private set => this.RaiseAndSetIfChanged(ref _contentViewModel, value);
    }

    public void ChangeViewTo(string vm) => ChangeViewTo(vm, null);

    public void ChangeViewTo(string vm, object? arg)
    {
        Console.WriteLine($"Going to view `{vm}`");
        switch (vm)
        {
            case "TakeSurvey" when arg is IReadOnlySurveyWrapper survey:
                _surveyTaker.StartNewSurvey(survey, _experimenterClient.GetNextUserId());
                ContentViewModel = _surveyTaker;
                break;
            case "ExperimenterMenu" when arg is IReadOnlySurveyWrapper survey:
                ContentViewModel = new ExperimenterMenuViewModel(_experimenterClient, survey, ChangeViewTo);
                break;
            case "MainMenu":
                ContentViewModel = new MainMenuViewModel(ChangeViewTo, _mainMenuClient);
                break;
            case "PauseMenu" when arg is IReadOnlySurveyWrapper survey:
                ContentViewModel = new PauseMenuViewModel(ChangeViewTo, survey);
                break;
            case "SuperUserMenu" when arg is (string username, string password, List<IModifySurveyWrapper> surveys):
                ContentViewModel = new SuperUserMenuViewModel(ChangeViewTo, _superUserClient, username, password, surveys);
                break;
            case "SuperUserMenu" when arg is (string username, string password):
                ContentViewModel = new SuperUserMenuViewModel(ChangeViewTo, _superUserClient, username, password);
                break;
            case "SurveySelectMenu" when arg is (IModifySurveyWrapper surveyWrapper, string username, string password):
                ContentViewModel = new SelectSurveyMenuViewModel(ChangeViewTo, surveyWrapper, username, password);
                break;
            default:
                throw new ArgumentException($"Invalid view model `{vm}` with invalid argument `{arg}`");
        }
    }
}