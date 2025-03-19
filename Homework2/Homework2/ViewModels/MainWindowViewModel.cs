using System.Collections.Generic;
using System.Reactive;
using System.Windows.Input;
using Avalonia.Threading;
using HeatOptimiser.Classes;
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
    
    public List<Teacher> Teachers { get; set; }
    public List<Student> Students { get; set; }
    
    public ICommand PaneCommand { get; }
    public ICommand LoginViewCommand { get; }
    public ICommand StudentViewCommand { get; }
    public ICommand TeacherViewCommand { get; }

    public MainWindowViewModel()
    {
        currentView = new LoginViewModel(this);
        PaneWidth = 0;
        IsPaneOpen = true;
        PaneCommand = new CommandHandler(OpenClosePane);
        StudentViewCommand = new CommandHandler(() => ChangeView(new StudentViewModel()));
        TeacherViewCommand = new CommandHandler(() => ChangeView(new TeacherViewModel()));
        LoginViewCommand = new CommandHandler(() => ChangeView(new LoginViewModel(this)));
        
        Teachers = new List<Teacher>
        {
            new Teacher(1, "raklo", "teacher1", "password1"),
            new Teacher(2, "fako", "teacher2", "password2")
        };

        Students = new List<Student>
        {
            new Student(1, "Hrusky", "student1", "password1"),
            new Student(2, "Bongo", "student2", "password2")
        };
    }

    public void ChangeView(ReactiveObject newView)
    {
        if (newView is StudentViewModel)
        {
            IsStudent = true;
            PaneWidth = 100;
            Dispatcher.UIThread.Post(() => CurrentView = newView);
        }
        else if (newView is TeacherViewModel)
        {
            IsTeacher = true;
            PaneWidth = 100;
            Dispatcher.UIThread.Post(() => CurrentView = newView);
        }
    }
    
    private void OpenClosePane()
    {
        IsPaneOpen = !IsPaneOpen;
    }
}