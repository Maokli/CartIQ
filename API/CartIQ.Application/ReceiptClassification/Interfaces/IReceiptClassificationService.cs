using CartIQ.Application.ReceiptClassification.DTOs;

namespace CartIQ.Application.ReceiptClassification.Interfaces;

public interface IReceiptClassificationService
{
    Task<ClassificationResultDto> ClassifyAsync(string rawOcrResult, CancellationToken cancellationToken);
}