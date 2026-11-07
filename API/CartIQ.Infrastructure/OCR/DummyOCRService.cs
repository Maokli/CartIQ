using Ardalis.GuardClauses;
using CartIQ.Application.Common.Interfaces;
using CartIQ.Application.Files.Interfaces;
using CartIQ.Application.OCR.Interfaces;
using CartIQ.Infrastructure.OCR.DTOs;

namespace CartIQ.Infrastructure.OCR;

/// <summary>
/// Dummy ocr service to test integration, real implementation will follow later
/// </summary>
public class DummyOCRService : IOcrService
{
    private readonly IFileUploadService _fileUploadService;
    private readonly IReceiptsRepository _receiptsRepository;
    public DummyOCRService(IFileUploadService fileUploadService,  IReceiptsRepository receiptsRepository)
    {
        _fileUploadService = fileUploadService;
        _receiptsRepository = receiptsRepository;
    }
    
    public async Task<OcrResultDto> DoOcr(Guid receiptId, CancellationToken cancellationToken)
    {
        var receipt = await _receiptsRepository.GetReceiptAsync(receiptId, cancellationToken);
        var fileName = receipt.FileMetadata.FileName;
        var file = await _fileUploadService.GetFile(fileName, cancellationToken);
        
        Thread.Sleep(500);
        return GetFakeOcrResult();
    }
    
    private OcrResultDto GetFakeOcrResult()
    {
        return new OcrResultDto()
        {
            Merchant = "Rewe",
            Currency = "EUR",
            Items = new List<ReceiptItem>()
            {
                new ReceiptItem()
                {
                    ProductName = "Eggs",
                    ProductPrice = 3.96,
                }
            }
        };
    }
}