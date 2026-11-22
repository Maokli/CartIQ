using CartIQ.Application.Messaging;
using CartIQ.Application.Realtime.ReceiptState.DTOs;
using CartIQ.Infrastructure.FileUpload.Messaging;
using CartIQ.Infrastructure.OCR.Messaging;
using MassTransit;

namespace CartIQ.Infrastructure.Messaging;

public class EventPublisher(IPublishEndpoint publishEndpoint) : IEventPublisher
{
    public async Task PublishFileUploadedEvent(Guid fileId, CancellationToken cancellationToken) => 
        await publishEndpoint.Publish(new FileUploadedEvent(fileId));

    public async Task PublishReceiptCreatedEvent(Guid receiptId, Guid fileId, CancellationToken cancellationToken) =>
        await publishEndpoint.Publish(new ReceiptCreatedEvent(receiptId, fileId));
    
    public async Task PublishOcrCompletedEvent(Guid receiptId, Guid fileId, CancellationToken cancellationToken) =>
        await publishEndpoint.Publish(new OcrCompletedEvent(receiptId, fileId));
    
    // Receipt processing realtime events
    public Task PublishReceiptProcessingPendingEvent(
        Guid receiptId, Guid userId,
        CancellationToken cancellationToken) =>
        PublishReceiptProcessingEvent(
            receiptId, userId, ReceiptProcessingStatus.Pending,
            "Receipt queued for processing.", cancellationToken);

    public Task PublishReceiptProcessingUploadingEvent(
        Guid receiptId, Guid userId,
        CancellationToken cancellationToken) =>
        PublishReceiptProcessingEvent(
            receiptId, userId, ReceiptProcessingStatus.Uploading,
            "Uploading receipt...", cancellationToken);

    public Task PublishReceiptProcessingOcrProcessingEvent(
        Guid receiptId, Guid userId,
        CancellationToken cancellationToken) =>
        PublishReceiptProcessingEvent(
            receiptId, userId, ReceiptProcessingStatus.OcrProcessing,
            "Extracting text from receipt...", cancellationToken);

    public Task PublishReceiptProcessingOcrCompletedEvent(
        Guid receiptId, Guid userId,
        CancellationToken cancellationToken) =>
        PublishReceiptProcessingEvent(
            receiptId, userId, ReceiptProcessingStatus.OcrCompleted,
            "OCR processing completed.", cancellationToken);

    public Task PublishReceiptProcessingClassifyingEvent(
        Guid receiptId, Guid userId,
        CancellationToken cancellationToken) =>
        PublishReceiptProcessingEvent(
            receiptId, userId, ReceiptProcessingStatus.Classifying,
            "Classifying receipt items...", cancellationToken);

    public Task PublishReceiptProcessingMatchingProductsEvent(
        Guid receiptId, Guid userId,
        CancellationToken cancellationToken) =>
        PublishReceiptProcessingEvent(
            receiptId, userId, ReceiptProcessingStatus.MatchingProducts,
            "Matching receipt items with products...", cancellationToken);

    public Task PublishReceiptProcessingCompletedEvent(
        Guid receiptId, Guid userId,
        CancellationToken cancellationToken) =>
        PublishReceiptProcessingEvent(
            receiptId, userId, ReceiptProcessingStatus.Completed,
            "Receipt processing completed successfully.", cancellationToken);

    public Task PublishReceiptProcessingFailedEvent(
        Guid receiptId, Guid userId,
        string message,
        CancellationToken cancellationToken) =>
        PublishReceiptProcessingEvent(
            receiptId, userId, ReceiptProcessingStatus.Failed,
            message, cancellationToken);
    

    private Task PublishReceiptProcessingEvent(
        Guid receiptId,
        Guid userId,
        ReceiptProcessingStatus status,
        string? message,
        CancellationToken cancellationToken)
    {
        var processingEvent = ReceiptProcessingEventFactory.Create(
            receiptId,
            userId,
            status,
            message: message);

        return publishEndpoint.Publish(processingEvent, cancellationToken);
    }
}