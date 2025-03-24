using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Homework2.Classes;
using ReactiveUI;

namespace Homework2.ViewModels;

public class TeacherViewModel : ViewModelBase
{
    private ObservableCollection<Subject> subjects;
    public  ObservableCollection<Subject> Subjects
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
    public ICommand DeleteSubjectsCommand { get; }

    public TeacherViewModel(User user)
    {
        Subjects = new ObservableCollection<Subject>();
        LoggedInUser = user;
        this.MySubjects();
        DeleteSubjectsCommand = new CommandHandler(DeleteSubject);
    }

    public void MySubjects()
    {
        foreach (var e in SubjectHandler.GetSubjects())
        {
            if (int.Parse(e.Value.TeacherId) == (LoggedInUser.Id))
            {
                Subjects.Add(e.Value);
            }
        }
    }

    public void DeleteSubject()
    {
        if (LoggedInUser != null && SelectedSubject != null)
        {
            SubjectHandler.DeleteSubject(SelectedSubject);
        }
        Subjects = new ObservableCollection<Subject>();
        MySubjects();
    }
}