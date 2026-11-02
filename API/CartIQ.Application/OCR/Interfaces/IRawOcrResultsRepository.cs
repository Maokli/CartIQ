using CartIQ.Domain.Entities;

namespace CartIQ.Application.OCR.Interfaces;

public interface IRawOcrResultsRepository
{
    Task<RawOcrResult> AddRawOcrResult(Guid id, string result, CancellationToken cancellationToken);
}