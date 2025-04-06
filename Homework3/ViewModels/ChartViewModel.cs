using System;
using System.Diagnostics;
using System.Reactive;
using System.Windows.Input;
using Avalonia.Interactivity;
using homework3_livecharts.Models;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using ReactiveUI;


namespace homework3_livecharts.ViewModels;

public class ChartViewModel : ViewModelBase
{
    public ISeries[] ChartSeries { get; private set; }
    public Axis[] XAxes { get; private set;}
    public Axis[] YAxes { get; private set;}
    public ReactiveCommand<Unit, Unit> DeleteButtonCommand { get; private set;}

    public MainWindowViewModel MWVM {get; private set;}


    public ChartViewModel(ISeries[] chartSeries, Axis[] xAxes, Axis[] yAxes, MainWindowViewModel mwvm)
    {
        ChartSeries = chartSeries;
        XAxes = xAxes;
        YAxes = yAxes;
        MWVM = mwvm;
        
        DeleteButtonCommand = ReactiveCommand.Create(DeleteSelf);
    }
    private void DeleteSelf()
    {
        Debug.WriteLine("XD");
        MWVM.DeleteChart(this);
    }
    
}
