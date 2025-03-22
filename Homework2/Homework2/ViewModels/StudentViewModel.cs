using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Homework2.Classes;
namespace Homework2.ViewModels;
using ReactiveUI;

public class StudentViewModel : ViewModelBase
{
    private ObservableCollection<Subject> subjects;
    public ObservableCollection<Subject> Subjects
    {
        get => subjects;
        set => this.RaiseAndSetIfChanged(ref subjects, value);
    }
    
    private Subject selectedSubject;
    public Subject SelectedSubject
    {
        get => selectedSubject;
        set => this.RaiseAndSetIfChanged(ref selectedSubject, value);
    }
    
    private User loggedInUser;
    public User LoggedInUser
    {
        get => loggedInUser;
        set => this.RaiseAndSetIfChanged(ref loggedInUser, value);
    }
    
    public ICommand DropSubjectsCommand { get; }

    public StudentViewModel(User user)
    {
        Subjects = new ObservableCollection<Subject>();
        this.MySubjects(user);
        LoggedInUser = user;
        DropSubjectsCommand = new CommandHandler(DropSubject);
    }
    
    public void MySubjects(User user)
    {
        foreach (var e in SubjectHandler.GetSubjects())
        {
            if (e.Value.StudentsEnrolled.Contains(user.Id))
            {
                Subjects.Add(e.Value);
            }
        }
    }

    public void DropSubject()
    {
        if (LoggedInUser != null && SelectedSubject != null)
        {
            SubjectHandler.StudentDrop(LoggedInUser, SelectedSubject);
        }
    }
}