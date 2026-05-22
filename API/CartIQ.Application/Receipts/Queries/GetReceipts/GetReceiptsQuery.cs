using CartIQ.Application.Common.Models;
using CartIQ.Application.Receipts.DTOs;
using MediatR;

namespace CartIQ.Application.Receipts.Queries.GetReceipts;

public class GetReceiptsQuery : IRequest<Result<PaginationResult<ReceiptDto>>>
{
    public int PageIndex { get; set; }
    public int PageSize { get; set; }

    public GetReceiptsQuery(int pageIndex, int pageSize)
    {
        PageIndex = pageIndex;
        PageSize = pageSize;
    }
}