using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reactive;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Platform.Storage;
using Homework4.Models;
using Homework4.Views;
using ReactiveUI;

namespace Homework4.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public ReactiveCommand<string, Unit> ButtonClickCommand { get; private set;}
    public ReactiveCommand<Unit, Unit> PauseAllCommand { get; private set; }
    public ReactiveCommand<Unit, Unit> PlayAllCommand { get; private set; }

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
    public ConcurrentBag<string> LoggedTasks { get; set; } = new ConcurrentBag<string>();
    private Data data;
    public Dictionary<string, Recipe> RecipesMap {get; set;} = new();
    
    
    public MainWindowViewModel() {
        ButtonClickCommand = ReactiveCommand.Create<string>(AddTaskClick);
        PauseAllCommand = ReactiveCommand.Create(PauseAll);
        PlayAllCommand = ReactiveCommand.Create(PlayAll);
    }



    private void AddTaskClick(string name) {
        Debug.WriteLine(name);
        Tasks.Add(new(RecipesMap[name], this));
    }

    public async Task LogButtonAsync(Window mainWindow) {
        var topLevel = TopLevel.GetTopLevel(mainWindow);

        var file = await topLevel.StorageProvider.SaveFilePickerAsync(new FilePickerSaveOptions
        {
            Title = "Save Text File",
            FileTypeChoices = [new FilePickerFileType(" ") { Patterns = ["*.csv"] }]
        });

        if (file is not null)
        {
            await using var stream = await file.OpenWriteAsync();
            using var streamWriter = new StreamWriter(stream);
            await streamWriter.WriteLineAsync("StartTime,EndTime,Name");
            foreach(var row in LoggedTasks)
            {
                await streamWriter.WriteLineAsync(row);
            }            
        }
    }

    public async void Load(Window mainWindow)
    {
        var topLevel = TopLevel.GetTopLevel(mainWindow);

        var files = await topLevel.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
        {
            Title = "Select a file",
            AllowMultiple = false,
            FileTypeFilter = [new FilePickerFileType(" ") { Patterns = ["*.json"] }]
        });

        try
        {
            await using var stream = await files[0].OpenReadAsync();
            using var streamReader = new StreamReader(stream);
            data = JsonSerializer.Deserialize<Data>(streamReader.ReadToEnd(), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            LoadHelper();
        }
        catch
        {
            try
            {
                var resource = Assembly.GetExecutingAssembly().GetManifestResourceStream($"{Assembly.GetExecutingAssembly().GetName().Name}.Data.json");
                var t = new StreamReader(resource);
                data = JsonSerializer.Deserialize<Data>(t.ReadToEnd(), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                LoadHelper();                
            }
            catch
            {
                data = JsonSerializer.Deserialize<Data>(DataJson.Data, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                LoadHelper();
            }
        }  
    }

    private void LoadHelper()
    {
        foreach (var xd in data.Recipes)
        {
            RecipesMap[xd.Name] = xd;
            Buttons.Add(xd.Name);
        }

        Tasks.Add(new(RecipesMap.First().Value, this));
    }

    private void PauseAll()
    {
        foreach (TaskViewModel task in Tasks)
        {
            task.IsCooking = false;
            task.IsCookingText = "Play";
        }
    }
    private void PlayAll()
    {
        foreach (TaskViewModel task in Tasks)
        {
            task.IsCooking = true;
            task.IsCookingText = "Pause";
        }
    }
}
