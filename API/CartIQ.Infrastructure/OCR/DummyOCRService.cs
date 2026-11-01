using CartIQ.Application.OCR.Interfaces;

namespace CartIQ.Infrastructure.OCR;

/// <summary>
/// Dummy ocr service to test integration, real implementation will follow later
/// </summary>
public class DummyOCRService : IOcrService
{
    public Task<string> DoOcr(MemoryStream fileContent)
    {
        Thread.Sleep(500);
        return Task.FromResult("REWE 20/12/2026");
    }
}