using CartIQ.Application.Common.Interfaces;
using CartIQ.Application.Common.Models;
using MediatR;

namespace CartIQ.Application.Receipts.Commands.DeleteReceipt;

public class DeleteReceiptCommandHandler : IRequestHandler<DeleteReceiptCommand, Result<bool>>
{
    private readonly IReceiptsRepository _receiptsRepository;
    public DeleteReceiptCommandHandler(IReceiptsRepository  receiptsRepository)
    {
        _receiptsRepository = receiptsRepository;
    }

    public async Task<Result<bool>> Handle(DeleteReceiptCommand request, CancellationToken cancellationToken)
    {
        var isSuccess = await _receiptsRepository.DeleteReceiptAsync(request.Id, cancellationToken);
        
        return Result<bool>.Success(isSuccess);
    }
}