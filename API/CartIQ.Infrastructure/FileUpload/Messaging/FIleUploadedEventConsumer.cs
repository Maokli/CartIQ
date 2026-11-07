using CartIQ.Application.Common.Interfaces;
using CartIQ.Application.Messaging;
using CartIQ.Infrastructure.Data.Repositories;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace CartIQ.Infrastructure.FileUpload.Messaging;

public sealed record FileUploadedEvent(Guid fileId);

public class FIleUploadedEventConsumer : IConsumer<FileUploadedEvent>
{
    private readonly ILogger<FIleUploadedEventConsumer> _logger;
    private readonly IReceiptsRepository _receiptsRepository;
    private readonly IEventPublisher _eventPublisher;
    public FIleUploadedEventConsumer(ILogger<FIleUploadedEventConsumer> logger, 
        ReceiptsRepository receiptsRepository,
        IEventPublisher eventPublisher)
    {
        _logger = logger;
        _receiptsRepository = receiptsRepository;
        _eventPublisher = eventPublisher;
    }
    
    public async Task Consume(ConsumeContext<FileUploadedEvent> context)
    {
        var message = context.Message;
        // 1- Create Empty Receipt
        var receipt = await _receiptsRepository.AddEmptyAsync(fileId: message.fileId, cancellationToken: context.CancellationToken);
        
        // 2- Publish receipt created Event
        await _eventPublisher.PublishReceiptCreatedEvent(receiptId: receipt.Id,
            fileId: message.fileId,
            cancellationToken: context.CancellationToken);
    }
}