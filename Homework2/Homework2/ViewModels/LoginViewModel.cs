using System;
using System.Reactive;
using System.Windows.Input;
using HeatOptimiser.Classes;
using ReactiveUI;

namespace Homework2.ViewModels;

public class LoginViewModel : ViewModelBase
{
    private string username;
    public string Username
    {
        get => username;
        set => this.RaiseAndSetIfChanged(ref username, value);
    }
    
    private string password;
    public string Password
    {
        get => password;
        set => this.RaiseAndSetIfChanged(ref password, value);
    }
    
    private bool revealPassword;
    public bool RevealPassword
    {
        get => revealPassword;
        set => this.RaiseAndSetIfChanged(ref revealPassword, value);
    }
    
    private bool wrongLogin;
    public bool WrongLogin
    {
        get => wrongLogin;
        set => this.RaiseAndSetIfChanged(ref wrongLogin, value);
    }
    
    private MainWindowViewModel mainWindowViewModel;
    
    public ICommand RevealPasswordCommand { get; }
    public ICommand CheckTheLoginCommand { get; }

    public LoginViewModel(MainWindowViewModel mainWindowViewModel)
    {
        WrongLogin = false;
        this.mainWindowViewModel = mainWindowViewModel;
        RevealPassword = false;
        RevealPasswordCommand = new CommandHandler(ShowhidePassword);
        CheckTheLoginCommand = new CommandHandler(CheckTheLogin);
    }

    public void ShowhidePassword()
    {
        RevealPassword = !RevealPassword;
    }

    public void CheckTheLogin()
    {
        foreach (var student in mainWindowViewModel.Students)
        {
            if (student.LoginCheck(Username, Password))
            {
                mainWindowViewModel.ChangeView(new StudentViewModel());
            }
        }

        foreach (var teacher in mainWindowViewModel.Teachers)
        {
            if (teacher.LoginCheck(Username, Password))
            {
                mainWindowViewModel.ChangeView(new TeacherViewModel());
            }
        }
        
        WrongLogin = true;
    }
}