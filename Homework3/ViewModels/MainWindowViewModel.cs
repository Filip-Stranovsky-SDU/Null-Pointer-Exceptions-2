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
    
    public ObservableCollection<Sale> Sales { get; set; }
    public ObservableCollection<Project> Projects { get; set; }
    public ObservableCollection<Music> Musics { get; set; }
    public ObservableCollection<FoodWastage> FoodWastages { get; set; }
    public ObservableCollection<Factor> Factors { get; set; }
    public ObservableCollection<ECommerce> ECommerces { get; set; }
    public ObservableCollection<Chocolate> Chocolates { get; set; }

    private ObservableCollection<ChartViewModel> _charts;
    public ObservableCollection<ChartViewModel> Charts
    {
        get => _charts;
        set => this.RaiseAndSetIfChanged(ref _charts, value);
    }
    
    
    public MainWindowViewModel()
    {
        Sales = new ObservableCollection<Sale>();
        Projects = new ObservableCollection<Project>();
        Musics = new ObservableCollection<Music>();
        FoodWastages = new ObservableCollection<FoodWastage>();
        Factors = new ObservableCollection<Factor>();
        ECommerces = new ObservableCollection<ECommerce>();
        Chocolates = new ObservableCollection<Chocolate>();
        //this.CreateCharts();
        
        ButtonClickCommand = ReactiveCommand.Create<string>(OnButtonClick);
        
        Buttons = new ObservableCollection<string> { "Most Sales", "XBox 360 Sales", "Sales pre 2000", "Sales by Year", "Nintendo Sales" };
        //Charts = new();
        
        ChartData cd = ChartCreator.GetChartData(s => true);
        
        Charts = new()/*{

        (
            new ChartViewModel(cd.ChartSeries, cd.XAxes, cd.YAxes, this)
        ),
        (
            new ChartViewModel(cd.ChartSeries, cd.XAxes, cd.YAxes, this)
        ),
        (
            new ChartViewModel(cd.ChartSeries, cd.XAxes, cd.YAxes, this)
        )
        }*/;
        
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
        Charts.Add(
            new ChartViewModel(cd.ChartSeries, cd.XAxes, cd.YAxes, this)
        );


    }
    public void DeleteChart(ChartViewModel chart) 
    {
        Charts.Remove(chart);
    }


}