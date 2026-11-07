using Ardalis.GuardClauses;
using CartIQ.Application.Common.Interfaces;
using CartIQ.Domain.Entities;
using CartIQ.Domain.Enums;
using CartIQ.Infrastructure.Data.Repositories.Shared;
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

    public async Task<Receipt> AddEmptyAsync(Guid fileId, CancellationToken cancellationToken)
    {
        var emptyReceipt = new Receipt(fileId)
        {
            // TODO: fix ugly meess
            CreatedBy = "louay",
            Created =  DateTime.UtcNow,
            LastModifiedBy = "louay",
            LastModified =  DateTime.UtcNow,
        };
        
        _dbContext.Receipts.Add(emptyReceipt);
        await _dbContext.UpdateFileStateAsync(fileId, FileState.ReceiptCreated, cancellationToken);
        
        await  _dbContext.SaveChangesAsync(cancellationToken);
        
        return emptyReceipt;
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

    public async Task<Receipt> GetReceiptAsync(Guid id, CancellationToken cancellationToken)
    {
        var receipt = await _dbContext.Receipts
            .Include(r => r.Items)
            .Include(r => r.FileMetadata)
            .FirstOrDefaultAsync(receipt => receipt.Id == id, cancellationToken);
        
        Guard.Against.NotFound(id, receipt);
        
        return receipt;
    }

    public async Task<bool> DeleteReceiptAsync(Guid id, CancellationToken cancellationToken)
    {
        var receipt = await GetReceiptAsync(id, cancellationToken);
        
        _dbContext.Receipts.Remove(receipt);
        
        return await _dbContext.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task<bool> UpdateReceiptAsync(Guid receiptId,
        string newMerchant,
        string newCurrency,
        IReadOnlyList<Guid> itemIdsToDelete, 
        IReadOnlyList<ReceiptItem> itemsToAdd, 
        CancellationToken cancellationToken)
    {
        var receiptToUpdate = await GetReceiptAsync(receiptId, cancellationToken);
        receiptToUpdate.Currency = newCurrency;
        receiptToUpdate.Merchant = newMerchant;
        
        _dbContext.Receipts.Update(receiptToUpdate);
        
        await _dbContext.DeleteReceiptItemsAsync(itemIdsToDelete, cancellationToken);
        _dbContext.AddReceiptItems(itemsToAdd, cancellationToken);
         
        return  await _dbContext.SaveChangesAsync(cancellationToken) > 0;
    }
}