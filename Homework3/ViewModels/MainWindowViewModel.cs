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
        ChartData cd = ChartCreator.GetChartData(s => true);
        
        Charts = new(){

        (
            new ChartViewModel(cd.ChartSeries, cd.XAxes, cd.YAxes, this)
        ),
        (
            new ChartViewModel(cd.ChartSeries, cd.XAxes, cd.YAxes, this)
        ),
        (
            new ChartViewModel(cd.ChartSeries, cd.XAxes, cd.YAxes, this)
        )
        };
    }

    

    private void CreateLineChart(Expression<Func<Sale, bool>> filter)
    {
        var years = Sales
            .AsQueryable()
            .Where(filter)
            .Where(s => !string.IsNullOrEmpty(s.Year) && s.Year != "N/A")
            .GroupBy(s => s.Year) //group games by year
            .Select(s => new //assign total sales to each year
            {
                Year = s.Key,
                TotalSales = s.Sum(s => s.Global_Sales)
            })
            .OrderBy(s => s.Year);
        SalesChart.Clear();
        SalesChart.Add(
            new LineSeries<double>
            {
                Values = years.Select(s => Convert.ToDouble(s.TotalSales)).ToArray(),
                Fill = new SolidColorPaint(SKColors.Blue)
            }
        );
        XAxes.Clear();
        XAxes.Add(
            new Axis
            {
                Labels = years.Select(s => s.Year).ToArray(),
                LabelsRotation = 90,
                TextSize = 12,
                Name = "Years"
            }
        );
        YAxes.Clear();
        YAxes.Add(
            new Axis
            {
                Name = "Sales (Millions)",
                TextSize = 12
            }
        );
    }

    private void OnButtonClick(string buttonText)
    {
        Expression<Func<Sale, bool>> filter;
        switch (buttonText)
        {
            case "Most Sales":
                filter = (s => true); // No query
                break;

            case "XBox 360 Sales":
                filter = (s => s.Platform == "X360"); // XBox 360 games only
                break;

            case "Sales pre 2000":
                filter = (s => Regex.IsMatch(s.Year, @"^\d+$") && int.Parse(s.Year) < 2000); // Games released before year 2000
                break;

            case "Sales by Year":
                filter = (s => true);
                break;

            case "Nintendo Sales":
                filter = (s => s.Publisher == "Nintendo"); //Games from Nintendo
                break;
            default:
                filter = (s => true);
                break;
        }
        ChartData cd = ChartCreator.GetChartData(filter);
        Charts.Add(
            new ChartViewModel(cd.ChartSeries, cd.XAxes, cd.YAxes, this)
        );


    }
    public void DeleteChart(ChartViewModel chart) 
    {
        Charts.Remove(chart);
    }


}