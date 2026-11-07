using MediatR;

namespace CartIQ.Application.OCR.Commands;

public class DoOcrCommand : IRequest
{
    public Guid FileId { get; set; }
    public Guid ReceiptId { get; set; }

    public DoOcrCommand(Guid fileId, Guid receiptId)
    {
        FileId = fileId;
        ReceiptId = receiptId;
    }
}