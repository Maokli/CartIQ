using CartIQ.Domain.Entities;

namespace CartIQ.Application.Common.Interfaces;

public interface IReceiptItemsRepository
{
    public Task<bool> DeleteReceiptItemsAsync(IEnumerable<Guid> receiptItemIds, CancellationToken cancellationToken);
    public Task<List<ReceiptItem>> AddReceiptItemsAsync(List<ReceiptItem> receiptItems, CancellationToken cancellationToken);
}