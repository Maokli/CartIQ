using CartIQ.Application.ReceiptClassification.Commands.ClassifyReceipt;
using CartIQ.Infrastructure.FileUpload.Messaging;
using CartIQ.Infrastructure.OCR.Messaging;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CartIQ.Infrastructure.ReceiptClassification.Messaging;

public class OcrCompletedEventConsumer : IConsumer<OcrCompletedEvent>
{
    private readonly ILogger<OcrCompletedEventConsumer> _logger;
    private readonly ISender _sender;
    
    public OcrCompletedEventConsumer(ILogger<OcrCompletedEventConsumer> logger,  ISender sender)
    {
        _logger = logger;
        _sender = sender;
    }
    
    public Task Consume(ConsumeContext<OcrCompletedEvent> context)
    {
        _logger.LogInformation("Received ReceiptCreatedEvent");
        
        var classifyReceiptCommand = new ClassifyReceiptCommand(context.Message.ReceiptId);
        _sender.Send(classifyReceiptCommand);
        
        return Task.CompletedTask;
    }
}