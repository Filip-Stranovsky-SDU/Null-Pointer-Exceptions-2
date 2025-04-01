using CsvHelper.Configuration.Attributes;

namespace homework3_livecharts.Models;

public class Sale
{
    [Name("Rank")]
    public int Rank { get; set; }
    
    [Name("Name")]
    public string Name { get; set; }
    
    [Name("Platform")]
    public string Platform { get; set; }
    
    [Name("Year")]
    public string Year { get; set; }
    
    [Name("Genre")]
    public string Genre { get; set; }
    
    [Name("Publisher")]
    public string Publisher { get; set; }
    
    [Name("NA_Sales")]
    public decimal NA_Sales { get; set; }
    
    [Name("EU_Sales")]
    public decimal EU_Sales { get; set; }
    
    [Name("JP_Sales")]
    public decimal JP_Sales { get; set; }
    
    [Name("Other_Sales")]
    public decimal Other_Sales { get; set; }
    
    [Name("Global_Sales")]
    public decimal Global_Sales { get; set; }
}