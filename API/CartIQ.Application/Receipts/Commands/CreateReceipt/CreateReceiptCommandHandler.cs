using CartIQ.Application.Common.Interfaces;
using CartIQ.Application.Common.Models;
using CartIQ.Application.Messaging;
using CartIQ.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CartIQ.Application.Receipts.Commands.CreateReceipt;

public class CreateReceiptCommandHandler : IRequestHandler<CreateReceiptCommand, Receipt>
{
    private readonly ILogger<CreateReceiptCommandHandler> _logger;
    private readonly IReceiptsRepository _receiptsRepository;
    private readonly IEventPublisher _eventPublisher;
    public CreateReceiptCommandHandler(ILogger<CreateReceiptCommandHandler> logger, 
        IReceiptsRepository receiptsRepository,
        IEventPublisher eventPublisher)
    {
        _logger = logger;
        _receiptsRepository = receiptsRepository;
        _eventPublisher = eventPublisher;
    }
    
    public async Task<Receipt> Handle(CreateReceiptCommand request, CancellationToken cancellationToken)
    {
        // 1- Create Empty Receipt
        var receipt = await _receiptsRepository.AddEmptyAsync(fileId: request.FileId, cancellationToken: cancellationToken);
        _logger.LogInformation("Receipt added with Id: {receiptId}", receipt.Id);

        return receipt;
    }
}