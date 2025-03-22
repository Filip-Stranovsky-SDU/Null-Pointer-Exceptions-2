using System.Collections.ObjectModel;
using System.Windows.Input;
using Homework2.Classes;

namespace Homework2.ViewModels;
using ReactiveUI;

public class EnrollViewModel : ViewModelBase
{
    private User loggedInUser;
    public User LoggedInUser
    {
        get => loggedInUser;
        set => this.RaiseAndSetIfChanged(ref loggedInUser, value);
    }
    
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
    
    public ICommand EnrollCommand { get; }

    public EnrollViewModel(User user)
    {
        Subjects = new ObservableCollection<Subject>();
        LoggedInUser = user;
        this.MySubjects();
        EnrollCommand = new CommandHandler(Enroll);
    }

    public void MySubjects()
    {
        foreach (var e in SubjectHandler.GetSubjects())
        {
            if (!(e.Value.StudentsEnrolled.Contains(loggedInUser.Id)))
            {
                Subjects.Add(e.Value);
            }
        }
    }

    public void Enroll()
    {
        if (SelectedSubject != null && LoggedInUser != null)
        {
            SubjectHandler.StudentEnroll(loggedInUser, SelectedSubject);
        }
    }
}