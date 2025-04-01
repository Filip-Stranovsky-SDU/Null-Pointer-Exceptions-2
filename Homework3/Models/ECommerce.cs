using System;
using CsvHelper.Configuration.Attributes;

namespace homework3_livecharts.Models;

public class ECommerce
{
    [Name("InvoiceNo")]
    public int InvoiceNo { get; set; }
    
    [Name("StockCode")]
    public string StockCode { get; set; }
    
    [Name("Description")]
    public string Description { get; set; }
    
    [Name("Quantity")]
    public int Quantity { get; set; }
    
    [Name("InvoiceDate")]
    public string InvoiceDate { get; set; }
    
    [Name("UnitPrice")]
    public decimal UnitPrice { get; set; }
    
    [Name("CustomerID")]
    public int CustomerID { get; set; }
    
    [Name("Country")]
    public string Country { get; set; }

}