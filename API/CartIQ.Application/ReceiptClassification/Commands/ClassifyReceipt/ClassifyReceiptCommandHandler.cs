using Ardalis.GuardClauses;
using CartIQ.Application.Common.Interfaces;
using CartIQ.Application.OCR.Interfaces;
using CartIQ.Application.ReceiptClassification.DTOs;
using CartIQ.Application.ReceiptClassification.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CartIQ.Application.ReceiptClassification.Commands.ClassifyReceipt;

public class ClassifyReceiptCommandHandler : IRequestHandler<ClassifyReceiptCommand, ClassificationResultDto>
{
    private readonly ILogger<ClassifyReceiptCommandHandler> _logger;
    private readonly IReceiptClassificationService _receiptClassificationService;
    private readonly IReceiptsRepository _receiptsRepository;

    public ClassifyReceiptCommandHandler(ILogger<ClassifyReceiptCommandHandler> logger, 
        IReceiptClassificationService receiptClassificationService,
        IReceiptsRepository receiptsRepository)
    {
        _logger = logger;
        _receiptClassificationService = receiptClassificationService;
        _receiptsRepository = receiptsRepository;
    }
    
    public async Task<ClassificationResultDto> Handle(ClassifyReceiptCommand request, CancellationToken cancellationToken)
    {
        var receipt = await _receiptsRepository.GetReceiptAsync(request.ReceiptId, cancellationToken);
        var rawOcrResult = receipt.RawOcrResult?.Result;
        Guard.Against.Null(rawOcrResult, nameof(rawOcrResult), "Cannot classify receipt with null ocr result");
        
        _logger.LogInformation("Started Classification for receipt {id}", receipt.Id);
        var classficationResult = await _receiptClassificationService.ClassifyAsync(rawOcrResult,  cancellationToken);
        _logger.LogInformation("Finished Classification for receipt {id}", receipt.Id);
        _logger.LogDebug("Classification result for receipt {id}: \n {result}", receipt.Id, classficationResult);
        
        return classficationResult;
    }
}