using CartIQ.Application.OCR.Interfaces;
using CartIQ.Domain.Entities;

namespace CartIQ.Infrastructure.Data.Repositories;

public class RawOcrResultsRepository : IRawOcrResultsRepository
{
    private readonly ApplicationDbContext _dbContext;

    public RawOcrResultsRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<RawOcrResult> AddRawOcrResult(Guid id, string result, CancellationToken cancellationToken)
    {
        var rawOcrResultToAdd = new RawOcrResult()
        {
            Id = id,
            Result = result
        };
        
        var additionResult = await _dbContext.RawOcrResults.AddAsync(rawOcrResultToAdd, cancellationToken);
        
        return additionResult.Entity;
    }
}