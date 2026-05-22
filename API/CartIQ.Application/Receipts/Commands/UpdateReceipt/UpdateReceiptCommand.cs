using CartIQ.Application.Common.Models;
using CartIQ.Application.Receipts.DTOs;
using MediatR;

namespace CartIQ.Application.Receipts.Commands.UpdateReceipt;

public class UpdateReceiptCommand : IRequest<Result<bool>>
{
    public Guid Id { get; set; }
    public string Merchant { get; set; }
    public string Currency { get; set; }
    public IReadOnlyCollection<ReceiptItemDto> ItemsToDelete { get; set; }
    public IReadOnlyCollection<ReceiptItemDto> ItemsToAdd { get; set; }
}