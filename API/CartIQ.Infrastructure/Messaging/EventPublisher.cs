using CartIQ.Application.Messaging;
using CartIQ.Infrastructure.FileUpload.Messaging;
using MassTransit;

namespace CartIQ.Infrastructure.Messaging;

public class EventPublisher(IPublishEndpoint publishEndpoint) : IEventPublisher
{
    public async Task PublishFileUploadedEvent(Guid fileId, CancellationToken cancellationToken) => 
        await publishEndpoint.Publish(new FileUploadedEvent(fileId));

    public async Task PublishReceiptCreatedEvent(Guid receiptId, Guid fileId, CancellationToken cancellationToken) =>
        await publishEndpoint.Publish(new ReceiptCreatedEvent(receiptId, fileId));
}