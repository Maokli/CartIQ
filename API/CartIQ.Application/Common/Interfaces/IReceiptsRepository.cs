using CartIQ.Domain.Entities;

namespace CartIQ.Application.Common.Interfaces;

public interface IReceiptsRepository
{
    Task<int> CountAsync(CancellationToken cancellationToken);
    Task<Receipt> AddEmptyAsync(Guid fileId, CancellationToken cancellationToken);
    Task<List<Receipt>> GetReceiptsAsync(int pageIndex, int pageSize, CancellationToken cancellationToken);
    Task<Receipt> GetReceiptAsync(Guid id, CancellationToken cancellationToken);
    Task<bool> DeleteReceiptAsync(Guid id, CancellationToken cancellationToken);
    Task<bool> UpdateReceiptAsync(Guid receiptId,
        string newMerchant,
        string newCurrency,
        IReadOnlyList<Guid> itemIdsToDelete, 
        IReadOnlyList<ReceiptItem> itemsToAdd, 
        CancellationToken cancellationToken);
}