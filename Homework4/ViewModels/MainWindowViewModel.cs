using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reactive;
using System.Reflection;
using System.Text.Json;
using Homework4.Models;
using ReactiveUI;

namespace Homework4.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public ReactiveCommand<string, Unit> ButtonClickCommand { get; private set;}
    public ReactiveCommand<Unit, Unit> LogButtonCommand { get; private set;}

    private ObservableCollection<string> _buttons = new();
    public ObservableCollection<string> Buttons
    {
        get => _buttons;
        set => _buttons = value;
    }
    private ObservableCollection<TaskViewModel> _tasks = new();
    public ObservableCollection<TaskViewModel> Tasks
    {
        get => _tasks;
        set => _tasks = value;
    }
    private Data data;
    public Dictionary<string, Recipe> RecipesMap {get; set;} = new();
    
    
    public MainWindowViewModel() {
        ButtonClickCommand = ReactiveCommand.Create<string>(AddTaskClick);
        LogButtonCommand = ReactiveCommand.Create(LogButton);

        var resource = Assembly.GetExecutingAssembly().GetManifestResourceStream($"{Assembly.GetExecutingAssembly().GetName().Name}.Data.json");
        var t = new StreamReader(resource);
        data = JsonSerializer.Deserialize<Data>(t.ReadToEnd(),  new JsonSerializerOptions(){PropertyNameCaseInsensitive=true});

        foreach(var xd in data.Recipes) {
            RecipesMap[xd.Name] = xd;
            Buttons.Add(xd.Name);
        }

        Tasks.Add(new(RecipesMap.First().Value));
    }



    private void AddTaskClick(string name) {
        Debug.WriteLine(name);
        //Tasks.Add(new(RecipesMap[name]));
    }

    private void LogButton() {

    }
}
