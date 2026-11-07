using CartIQ.Infrastructure.OCR.DTOs;

namespace CartIQ.Application.OCR.Interfaces;

public interface IOcrService
{
    Task<string> DoOcr(Guid receiptId, CancellationToken cancellationToken);
}