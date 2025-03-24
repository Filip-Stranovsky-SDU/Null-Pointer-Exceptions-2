using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Homework2.Classes;
using ReactiveUI;

namespace Homework2.ViewModels;

public class EditViewModel : ViewModelBase
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
    
    private Subject? selectedSubject;
    public Subject? SelectedSubject
    {
        get => selectedSubject;
        set {this.RaiseAndSetIfChanged(ref selectedSubject, value);
            IsSelected = true;
            EditedName = value.Name;
            EditedDescription = value.Description;
        }
    }
    private bool isSelected = false;
    public bool IsSelected {get => isSelected; 
                            set => this.RaiseAndSetIfChanged(ref isSelected, value);}
    private string _editedName;
    private string _editedDescription;

    public string EditedName
    {
        get => _editedName;
        set => this.RaiseAndSetIfChanged(ref _editedName, value);
    }

    public string EditedDescription
    {
        get => _editedDescription;
        set => this.RaiseAndSetIfChanged(ref _editedDescription, value);
    }

    public ICommand EditCommand { get; }
    public ICommand CancelCommand { get; }
    
    public ICommand AddSubjectCommand { get; }


    public EditViewModel(User user)
    {
        Subjects = new ObservableCollection<Subject>();
        LoggedInUser = user;
        this.MySubjects();
        EditCommand = new CommandHandler(Edit);
        CancelCommand = new CommandHandler(Cancel);
        
        AddSubjectCommand = new CommandHandler(AddSubject);
    }

    public void MySubjects()
    {
        Subjects = new ObservableCollection<Subject>();
        foreach (var e in SubjectHandler.GetSubjects())
        {
            if (int.Parse(e.Value.TeacherId) == loggedInUser.Id)
            {
                Subjects.Add(e.Value);
            }
        }
    }


    private void Edit()
    {
        selectedSubject!.Description = EditedDescription;
        selectedSubject!.Name = EditedName;
        SubjectHandler.EditSubject(selectedSubject);
        
        MySubjects();
        IsSelected = false;
        EditedName = string.Empty;
        EditedDescription = string.Empty;
    }

    private void Cancel()
    {
        // Reset the text fields or handle cancel action
        MySubjects();
        IsSelected = false;
        EditedName = string.Empty;
        EditedDescription = string.Empty;
    }

    private void AddSubject()
    {
        var random = new Random();
        SelectedSubject = new(random.Next(20, 9999), "", "", LoggedInUser.Id.ToString(), new());
        
    }
}