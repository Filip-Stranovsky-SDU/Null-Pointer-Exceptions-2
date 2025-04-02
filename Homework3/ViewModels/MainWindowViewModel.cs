using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using HarfBuzzSharp;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;

namespace homework3_livecharts.ViewModels;
using ReactiveUI;
using homework3_livecharts.Models;
using System.IO;

public class MainWindowViewModel : ViewModelBase
{
    private bool chocolate;
    public bool Chocolate
    {
        get => chocolate;
        set => this.RaiseAndSetIfChanged(ref chocolate, value);
    }

    private bool eCommerce;
    public bool ECommerce
    {
        get => eCommerce;
        set => this.RaiseAndSetIfChanged(ref eCommerce, value);
    }

    private bool globalFoodWastage;
    public bool GlobalFoodWastage
    {
        get => globalFoodWastage;
        set => this.RaiseAndSetIfChanged(ref globalFoodWastage, value);
    }

    private bool globalMusicStreaming;
    public bool GlobalMusicStreaming
    {
        get => globalMusicStreaming;
        set => this.RaiseAndSetIfChanged(ref globalMusicStreaming, value);
    }

    private bool kicksterter;
    public bool Kicksterter
    {
        get => kicksterter;
        set => this.RaiseAndSetIfChanged(ref kicksterter, value);
    }

    private bool studentPerformance;
    public bool StudentPerformance
    {
        get => studentPerformance;
        set => this.RaiseAndSetIfChanged(ref studentPerformance, value);
    }
    
    private bool videoGames;
    public bool VideoGames
    {
        get => videoGames;
        set => this.RaiseAndSetIfChanged(ref videoGames, value);
    }

    public ObservableCollection<Sale> Sales { get; set; }
    public ObservableCollection<Project> Projects { get; set; }
    public ObservableCollection<Music> Musics { get; set; }
    public ObservableCollection<FoodWastage> FoodWastages { get; set; }
    public ObservableCollection<Factor> Factors { get; set; }
    public ObservableCollection<ECommerce> ECommerces { get; set; }
    public ObservableCollection<Cholocate> Cholocates { get; set; }
    
    public ISeries[] SalesChart { get; set; }
    public Axis[] XAxes { get; set; }
    public Axis[] YAxes { get; set; }
    
    public MainWindowViewModel()
    {
        Chocolate = false;
        ECommerce = false;
        GlobalFoodWastage = false;
        GlobalMusicStreaming = false;
        Kicksterter = false;
        StudentPerformance = false;
        VideoGames = false;
        Sales = new ObservableCollection<Sale>();
        Projects = new ObservableCollection<Project>();
        Musics = new ObservableCollection<Music>();
        FoodWastages = new ObservableCollection<FoodWastage>();
        Factors = new ObservableCollection<Factor>();
        ECommerces = new ObservableCollection<ECommerce>();
        Cholocates = new ObservableCollection<Cholocate>();
        this.LoadData();
        this.CreateCharts();
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
    
    private void CreateCharts()
    {
        var topSales = Sales
            .Where(s => s.Global_Sales > 0)  // Filter out invalid values
            .OrderByDescending(s => s.Global_Sales)
            .Take(10) // Take top 10 games
            .ToList();

        SalesChart = new ISeries[]
        {
            new ColumnSeries<double>
            {
                Values = topSales.Select(s => Convert.ToDouble(s.Global_Sales)).ToArray(),
                Fill = new SolidColorPaint(SKColors.Blue)
            }
        };

        XAxes = new[]
        {
            new Axis
            {
                Labels = topSales.Select(s => s.Name).ToArray(),
                LabelsRotation = 30, 
                TextSize = 12,
                Name = "Games"
            }
        };

        YAxes = new[]
        {
            new Axis
            {
                Name = "Sales (Millions)",
                TextSize = 12
            }
        };
    }
}