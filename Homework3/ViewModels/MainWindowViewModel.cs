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
    public ObservableCollection<ISeries> SalesChart { get; private set;} = new();
    public ObservableCollection<Axis> XAxes { get; private set;} = new() {new Axis()};
    public ObservableCollection<Axis> YAxes { get; private set;} = new() {new Axis()};

    
    public MainWindowViewModel()
    {
        Sales = new ObservableCollection<Sale>();
        Projects = new ObservableCollection<Project>();
        Musics = new ObservableCollection<Music>();
        FoodWastages = new ObservableCollection<FoodWastage>();
        Factors = new ObservableCollection<Factor>();
        ECommerces = new ObservableCollection<ECommerce>();
        Chocolates = new ObservableCollection<Chocolate>();
        this.LoadData();
        //this.CreateCharts();
        
        ButtonClickCommand = ReactiveCommand.Create<string>(OnButtonClick);

        Buttons = new ObservableCollection<string> { "Most Sales", "XBox 360 Sales", "Sales pre 2000", "Sales by Year", "Nintendo Sales" };
        
    }

    private void LoadData()
    {
        var games = DataLoader.LoadData<Sale>(
            Path.Combine(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory)!.Parent!.Parent!.Parent!.FullName, "Assets", "VideoGamesSales.csv"));
        foreach (var game in games)
        {
            Sales.Add(game);
        }
    }
    
    private void CreateBarChart(Expression<Func<Sale, bool>> filter, int count = 10)
    {
        var topSales = Sales
            .AsQueryable()
            .Where(filter)  // Query
            .Where(s => s.Global_Sales > 0) // Ensure valid sales data
            .OrderByDescending(s => s.Global_Sales)
            .Take(count) // Take top 10 games
            .ToList();
        SalesChart.Clear();
        SalesChart.Add(  
            new ColumnSeries<double>
            {
                Values = topSales.Select(s => Convert.ToDouble(s.Global_Sales)).ToArray(),
                Fill = new SolidColorPaint(SKColors.Blue)
            }
        );
        XAxes.Clear();
        XAxes.Add(
            new Axis
            {
                Labels = topSales.Select(s => s.Name).ToArray(),
                LabelsRotation = 30, 
                TextSize = 12,
                Name = "Games"
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
        switch (buttonText)
        {
            case "Most Sales":
                this.CreateBarChart(s => true); // No query
                break;

            case "XBox 360 Sales":
                this.CreateBarChart(s => s.Platform == "X360"); // XBox 360 games only
                break;

            case "Sales pre 2000":
                this.CreateBarChart(s => Regex.IsMatch(s.Year, @"^\d+$") && int.Parse(s.Year) < 2000); // Games released before year 2000
                break;

            case "Sales by Year":
                this.CreateLineChart(s => true);
                break;

            case "Nintendo Sales":
                this.CreateLineChart(s => s.Publisher == "Nintendo"); //Games from Nintendo
                break;
        }

    }
}