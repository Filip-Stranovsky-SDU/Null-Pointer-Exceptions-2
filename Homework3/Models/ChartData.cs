
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;

namespace homework3_livecharts.Models;


public class ChartData
{
    
    public ISeries[] ChartSeries { get; private set; }
    public Axis[] XAxes { get; private set;}
    public Axis[] YAxes { get; private set;}

    public ChartData(ISeries[] chartSeries, Axis[] xAxes, Axis[] yAxes)
    {
        ChartSeries = chartSeries;
        XAxes = xAxes;
        YAxes = yAxes;
    }
}