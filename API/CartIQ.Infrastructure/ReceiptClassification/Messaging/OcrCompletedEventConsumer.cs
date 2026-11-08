using CartIQ.Infrastructure.FileUpload.Messaging;
using CartIQ.Infrastructure.OCR.Messaging;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace CartIQ.Infrastructure.ReceiptClassification.Messaging;

public class OcrCompletedEventConsumer : IConsumer<OcrCompletedEvent>
{
    private readonly ILogger<OcrCompletedEventConsumer> _logger;
    
    public OcrCompletedEventConsumer(ILogger<OcrCompletedEventConsumer> logger)
    {
        _logger = logger;
    }
    
    public Task Consume(ConsumeContext<OcrCompletedEvent> context)
    {
        _logger.LogInformation("Received ReceiptCreatedEvent");
        
        return Task.CompletedTask;
    }
}