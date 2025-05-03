using System;
using System.Collections.Concurrent;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reactive;
using System.Threading;
using System.Threading.Tasks;
using Avalonia.Metadata;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using Homework4.Models;
using ReactiveUI;
using Tmds.DBus.Protocol;

namespace Homework4.ViewModels;

public partial class TaskViewModel : ViewModelBase
{
    public bool IsCooking { get; set; }
    public Recipe Recipe { get; set; }
    private MainWindowViewModel main;
    [ObservableProperty]
    private string isCookingText;
    [ObservableProperty]
    private double progress;
    [ObservableProperty]
    private string text;
    public ObservableCollection<double> Progresses { get; set; } = new();
    public ObservableCollection<string> SubTasks { get; set; } = new();
    public ReactiveCommand<Unit, Unit> PauseCommand { get; private set; }
    public ReactiveCommand<Unit, Unit> RemoveCommand { get; private set; }

    public TaskViewModel(Recipe r, MainWindowViewModel main) {
        Recipe = r;
        IsCooking = true;
        Text = r.Name;
        IsCookingText = "Pause";
        this.main = main;
        PauseCommand = ReactiveCommand.Create(Pause);
        RemoveCommand = ReactiveCommand.Create(Remove);

        foreach (var xd in r.Steps) {
            Progresses.Add(0);
            SubTasks.Add(xd.Step);
        }
        
        Task task = Task.Run(async () =>
        {
            DateTime dateTime = DateTime.Now;
            await Cook();
            main.LoggedTasks.Add($"{dateTime},{Recipe}");
        });
    }
    
    public async Task Cook()
    {
        double total = Recipe.Steps.Sum(s => s.Duration);
        for (int i = 0; i < Recipe.Steps.Count; i++)
        {
            StepClass step = Recipe.Steps[i];
            while (Progresses[i] < 100)
            {
                Thread.Sleep(100);
                if (!IsCooking) continue;
                await Dispatcher.UIThread.InvokeAsync(() =>
                {
                    Progresses[i] += 10 / step.Duration;
                    Progresses[i] = Math.Min(Progresses[i], 100);
                    Progress += 10 / total;
                    Progress = Math.Min(Progress, 100);
                });
            }
        }
    }

    private void Pause()
    {
        IsCooking = !IsCooking;
        if (IsCooking) IsCookingText = "Pause";
        if (!IsCooking) IsCookingText = "Play";
    }

    private void Remove()
    {
        main.Tasks.Remove(this);
    }
}

