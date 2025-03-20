using System.Collections.Generic;
using System.Collections.ObjectModel;
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

    public StudentViewModel(User user)
    {
        Subjects = new ObservableCollection<Subject>();
        this.MySubjects(user);
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
}