namespace CartIQ.Infrastructure.OCR.DTOs;


public class ReceiptItem
{
    /// <summary>
    /// Name of the product as it appears on the receipt.
    /// Example: Happy Farmer Chicken breast filet, RinderHackfleish
    /// </summary>
    public string ProductName { get; set; }
    
    /// <summary>
    /// Price as it appears on the receipt.
    /// </summary>
    public double ProductPrice { get; set; }
}

public class OcrResultDto
{
    /// <summary>
    /// Merchant or Vendor or Issuer of the receipt.
    /// Example: Lidl, Aldi, Edeka
    /// </summary>
    public string Merchant { get; set; }
    
    /// <summary>
    /// Currency of the receipt.
    /// Example: EUR, USD
    /// </summary>
    public string Currency { get; set; }
    public IReadOnlyCollection<ReceiptItem> Items { get; set; }
}