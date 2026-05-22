using AutoMapper;
using CartIQ.Application.Common.Interfaces;
using CartIQ.Application.Common.Models;
using CartIQ.Application.Receipts.DTOs;
using CartIQ.Domain.Entities;
using MediatR;

namespace CartIQ.Application.Receipts.Commands.UpdateReceipt;

public class UpdateReceiptCommandHandler : IRequestHandler<UpdateReceiptCommand, Result<bool>>
{
    private readonly IReceiptsRepository _receiptsRepository;
    private readonly IReceiptItemsRepository _receiptItemsRepository;
    private readonly IMapper _mapper;
    public UpdateReceiptCommandHandler(IReceiptsRepository  receiptsRepository, IReceiptItemsRepository  receiptItemsRepository,  IMapper mapper)
    {
        _receiptsRepository = receiptsRepository;
        _receiptItemsRepository = receiptItemsRepository;
        _mapper = mapper;
    }
    
    public async Task<Result<bool>> Handle(UpdateReceiptCommand request, CancellationToken cancellationToken)
    {
        // TODO: Wrap in transaction
        await _receiptItemsRepository.DeleteReceiptItemsAsync(
            receiptItemIds: request.ItemsToDelete.Select(i => i.Id).ToList(),
            cancellationToken: cancellationToken);

        var itemsToAdd = request.ItemsToAdd
            .Select(i =>
                new ReceiptItem(receiptId: request.Id,
                    productName: i.ProductName,
                    productPrice: i.ProductPrice
                )
            )
            .ToList();
        await _receiptItemsRepository.AddReceiptItemsAsync(
            receiptItems: itemsToAdd,
            cancellationToken: cancellationToken
        );

        var isSuccess = await _receiptsRepository.UpdateReceiptAsync(
            receipt: new Receipt(id: request.Id, currency: request.Currency, merchant: request.Merchant),
            cancellationToken: cancellationToken
        );
        
        return Result<bool>.Success(isSuccess);
    }
}