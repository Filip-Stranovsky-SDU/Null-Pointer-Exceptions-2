using CsvHelper.Configuration.Attributes;

namespace homework3_livecharts.Models;

public class FoodWastage
{
    [Name("Country")]
    public string Country { get; set; }
    
    [Name("Year")]
    public int Year { get; set; }
    
    [Name("Food Category")]
    public string FoodCategory { get; set; }
    
    [Name("Total Waste (Tons)")]
    public decimal TotalWaste { get; set; }
    
    [Name("Economic Loss (Million $)")]
    public decimal EconomicLoss { get; set; }
    
    [Name("Avg Waste per Capita (Kg)")]
    public decimal AvgWastePerCapita { get; set; }
    
    [Name("Population (Million)")]
    public decimal Population { get; set; }
    
    [Name("Household Waste (%)")]
    public decimal HouseholdWaste { get; set; }
}