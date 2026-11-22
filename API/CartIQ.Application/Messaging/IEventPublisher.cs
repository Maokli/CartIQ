namespace CartIQ.Application.Messaging;

public interface IEventPublisher
{
    Task PublishFileUploadedEvent(Guid fileId, CancellationToken cancellationToken);
    Task PublishReceiptCreatedEvent(Guid receiptId, Guid fileId, CancellationToken cancellationToken);
    Task PublishOcrCompletedEvent(Guid receiptId, Guid fileId, CancellationToken cancellationToken);
    Task PublishReceiptProcessingPendingEvent(Guid receiptId, Guid userId, CancellationToken cancellationToken);
    Task PublishReceiptProcessingUploadingEvent(Guid receiptId, Guid userId, CancellationToken cancellationToken);
    Task PublishReceiptProcessingOcrProcessingEvent(Guid receiptId, Guid userId, CancellationToken cancellationToken);
    Task PublishReceiptProcessingOcrCompletedEvent(Guid receiptId, Guid userId, CancellationToken cancellationToken);
    Task PublishReceiptProcessingClassifyingEvent(Guid receiptId, Guid userId, CancellationToken cancellationToken);
    Task PublishReceiptProcessingMatchingProductsEvent(Guid receiptId, Guid userId, CancellationToken cancellationToken);
    Task PublishReceiptProcessingCompletedEvent(Guid receiptId, Guid userId, CancellationToken cancellationToken);
    Task PublishReceiptProcessingFailedEvent(Guid receiptId, Guid userId, string message, CancellationToken cancellationToken);
}