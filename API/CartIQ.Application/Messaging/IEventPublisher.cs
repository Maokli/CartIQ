namespace CartIQ.Application.Messaging;

public interface IEventPublisher
{
    Task PublishFileUploadedEvent(Guid fileId, CancellationToken cancellationToken);
    Task PublishReceiptCreatedEvent(Guid receiptId, Guid fileId, CancellationToken cancellationToken);
}