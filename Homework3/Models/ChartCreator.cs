using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using homework3_livecharts.ViewModels;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;

namespace homework3_livecharts.Models;



public static class ChartCreator
{

    
    private static ObservableCollection<Sale> sales = new();
    public static ChartData GetChartData(Expression<Func<Sale, bool>> filter, int count = 10)
    {
        if (sales.Count == 0) LoadData();

        var topSales = sales
            .AsQueryable()
            .Where(filter)  // Query
            .Where(s => s.Global_Sales > 0) // Ensure valid sales data
            .OrderByDescending(s => s.Global_Sales)
            .Take(count) // Take top 10 games
            .ToList();
        ISeries[] salesChart = 
            {new ColumnSeries<double>
            {
                Values = topSales.Select(s => Convert.ToDouble(s.Global_Sales)).ToArray(),
                Fill = new SolidColorPaint(SKColors.Blue)
            }};
        Axis[] xAxes = 
            {new Axis
            {
                Labels = topSales.Select(s => s.Name).ToArray(),
                LabelsRotation = 30, 
                TextSize = 12,
                Name = "Games"
            }};
        Axis[] yAxes = 
            {new Axis
            {
                Name = "Sales (Millions)",
                TextSize = 12
            }};
        return new ChartData(salesChart, xAxes, yAxes);
    }

    private static void LoadData()
    {
        var games = DataLoader.LoadData<Sale>(
            Path.Combine(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory)!.Parent!.Parent!.Parent!.FullName, "Assets", "VideoGamesSales.csv"));
        foreach (var game in games)
        {
            sales.Add(game);
        }
    }

    public static ChartData CreateLineChart(Expression<Func<Sale, bool>> filter)
    {
        var years = sales
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
       ISeries[] salesChart =
            {new LineSeries<double>
            {
                Values = years.Select(s => Convert.ToDouble(s.TotalSales)).ToArray(),
                Fill = new SolidColorPaint(SKColors.Blue)
            }
            };
        Axis[] xAxes = 
            {new Axis
            {
                Labels = years.Select(s => s.Year).ToArray(),
                LabelsRotation = 90,
                TextSize = 12,
                Name = "Years"
            }};
        Axis[] yAxes = 
            {
            new Axis
            {
                Name = "Sales (Millions)",
                TextSize = 12
            }};
        return new ChartData(salesChart, xAxes, yAxes);
    }





}