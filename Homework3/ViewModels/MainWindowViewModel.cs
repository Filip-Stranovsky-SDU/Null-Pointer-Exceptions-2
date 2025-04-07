using System;
using System.Collections.ObjectModel;
using System.Linq;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using System.Reactive;
using ReactiveUI;
using homework3_livecharts.Models;
using System.IO;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Collections.Generic;




namespace homework3_livecharts.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private ObservableCollection<string> _buttons;
    public ObservableCollection<string> Buttons
    {
        get => _buttons;
        set => _buttons = value;
    }

    public ReactiveCommand<string, Unit> ButtonClickCommand { get; private set;}
    
    public ReactiveCommand<Unit, Unit> UndoButtonCommand { get; private set;}    
    public ReactiveCommand<Unit, Unit> RedoButtonCommand { get; private set;}

    public ObservableCollection<Sale> Sales { get; set; }

    private ObservableCollection<ChartViewModel> _charts;
    public ObservableCollection<ChartViewModel> Charts
    {
        get => _charts;
        set => this.RaiseAndSetIfChanged(ref _charts, value);
    }
    private Stack<KeyValuePair<bool, ChartViewModel>> undoStack = new();
    
    private Stack<KeyValuePair<bool, ChartViewModel>> redoStack = new();
    
    
    public MainWindowViewModel()
    {        
        ButtonClickCommand = ReactiveCommand.Create<string>(OnButtonClick);
        
        RedoButtonCommand = ReactiveCommand.Create(RedoButton);
        UndoButtonCommand = ReactiveCommand.Create(UndoButton);
        
        Buttons = new ObservableCollection<string> { "Most Sales", "XBox 360 Sales", "Sales pre 2000", "Sales by Year", "Nintendo Sales" };
        //Charts = new();
        
        ChartData cd = ChartCreator.GetBarChartData(s => true);
        
        Charts = new();
        
    }

    private void OnButtonClick(string buttonText)
    {
        Expression<Func<Sale, bool>> filter;
        ChartData cd;
        switch (buttonText)
        {
            case "Most Sales":
                filter = (s => true); // No query
                cd = ChartCreator.GetBarChartData(filter);
                break;

            case "XBox 360 Sales":
                filter = (s => s.Platform == "X360"); // XBox 360 games only
                cd = ChartCreator.GetBarChartData(filter);
                break;

            case "Sales pre 2000":
                filter = (s => Regex.IsMatch(s.Year, @"^\d+$") && int.Parse(s.Year) < 2000); // Games released before year 2000
                cd = ChartCreator.GetBarChartData(filter);
                break;

            case "Sales by Year":
                filter = (s => true);
                cd = ChartCreator.GetLineChartData(filter);
                break;

            case "Nintendo Sales":
                filter = (s => s.Publisher == "Nintendo"); //Games from Nintendo
                cd = ChartCreator.GetLineChartData(filter);
                break;
            default:
                filter = (s => true);
                cd = ChartCreator.GetBarChartData(filter);
                break;
        }
        ChartViewModel temp = new ChartViewModel(cd.ChartSeries, cd.XAxes, cd.YAxes, this);
        undoStack.Push(new(true, temp));
        redoStack.Clear();
        Charts.Add(temp);


    }
    public void DeleteChart(ChartViewModel chart) 
    {
        undoStack.Push(new(false, chart));
        redoStack.Clear();
        Charts.Remove(chart);
    }

    private void UndoButton()
    {
        if (undoStack.Count==0) return;
        if (undoStack.Peek().Key) Charts.Remove(undoStack.Peek().Value);
        if (!undoStack.Peek().Key) Charts.Add(undoStack.Peek().Value);
        redoStack.Push(undoStack.Peek());
        undoStack.Pop();
    }
    private void RedoButton()
    {
        if (redoStack.Count==0) return;
        if (!redoStack.Peek().Key) Charts.Remove(redoStack.Peek().Value);
        if (redoStack.Peek().Key) Charts.Add(redoStack.Peek().Value);
        undoStack.Push(redoStack.Peek());
        redoStack.Pop();
    }


}