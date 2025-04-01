namespace homework3_livecharts.Models;
using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;

public class Cholocate
{
    [Name("Sales Person")]
    public string SalesPerson { get; set; }
    
    [Name("Country")]
    public string Country { get; set; }
    
    [Name("Product")]
    public string Product { get; set; }
    
    [Name("Date")]
    public string Date { get; set; }
    
    [Name("Amount")]
    public string Amount { get; set; }
    
    [Name("Boxes Shipped")]
    public int BoxesShipped { get; set; }
}