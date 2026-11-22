using CartIQ.Application.Realtime.ReceiptState.DTOs;
using CartIQ.Application.Realtime.ReceiptState.Interfaces;
using CartIQ.WebApi.Infrastructure;

namespace CartIQ.WebApi.Endpoints;

using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Net.ServerSentEvents;

public class Realtime : IEndpointGroup
{
    public static string RoutePrefix => "/api/realtime";

    public static void Map(RouteGroupBuilder groupBuilder)
    {
        groupBuilder.MapGet(SubscribeToReceipt, "receipts/{id:guid}");
    }

    [EndpointSummary("Subscribe to receipt processing updates.")]
    [EndpointDescription(
        "Establishes an SSE connection to receive real-time processing updates for a receipt.")]
    public static IResult SubscribeToReceipt(
        Guid id,
        ISubscriptionManager subscriptions,
        CancellationToken cancellationToken)
    {
        // TODO: Fix with proper auth when auth is in place
        var userId = id;

        var events = StreamEvents(
            subscriptions,
            userId,
            id,
            cancellationToken);

        return TypedResults.ServerSentEvents(events);
    }

    private static async IAsyncEnumerable<SseItem<ReceiptProcessingEvent>>
        StreamEvents(
            ISubscriptionManager subscriptions,
            Guid userId,
            Guid receiptId,
            [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        await foreach (var message in subscriptions.SubscribeAsync(
                           userId,
                           receiptId,
                           cancellationToken))
        {
            yield return new SseItem<ReceiptProcessingEvent>(
                message,
                eventType: "receipt-processing")
            {
                EventId = message.EventId.ToString()
            };
        }
    }
}
