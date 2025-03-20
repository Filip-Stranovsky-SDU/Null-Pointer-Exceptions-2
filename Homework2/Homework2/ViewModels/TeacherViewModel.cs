using System.Collections.ObjectModel;
using System.Linq;
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

    public TeacherViewModel(User user)
    {
        Subjects = new ObservableCollection<Subject>();
        this.MySubjects(user);
    }

    public void MySubjects(User user)
    {
        foreach (var e in SubjectHandler.GetSubjects())
        {
            if (int.Parse(e.Value.TeacherId) == (user.Id))
            {
                Subjects.Add(e.Value);
            }
        }
    }
}