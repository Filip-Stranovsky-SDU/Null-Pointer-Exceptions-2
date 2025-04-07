using System;
using System.Collections.ObjectModel;
using System.Reactive;
using ReactiveUI;
using homework3_livecharts.Models;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
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
    public MainWindowViewModel()
    {        
        ButtonClickCommand = ReactiveCommand.Create<string>(OnButtonClick);
        
        RedoButtonCommand = ReactiveCommand.Create(RedoButton);
        UndoButtonCommand = ReactiveCommand.Create(UndoButton);
        
        Buttons = new ObservableCollection<string> { "Most Sales", "XBox 360 Sales", "Sales pre 2000", "Sales by Year", "Nintendo Sales" };
        //Charts = new();
        
        ChartData cd = ChartCreator.GetChartData(s => true);
        
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
                cd = ChartCreator.GetChartData(filter);
                break;

            case "XBox 360 Sales":
                filter = (s => s.Platform == "X360"); // XBox 360 games only
                cd = ChartCreator.GetChartData(filter);
                break;

            case "Sales pre 2000":
                filter = (s => Regex.IsMatch(s.Year, @"^\d+$") && int.Parse(s.Year) < 2000); // Games released before year 2000
                cd = ChartCreator.GetChartData(filter);
                break;

            case "Sales by Year":
                filter = (s => true);
                cd = ChartCreator.CreateLineChart(filter);
                break;

            case "Nintendo Sales":
                filter = (s => s.Publisher == "Nintendo"); //Games from Nintendo
                cd = ChartCreator.CreateLineChart(filter);
                break;
            default:
                filter = (s => true);
                cd = ChartCreator.GetChartData(filter);
                break;
        }
        ChartViewModel temp = new ChartViewModel(cd.ChartSeries, cd.XAxes, cd.YAxes, this);
        
        UndoRedoHandler.AddChart(temp);
        Charts.Add(temp);


    }
    public void DeleteChart(ChartViewModel chart) 
    {
        Charts.Remove(chart);
        UndoRedoHandler.RemoveChart(chart);
    }

    private void UndoButton()
    {
        UndoRedoHandler.Undo(Charts);
    }
    private void RedoButton()
    {
        UndoRedoHandler.Redo(Charts);
    }


}