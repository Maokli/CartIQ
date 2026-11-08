using CartIQ.Application.Common.Interfaces;
using CartIQ.Application.Messaging;
using CartIQ.Application.Receipts.Commands.CreateReceipt;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CartIQ.Infrastructure.FileUpload.Messaging;

public sealed record FileUploadedEvent(Guid fileId);

public class FIleUploadedEventConsumer : IConsumer<FileUploadedEvent>
{
    private readonly ILogger<FIleUploadedEventConsumer> _logger;
    private readonly ISender _sender;
    private readonly IEventPublisher _eventPublisher;
    public FIleUploadedEventConsumer(ILogger<FIleUploadedEventConsumer> logger, 
        ISender sender,
        IEventPublisher eventPublisher)
    {
        _logger = logger;
        _sender = sender;
        _eventPublisher = eventPublisher;
    }
    
    public async Task Consume(ConsumeContext<FileUploadedEvent> context)
    {
        var message = context.Message;
        _logger.LogInformation("FileUploadedEventConsumer received: {fileId}", message.fileId);
        
        // 1- Create receipt
        var createReceiptCommand = new CreateReceiptCommand(message.fileId);
        var receipt = await _sender.Send(createReceiptCommand, context.CancellationToken);
            
        // 2- Publish receipt created Event
        await _eventPublisher.PublishReceiptCreatedEvent(receiptId: receipt.Id,
            fileId: message.fileId,
            cancellationToken: context.CancellationToken);
    }
}