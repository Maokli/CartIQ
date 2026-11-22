using CartIQ.Application.ReceiptClassification.DTOs;
using MediatR;

namespace CartIQ.Application.ReceiptClassification.Commands.ClassifyReceipt;

public class ClassifyReceiptCommand : IRequest<ClassificationResultDto>
{
    public Guid ReceiptId { get; }

    public ClassifyReceiptCommand(Guid receiptId)
    {
        ReceiptId = receiptId;
    }
}