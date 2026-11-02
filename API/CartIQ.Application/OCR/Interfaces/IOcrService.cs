namespace CartIQ.Application.OCR.Interfaces;

public interface IOcrService
{
    Task<string> DoOcr(MemoryStream fileContent);
}