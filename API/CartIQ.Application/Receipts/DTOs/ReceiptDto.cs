using CartIQ.Application.Common.Models;
using CartIQ.Domain.Enums;

namespace CartIQ.Application.Receipts.DTOs;

public class ReceiptDto
{
    public Guid Id { get; set; }
    public DateTimeOffset Created { get; set; }
    public MerchantDto Merchant { get; set; }
    public string Currency { get; set; }
    public double TotalAmount { get; set; }
    public NutrientsDto Nutrients { get; set; }
    public string Category { get; set; }
    public List<string> TopItems { get; set; }
    public FileMetadataDto File { get; set; }
}

public class FileMetadataDto
{
    public string ContentType { get; set; }
    public string FileName { get; set; }
    public FileState State { get; set; }
}
public class MerchantDto
{
    public string Name { get; set; }
    public string BackgroundColor { get; set; }
    public string Color { get; set; }
}