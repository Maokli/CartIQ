using CartIQ.Application.Common.Models;
using CartIQ.Application.Receipts.DTOs;
using MediatR;

namespace CartIQ.Application.Receipts.Queries.GetReceipt;

public class GetReceiptDetailsQuery : IRequest<Result<ReceiptDetailsDto>>
{
    public Guid Id { get; set; }

    public GetReceiptDetailsQuery(Guid id)
    {
        Id = id;
    }
}