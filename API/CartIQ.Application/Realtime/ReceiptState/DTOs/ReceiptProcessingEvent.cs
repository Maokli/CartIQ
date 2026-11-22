namespace CartIQ.Application.Realtime.ReceiptState.DTOs;
public enum ReceiptProcessingStatus
{
    Pending,
    Uploading,
    OcrProcessing,
    OcrCompleted,
    Classifying,
    MatchingProducts,
    Completed,
    Failed
}

public sealed record ReceiptProcessingEvent(
    Guid EventId,
    Guid ReceiptId,
    Guid UserId,
    ReceiptProcessingStatus Status,
    string? Message,
    int Progress,
    int Version,
    DateTimeOffset OccurredAt
);

public static class ReceiptProcessingEventFactory
{
    private const int Version = 1;

    public static ReceiptProcessingEvent Create(
        Guid receiptId,
        Guid userId,
        ReceiptProcessingStatus status,
        string? message = null)
    {
        return new ReceiptProcessingEvent(
            Guid.NewGuid(),
            receiptId,
            userId,
            status,
            message,
            CalculateProgress(status),
            Version,
            DateTimeOffset.UtcNow
        );
    }

    private static int CalculateProgress(ReceiptProcessingStatus status)
    {
        return status switch
        {
            ReceiptProcessingStatus.Pending          => 0,
            ReceiptProcessingStatus.Uploading        => 10,
            ReceiptProcessingStatus.OcrProcessing    => 30,
            ReceiptProcessingStatus.OcrCompleted     => 50,
            ReceiptProcessingStatus.Classifying      => 65,
            ReceiptProcessingStatus.MatchingProducts => 85,
            ReceiptProcessingStatus.Completed        => 100,
            ReceiptProcessingStatus.Failed           => 0,
            _ => throw new ArgumentOutOfRangeException(nameof(status), status, null)
        };
    }
}
