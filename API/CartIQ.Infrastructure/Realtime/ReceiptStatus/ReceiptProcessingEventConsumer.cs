using CartIQ.Application.Realtime.ReceiptState.DTOs;
using CartIQ.Application.Realtime.ReceiptState.Interfaces;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace CartIQ.Infrastructure.Realtime.ReceiptStatus;

public class ReceiptProcessingEventConsumer : IConsumer<ReceiptProcessingEvent>
{
    private readonly ILogger<ReceiptProcessingEventConsumer> _logger;
    private readonly ISubscriptionManager  _subscriptionManager;
    
    public ReceiptProcessingEventConsumer(ILogger<ReceiptProcessingEventConsumer> logger,  ISubscriptionManager subscriptionManager)
    {
        _logger = logger;
        _subscriptionManager = subscriptionManager;
    }
    
    public async Task Consume(ConsumeContext<ReceiptProcessingEvent> context)
    {
        var message = context.Message;
        _logger.LogDebug($"Received processing status {message.Status} for receipt {message.ReceiptId}");
        
        await _subscriptionManager.PublishAsync(message, context.CancellationToken);
    }
}