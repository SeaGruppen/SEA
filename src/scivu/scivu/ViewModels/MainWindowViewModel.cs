﻿using System;
using System.Reactive;
using ReactiveUI;
using Model.FrontEndAPI;
using Model.Survey;
using Model.Factory;


namespace scivu.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    internal ViewModelBase _contentViewModel;
    private readonly IClientRequest _client;

    private readonly IFrontEndMainMenu _mainMenuClient;

    public SurveyViewModel Surveys { get; }

    public ReactiveCommand<string, Unit> Change { get; }

    public MainWindowViewModel()
    {
        _client = new ClientRequest();

        Surveys = new SurveyViewModel();
        Change = ReactiveCommand.Create<string>(ChangeViewTo);

        _mainMenuClient = FrontEndFactory.CreateMainMenu();

        _contentViewModel = new MainMenuViewModel(ChangeViewTo, _mainMenuClient);
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
                ContentViewModel = new SurveyTakeViewModel(_client, ChangeViewTo, survey, 42);
                break;
            case "ExperimenterMenu" when arg is IReadOnlySurveyWrapper survey:
                ContentViewModel = new ExperimenterMenuViewModel(survey, ChangeViewTo);
                break;
            case "MainMenu":
                ContentViewModel = new MainMenuViewModel(ChangeViewTo);
                break;
            case "PauseMenu" when arg is IReadOnlySurveyWrapper _:
                throw new NotImplementedException("PauseMenu Viewmodel");
            case "SuperUserMenu":
                throw new NotImplementedException("Changing to super user menu");
            default:
                throw new ArgumentException($"Invalid view model `{vm}` with invalid argument `{arg}`");
        }
    }
}