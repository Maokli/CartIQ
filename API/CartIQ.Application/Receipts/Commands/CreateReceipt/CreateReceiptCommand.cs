using CartIQ.Application.Common.Models;
using CartIQ.Domain.Entities;
using MediatR;

namespace CartIQ.Application.Receipts.Commands.CreateReceipt;

public class CreateReceiptCommand : IRequest<Receipt>
{
    public Guid FileId { get; }

    public CreateReceiptCommand(Guid fileId)
    {
        FileId = fileId;
    }
}