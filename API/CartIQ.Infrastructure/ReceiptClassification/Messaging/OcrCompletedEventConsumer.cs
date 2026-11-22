using CartIQ.Application.Files.Commands.UpdateState;
using CartIQ.Application.Messaging;
using CartIQ.Application.ReceiptClassification.Commands.ClassifyReceipt;
using CartIQ.Application.ReceiptClassification.DTOs;
using CartIQ.Application.Receipts.Commands.UpdateReceipt;
using CartIQ.Application.Receipts.DTOs;
using CartIQ.Domain.Enums;
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
    private readonly IEventPublisher _eventPublisher;
    
    public OcrCompletedEventConsumer(ILogger<OcrCompletedEventConsumer> logger,  ISender sender, IEventPublisher eventPublisher)
    {
        _logger = logger;
        _sender = sender;
        _eventPublisher = eventPublisher;
    }
    
    public async Task Consume(ConsumeContext<OcrCompletedEvent> context)
    {
        _logger.LogInformation("Received ReceiptCreatedEvent");
        
        // 1- Classify receipt
        var message = context.Message;
        await _eventPublisher.PublishReceiptProcessingClassifyingEvent(message.ReceiptId, message.ReceiptId, context.CancellationToken);
        var classifyReceiptCommand = new ClassifyReceiptCommand(context.Message.ReceiptId);
        var classificationResult = await _sender.Send(classifyReceiptCommand, context.CancellationToken);
        
        // 2- Persist classification result
        var updateReceiptCommand = GetUpdateReceiptCommand(context.Message.ReceiptId, classificationResult);
        await _sender.Send(updateReceiptCommand, context.CancellationToken);
        await _sender.Send(new UpdateFileStateCommand(context.Message.FileId, FileState.OcrCompleted), context.CancellationToken);
        await _eventPublisher.PublishReceiptProcessingMatchingProductsEvent(message.ReceiptId, message.ReceiptId, context.CancellationToken);
        await _eventPublisher.PublishReceiptProcessingCompletedEvent(message.ReceiptId, message.ReceiptId, context.CancellationToken);
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