namespace CartIQ.Infrastructure.OCR.Messaging;

public class OcrCompletedEvent
{
    public Guid ReceiptId { get; set; }
    public Guid FileId { get; set; }

    public OcrCompletedEvent(Guid receiptId, Guid fileId)
    {
        ReceiptId = receiptId;
        FileId = fileId;
    }
}