﻿using System;
using System.Diagnostics;
using Model.FrontEndAPI;
using ReactiveUI;
using scivu.Model;

namespace scivu.ViewModels;

public class MainMenuViewModel : ViewModelBase
{
    private const int PinCodeLength = 6;

    private readonly Action<string, object> _changeViewCommand;
    private readonly IFrontEndMainMenu _client;

    private string? _username;
    private string? _password;

    private string _errorMessage = string.Empty;

    private bool _isExperimenterLogin;
    private bool _isSuperLogin;

    private bool _isCreateUser;
    private bool _isLoginEnabled;

    public MainMenuViewModel(Action<string, object> changeViewCommand, IFrontEndMainMenu client)
    {
        _isExperimenterLogin = false;
        _isSuperLogin = false;

        _changeViewCommand = changeViewCommand;
        _client = client;
    }

    public bool IsLoginEnabled
    {
        get => _isLoginEnabled;
        set => this.RaiseAndSetIfChanged(ref _isLoginEnabled, value);
    }

    public string ErrorMessage
    {
        get => _errorMessage;
        private set => this.RaiseAndSetIfChanged(ref _errorMessage, value);
    }

    public string? Username
    {
        get => _username;
        set
        {
            this.RaiseAndSetIfChanged(ref _username, value);
            IsLoginEnabled = EnableLoginButton();
        }
    }

    public string? Password
    {
        get => _password;
        set
        {
            this.RaiseAndSetIfChanged(ref _password, value);
            IsLoginEnabled = EnableLoginButton();
        }
    }

    public bool IsExperimenterLogin
    {
        get => _isExperimenterLogin;
        private set => this.RaiseAndSetIfChanged(ref _isExperimenterLogin, value);
    }

    public bool IsSuperLogin
    {
        get => _isSuperLogin;
        private set => this.RaiseAndSetIfChanged(ref _isSuperLogin, value);
    }

    public bool IsCreateUser
    {
        get => _isCreateUser;
        private set => this.RaiseAndSetIfChanged(ref _isCreateUser, value);
    }

    public bool IsLogin => IsExperimenterLogin || IsSuperLogin;
    public bool IsInputting => IsExperimenterLogin || IsSuperLogin || IsCreateUser;

    public async void ImportSurvey()
    {
        var file = await FileExplorer.OpenSurveyAsync();
        if (file != null)
        {
            var path = file.Path.AbsolutePath.ToString();;
            if (_client.ImportSurveyWrapper(path))
            {
                return;
            }

            var stdmsg = ErrorDiagnostics.GetErrorMessage(ErrorDiagnosticsID.WAR_CouldNotImportSurvey);
            ErrorMessage = $"{stdmsg}: '{path}'";
            return;
        }

        ErrorMessage = ErrorDiagnostics.GetErrorMessage(ErrorDiagnosticsID.WAR_InvalidSurveyFileType);
    }

    public void GoToLogin(bool isSuperUser)
    {
        if (isSuperUser) IsSuperLogin = true;
        else IsExperimenterLogin = true;

        ErrorMessage = string.Empty;

        // Need to raise that fields under IsLogin has changed!
        this.RaisePropertyChanged(nameof(IsLogin));
        this.RaisePropertyChanged(nameof(IsInputting));
    }

    public void GoToCreateUser()
    {
        IsCreateUser = true;
        ErrorMessage = string.Empty;

        // Need to raise that fields under IsInputting has changed!
        this.RaisePropertyChanged(nameof(IsInputting));
        this.RaisePropertyChanged(nameof(IsCreateUser));
    }

    public void GoBackToMenu()
    {
        IsSuperLogin = false;
        IsExperimenterLogin = false;
        IsCreateUser = false;

        Username = null;
        Password = null;
        ErrorMessage = string.Empty;

        // Need to raise that fields under IsLogin has changed!
        this.RaisePropertyChanged(nameof(IsLogin));
        this.RaisePropertyChanged(nameof(IsInputting));
    }

    public void DoLogin()
    {
        if (IsSuperLogin) DoSuperUserLogin();
        else DoExperimenterLogin();
    }

    public void DoCreateUser()
    {
        Debug.Assert(IsCreateUser);

        _client.AddSuperUser(Username!, Password!);
        GoBackToMenu();
    }

    private void DoSuperUserLogin()
    {
        Debug.Assert(IsSuperLogin);

        var result = _client.ValidateSuperUser(Username!, Password!);
        if (result != null)
        {
            var tuple = (Username!,Password!,result!);
            _changeViewCommand.Invoke("SuperUserMenu", tuple);
            return;
        }

        ErrorMessage = ErrorDiagnostics.GetErrorMessage(ErrorDiagnosticsID.ERR_InvalidLogin);
    }

    private bool EnableLoginButton() => IsSuperLogin
        ? EnableSuperUserLogin()
        : IsExperimenterLogin && EnableExperimenterLogin();

    private bool EnableSuperUserLogin()
    {
        Debug.Assert(IsSuperLogin);
        return !string.IsNullOrWhiteSpace(Username)
               && !string.IsNullOrWhiteSpace(Password);
    }

    private bool EnableExperimenterLogin()
    {
        Debug.Assert(IsExperimenterLogin);
        return !string.IsNullOrWhiteSpace(Password)
               && Password.Length == PinCodeLength
               && Int32.TryParse(Password, out _);
    }

    private void DoExperimenterLogin()
    {
        Debug.Assert(IsExperimenterLogin);

        if (Int32.TryParse(Password, out var pin))
        {
            var survey = _client.GetSurveyWrapper(pin);
            if (survey != null)
            {
                _changeViewCommand.Invoke("ExperimenterMenu", survey!);
                return;
            }

            ErrorMessage = ErrorDiagnostics.GetErrorMessage(ErrorDiagnosticsID.ERR_PinCodeNotFound);
        }
    }
}