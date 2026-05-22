using CartIQ.Application.Common.Models;
using MediatR;

namespace CartIQ.Application.Receipts.Commands.DeleteReceipt;

public class DeleteReceiptCommand : IRequest<Result<bool>>
{
    public Guid Id { get; set; }

    public DeleteReceiptCommand(Guid id)
    {
        Id = id;
    }
}