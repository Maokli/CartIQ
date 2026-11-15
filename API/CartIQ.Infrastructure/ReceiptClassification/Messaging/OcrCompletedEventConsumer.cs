using CartIQ.Application.ReceiptClassification.Commands.ClassifyReceipt;
using CartIQ.Application.ReceiptClassification.DTOs;
using CartIQ.Application.Receipts.Commands.UpdateReceipt;
using CartIQ.Application.Receipts.DTOs;
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
    
    public async Task Consume(ConsumeContext<OcrCompletedEvent> context)
    {
        _logger.LogInformation("Received ReceiptCreatedEvent");
        
        // 1- Classify receipt
        var classifyReceiptCommand = new ClassifyReceiptCommand(context.Message.ReceiptId);
        var classificationResult = await _sender.Send(classifyReceiptCommand, context.CancellationToken);
        
        // 2- Persist classification result
        // TODO: Update state also
        var updateReceiptCommand = GetUpdateReceiptCommand(context.Message.ReceiptId, classificationResult);
        await _sender.Send(updateReceiptCommand, context.CancellationToken);
    }

    private UpdateReceiptCommand GetUpdateReceiptCommand(Guid receiptId, ClassificationResultDto classificationResult)
    {
        return new UpdateReceiptCommand()
        {
            Id = receiptId,
            Currency = classificationResult.Currency,
            Merchant = classificationResult.Merchant,
            ItemsToAdd = classificationResult.Items.Select(i =>
                new ReceiptItemDto()
                {
                    ProductName = i.ProductName,
                    ProductPrice = i.ProductPrice
                }
            ).ToList(),
            ItemsToDelete = [],
        };
    }
}