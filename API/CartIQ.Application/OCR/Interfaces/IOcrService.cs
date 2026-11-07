using CartIQ.Infrastructure.OCR.DTOs;

namespace CartIQ.Application.OCR.Interfaces;

public interface IOcrService
{
    Task<OcrResultDto> DoOcr(Guid receiptId, CancellationToken cancellationToken);
}