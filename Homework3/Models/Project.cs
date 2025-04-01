using System;
using CsvHelper.Configuration.Attributes;

namespace homework3_livecharts.Models;

public class Project
{
    [Name("ID")]
    public long ID { get; set; }
    
    [Name("Name")]
    public string Name { get; set; }
    
    [Name("Category")]
    public string Category { get; set; }
    
    [Name("main_category")]
    public string Main_Category { get; set; }
    
    [Name("currency")]
    public string Currency { get; set; }
    
    [Name("deadline")]
    public string Deadline { get; set; }
    
    [Name("goal")]
    public decimal Goal { get; set; }
    
    [Name("launched")]
    public string Launched { get; set; }
    
    [Name("pledged")]
    public decimal Pledged { get; set; }
    
    [Name("state")]
    public string State { get; set; }
    
    [Name("backers")]
    public decimal Backers { get; set; }
    
    [Name("country")]
    public string Country { get; set; }
    
    [Name("usd pledged")]
    public decimal Usd_Pledged { get; set; }
    
}