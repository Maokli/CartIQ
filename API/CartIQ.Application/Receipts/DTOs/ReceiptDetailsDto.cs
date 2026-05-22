namespace CartIQ.Application.Receipts.DTOs;

public class ReceiptItemDto
{
    public Guid Id { get; set; }
    public required string ProductName { get; set; }
    public double ProductPrice { get; set; }
}

public class ReceiptDetailsDto
{
    public Guid Id { get; set; }
    public string Merchant { get; set; }
    public string Currency { get; set; }
    public IReadOnlyCollection<ReceiptItemDto> Items { get; set; }
}