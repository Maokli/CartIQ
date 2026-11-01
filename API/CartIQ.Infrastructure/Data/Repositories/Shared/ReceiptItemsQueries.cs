using Ardalis.GuardClauses;
using CartIQ.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CartIQ.Infrastructure.Data.Repositories.Shared;

// TODO: Refactor this spaghetti
public static class ReceiptItemsQueries
{
    public static async Task DeleteReceiptItemsAsync(this ApplicationDbContext dbContext, 
        IEnumerable<Guid> receiptItemIds,
        CancellationToken cancellationToken)
    {
        var receiptItems = await dbContext.ReceiptItems
            .Where(receiptItem => receiptItemIds.Contains(receiptItem.Id))
            .ToListAsync(cancellationToken);
        
        foreach (var receiptItemId in receiptItemIds)
        {
            var receiptItem = receiptItems.FirstOrDefault(receiptItem => receiptItem.Id == receiptItemId);
            Guard.Against.NotFound(receiptItemId, receiptItem);
        }
        
        dbContext.ReceiptItems.RemoveRange(receiptItems);
    }
    
    public static void AddReceiptItems(this ApplicationDbContext dbContext, 
        IReadOnlyList<ReceiptItem> receiptItems, 
        CancellationToken cancellationToken)
    {
        dbContext.ReceiptItems.AddRange(receiptItems.ToArray());
    }
}