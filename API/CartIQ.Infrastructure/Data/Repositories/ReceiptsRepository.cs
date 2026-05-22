using CartIQ.Application.Common.Interfaces;
using CartIQ.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CartIQ.Infrastructure.Data.Repositories;

public class ReceiptsRepository : IReceiptsRepository
{
    private readonly ApplicationDbContext _dbContext;
    public ReceiptsRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public Task<int> CountAsync(CancellationToken cancellationToken)
    {
        return _dbContext.Receipts.CountAsync(cancellationToken);
    }

    public Task<List<Receipt>> GetReceiptsAsync(int pageIndex, int pageSize, CancellationToken cancellationToken)
    {
        return _dbContext.Receipts
            .Skip(pageIndex*pageSize)
            .Take(pageSize)
            .Include(r => r.Items)
            .ThenInclude(i => i.Product)
            .ToListAsync(cancellationToken);
    }

    public Task<Receipt> GetReceiptAsync(Guid id, CancellationToken cancellationToken)
    {
        return _dbContext.Receipts
            .Include(r => r.Items)
            .FirstOrDefaultAsync(receipt => receipt.Id == id, cancellationToken);
    }

    public async Task<bool> DeleteReceiptAsync(Guid id, CancellationToken cancellationToken)
    {
        var deletedCount = await _dbContext.Receipts
            .Where(receipt => receipt.Id == id)
            .ExecuteDeleteAsync();
        
        return deletedCount > 0;
    }

    public async Task<bool> UpdateReceiptAsync(Receipt receipt, CancellationToken cancellationToken)
    {
         var updatedCount = await _dbContext.Receipts
            .Where(r => r.Id == receipt.Id)
            .ExecuteUpdateAsync(setters => 
                setters.SetProperty(r => r.Currency, receipt.Currency)
                    .SetProperty(r => r.Merchant, receipt.Merchant),
                cancellationToken);
         
         return updatedCount > 0;
    }
}