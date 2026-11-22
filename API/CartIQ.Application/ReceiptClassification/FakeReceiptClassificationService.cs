using CartIQ.Application.ReceiptClassification.DTOs;
using CartIQ.Application.ReceiptClassification.Interfaces;
using Microsoft.Extensions.Logging;

namespace CartIQ.Application.ReceiptClassification;

public class FakeReceiptClassificationService : IReceiptClassificationService
{
    private readonly ILogger<FakeReceiptClassificationService> _logger;

    public FakeReceiptClassificationService(ILogger<FakeReceiptClassificationService> logger)
    {
        _logger = logger;
    }
    
    public Task<ClassificationResultDto> ClassifyAsync(string rawOcrResult, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Classifying using FakeReceiptClassificationService");
        var result = new ClassificationResultDto
        {
            Merchant = "REWE",
            Currency = "EUR",
            Items = new List<ReceiptItem>
            {
                new() { ProductName = "REWE Bio Hähnchenbrustfilet 400g", ProductPrice = 5.99 },
                new() { ProductName = "Rinderhackfleisch 500g", ProductPrice = 4.99 },
                new() { ProductName = "Grana Padano gerieben 150g", ProductPrice = 2.49 },
                new() { ProductName = "Barilla Penne Rigate 500g", ProductPrice = 1.79 },
                new() { ProductName = "REWE Beste Wahl passierte Tomaten 500g", ProductPrice = 0.99 },
                new() { ProductName = "Gurke", ProductPrice = 0.89 },
                new() { ProductName = "Paprika rot", ProductPrice = 1.29 },
                new() { ProductName = "Bananen 1kg", ProductPrice = 1.49 },
                new() { ProductName = "Milbona H-Milch 1L", ProductPrice = 1.09 },
                new() { ProductName = "ja! Eier aus Bodenhaltung 10 Stück", ProductPrice = 2.49 }
            }
        };
        
        return Task.FromResult(result);
    }
}