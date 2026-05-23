using CartIQ.Application.Common.Interfaces;
using CartIQ.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CartIQ.Infrastructure.Data.Repositories;

public class ReceiptItemsRepository : IReceiptItemsRepository
{
    private readonly ApplicationDbContext _dbContext;
    public ReceiptItemsRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<bool> DeleteReceiptItemsAsync(IEnumerable<Guid> receiptItemIds, CancellationToken cancellationToken)
    {
        var deletedCount = await _dbContext.ReceiptItems
            .Where(receiptItem => receiptItemIds.Contains(receiptItem.Id))
            .ExecuteDeleteAsync(cancellationToken);
        
        return deletedCount == receiptItemIds.Count();
    }

    public async Task<List<ReceiptItem>> AddReceiptItemsAsync(List<ReceiptItem> receiptItems, CancellationToken cancellationToken)
    {
        _dbContext.ReceiptItems.AddRange(receiptItems.ToArray());
        await _dbContext.SaveChangesAsync(cancellationToken);
        
        return receiptItems;
    }
}