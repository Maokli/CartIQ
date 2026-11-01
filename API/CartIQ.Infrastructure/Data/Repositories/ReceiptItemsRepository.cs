using Ardalis.GuardClauses;
using CartIQ.Application.Common.Interfaces;
using CartIQ.Domain.Entities;
using CartIQ.Infrastructure.Data.Repositories.Shared;
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
        await _dbContext.DeleteReceiptItemsAsync(receiptItemIds, cancellationToken);
        
        var result = await _dbContext.SaveChangesAsync(cancellationToken);
        if (result <= 0)
        {
            throw new DbUpdateException();
        }

        return true;
    }

    public async Task<List<ReceiptItem>> AddReceiptItemsAsync(List<ReceiptItem> receiptItems, CancellationToken cancellationToken)
    {
        _dbContext.AddReceiptItems(receiptItems, cancellationToken);
        var result = await _dbContext.SaveChangesAsync(cancellationToken);

        if (result <= 0)
        {
            throw new DbUpdateException();
        }
        
        return receiptItems;
    }
}