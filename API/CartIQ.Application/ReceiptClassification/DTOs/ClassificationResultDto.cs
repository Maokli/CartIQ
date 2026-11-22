using System.ComponentModel;

namespace CartIQ.Application.ReceiptClassification.DTOs;


public class ReceiptItem
{
    [Description("""
        Name of the product as it appears on the receipt.
        Examples: Happy Farmer Chicken breast filet, Rinderhackfleisch.
    """)]
    public string ProductName { get; set; }
    
    [Description("Price of the product as it appears on the receipt.")]
    public double ProductPrice { get; set; }
}

public class ClassificationResultDto
{ 
    [Description("""
        Merchant, vendor, or issuer of the receipt.
        Examples: Lidl, Aldi, Edeka.
    """)]
    public string Merchant { get; set; }
    
    [Description("""
        Currency used on the receipt.
        Examples: EUR, USD.
    """)]
    public string Currency { get; set; }
    
    [Description("All individual product line items extracted from the receipt.")]
    public IReadOnlyCollection<ReceiptItem> Items { get; set; }
}