using CartIQ.Application.Realtime.ReceiptState.DTOs;

namespace CartIQ.Application.Realtime.ReceiptState.Interfaces;

public interface ISubscriptionManager
{
    IAsyncEnumerable<ReceiptProcessingEvent> SubscribeAsync(
        Guid userId,
        Guid receiptId,
        CancellationToken cancellationToken);

    ValueTask PublishAsync(
        ReceiptProcessingEvent message,
        CancellationToken cancellationToken);
}