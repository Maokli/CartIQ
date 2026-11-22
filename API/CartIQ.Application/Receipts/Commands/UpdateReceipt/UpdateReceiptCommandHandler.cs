using AutoMapper;
using CartIQ.Application.Common.Interfaces;
using CartIQ.Application.Common.Models;
using CartIQ.Application.Receipts.DTOs;
using CartIQ.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CartIQ.Application.Receipts.Commands.UpdateReceipt;

public class UpdateReceiptCommandHandler : IRequestHandler<UpdateReceiptCommand, Result<bool>>
{
    private readonly IReceiptsRepository _receiptsRepository;
    private readonly ILogger<UpdateReceiptCommandHandler> _logger;
    public UpdateReceiptCommandHandler(IReceiptsRepository  receiptsRepository,
        ILogger<UpdateReceiptCommandHandler> logger)
    {
        _receiptsRepository = receiptsRepository;
        _logger = logger;
    }
    
    public async Task<Result<bool>> Handle(UpdateReceiptCommand request, CancellationToken cancellationToken)
    {
        var itemIdsToDelete = request.ItemsToDelete.Select(i => i.Id).ToList();
        var itemsToAdd = request.ItemsToAdd
            .Select(i =>
                new ReceiptItem(receiptId: request.Id,
                    productName: i.ProductName,
                    productPrice: i.ProductPrice
                )
                {
                    // TODO: fix ugly meess
                    CreatedBy = "louay",
                    Created =  DateTime.UtcNow,
                    LastModifiedBy = "louay",
                    LastModified =  DateTime.UtcNow,
                }
            )
            .ToList();
        
        _logger.LogInformation($"Updating receipt {request.Id}");
        _logger.LogDebug($"Updating receipt {request}");
        
        var isSuccess = await _receiptsRepository.UpdateReceiptAsync(receiptId: request.Id, 
            newCurrency: request.Currency, 
            newMerchant: request.Merchant,
            itemIdsToDelete: itemIdsToDelete,
            itemsToAdd: itemsToAdd,
            cancellationToken: cancellationToken
        );
        
        return Result<bool>.Success(isSuccess);
    }
}