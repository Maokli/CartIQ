using CartIQ.Application.Common.Interfaces;
using CartIQ.Application.Files.Interfaces;
using CartIQ.Application.OCR.Interfaces;
using Microsoft.Extensions.Logging;

namespace CartIQ.Infrastructure.OCR;

public class McpOcrAPiOCRService : IOcrService
{
    private readonly IFileUploadService _fileUploadService;
    private readonly IReceiptsRepository _receiptsRepository;
    private readonly McpOcrApiHttpClient _ocrApiHttpClient;
    
    public McpOcrAPiOCRService(IFileUploadService fileUploadService,  
        IReceiptsRepository receiptsRepository,
        HttpClient httpClient,
        ILogger<McpOcrAPiOCRService> logger)
    {
        _fileUploadService = fileUploadService;
        _receiptsRepository = receiptsRepository;
        _ocrApiHttpClient = new McpOcrApiHttpClient(httpClient);
        logger.LogInformation("Http client created with base address {baseAddress}",  httpClient.BaseAddress);
    }
    
    // TODO: I feel like this too much business logic to be in infrastructure, Make a decision here
    public async Task<string> DoOcr(Guid receiptId, CancellationToken cancellationToken)
    {
        var receipt = await _receiptsRepository.GetReceiptAsync(receiptId, cancellationToken);
        var fileName = receipt.FileMetadata.FileName;
        var file = await _fileUploadService.GetFile(fileName, cancellationToken);

        var ocrResult = await _ocrApiHttpClient.ProcessAsync(file, fileName, cancellationToken);
        
        return ocrResult;
    }
}