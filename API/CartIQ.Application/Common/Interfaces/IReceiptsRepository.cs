using CartIQ.Domain.Entities;

namespace CartIQ.Application.Common.Interfaces;

public interface IReceiptsRepository
{
    Task<int> CountAsync(CancellationToken cancellationToken);
    Task<List<Receipt>> GetReceiptsAsync(int pageIndex, int pageSize, CancellationToken cancellationToken);
    Task<Receipt> GetReceiptAsync(Guid id, CancellationToken cancellationToken);
    Task<bool> DeleteReceiptAsync(Guid id, CancellationToken cancellationToken);
    Task<bool> UpdateReceiptAsync(Receipt receipt, CancellationToken cancellationToken);
}