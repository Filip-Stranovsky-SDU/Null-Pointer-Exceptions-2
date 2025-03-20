using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive;
using System.Text.Json;
using System.Windows.Input;
using Avalonia.Threading;
using Homework2.Classes;
using ReactiveUI;

namespace Homework2.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private ReactiveObject currentView;
    public ReactiveObject CurrentView
    {
        get => currentView;
        set => this.RaiseAndSetIfChanged(ref currentView, value);
    }
    
    private bool isPaneOpen;
    public bool IsPaneOpen
    {
        get => isPaneOpen;
        set => this.RaiseAndSetIfChanged(ref isPaneOpen, value);
    }

    private int paneWidth;
    public int PaneWidth
    {
        get => paneWidth;
        set => this.RaiseAndSetIfChanged(ref paneWidth, value);
    }

    private bool isStudent;
    public bool IsStudent
    {
        get => isStudent;
        set => this.RaiseAndSetIfChanged(ref isStudent, value);
    }
    
    private bool isTeacher;
    public bool IsTeacher
    {
        get => isTeacher;
        set => this.RaiseAndSetIfChanged(ref isTeacher, value);
    }
    
    public ICommand PaneCommand { get; }
    public ICommand LoginViewCommand { get; }
    public ICommand StudentViewCommand { get; }
    public ICommand TeacherViewCommand { get; }
    public ICommand EditTeachersCommand { get; }
    public ICommand EnrollStudentsCommand { get; }
    public ICommand SubjectsTeachersCommand { get; }
    public ICommand SubjectsStudentsCommand { get; }

    public MainWindowViewModel()
    {
        
        currentView = new LoginViewModel(this);
        PaneWidth = 0;
        IsPaneOpen = true;
        PaneCommand = new CommandHandler(OpenClosePane);
        StudentViewCommand = new CommandHandler(() => ChangeView(new StudentViewModel()));
        TeacherViewCommand = new CommandHandler(() => ChangeView(new TeacherViewModel()));
        LoginViewCommand = new CommandHandler(() => ChangeView(new LoginViewModel(this)));
        EditTeachersCommand = new CommandHandler(() => ChangeView(new EditViewModel()));
        EnrollStudentsCommand = new CommandHandler(() => ChangeView(new EnrollViewModel()));
    
    }
    
    public void ChangeView(ReactiveObject newView)
    {
        Dispatcher.UIThread.Post(() => CurrentView = newView);
    }
    
    private void OpenClosePane()
    {
        IsPaneOpen = !IsPaneOpen;
    }
}