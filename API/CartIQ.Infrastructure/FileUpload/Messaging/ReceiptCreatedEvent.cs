namespace CartIQ.Infrastructure.FileUpload.Messaging;

public class ReceiptCreatedEvent
{
    public Guid ReceiptId { get; set; }
    public Guid FileId { get; set; }

    public ReceiptCreatedEvent(Guid receiptId, Guid fileId)
    {
        ReceiptId = receiptId;
        FileId = fileId;
    }
}