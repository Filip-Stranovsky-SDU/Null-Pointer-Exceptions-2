using System.Collections.ObjectModel;
using Homework4.Models;

namespace Homework4.ViewModels;

public partial class TaskViewModel : ViewModelBase
{
    public Recipe Recipe{get; set;}
    public int Progress {get; set;}
    public string Text {get; set;}
    public ObservableCollection<int> Progresses { get; set;} =new();

    public ObservableCollection<string> SubTasks {get; set;}=new();
    public TaskViewModel(Recipe r) {
        Recipe = r;

        foreach(var xd in r.Steps) {
            Progresses.Add(0);
            SubTasks.Add(xd.Step);
        }
    }
    

}

