using System.Collections.Concurrent;
using System.Threading.Channels;
using CartIQ.Application.Realtime.ReceiptState.DTOs;
using CartIQ.Application.Realtime.ReceiptState.Interfaces;

namespace CartIQ.Application.Realtime.ReceiptState;

public class SubscriptionManager : ISubscriptionManager
{
    private readonly ConcurrentDictionary<
        (Guid UserId, Guid ReceiptId),
        ConcurrentDictionary<Guid, Channel<ReceiptProcessingEvent>>
    > _subscriptions = new();
    
    public async IAsyncEnumerable<ReceiptProcessingEvent> SubscribeAsync(Guid userId, Guid receiptId, CancellationToken cancellationToken)
    {
        var subscriptionId = Guid.NewGuid();

        var channel = Channel.CreateBounded<ReceiptProcessingEvent>(
            new BoundedChannelOptions(32)
            {
                FullMode = BoundedChannelFullMode.DropOldest,
                SingleReader = true,
                SingleWriter = false
            });

        var key = (userId, receiptId);

        var subscribers = _subscriptions.GetOrAdd(
            key,
            _ => new ConcurrentDictionary<Guid, Channel<ReceiptProcessingEvent>>());

        subscribers.TryAdd(subscriptionId, channel);

        try
        {
            await foreach (var message in channel.Reader.ReadAllAsync(
                               cancellationToken))
            {
                yield return message;
            }
        }
        finally
        {
            subscribers.TryRemove(subscriptionId, out _);
            channel.Writer.TryComplete();

            if (subscribers.IsEmpty)
            {
                _subscriptions.TryRemove(
                    new KeyValuePair<
                        (Guid UserId, Guid ReceiptId),
                        ConcurrentDictionary<Guid, Channel<ReceiptProcessingEvent>>
                    >(key, subscribers));
            }
        }
    }

    public ValueTask PublishAsync(ReceiptProcessingEvent message, CancellationToken cancellationToken)
    {
        var key = (message.UserId, message.ReceiptId);

        if (!_subscriptions.TryGetValue(key, out var subscribers))
            return ValueTask.CompletedTask;

        foreach (var channel in subscribers.Values)
        {
            channel.Writer.TryWrite(message);
        }

        return ValueTask.CompletedTask;
    }
}