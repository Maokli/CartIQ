using AutoMapper;
using CartIQ.Application.Common.Interfaces;
using CartIQ.Application.Common.Models;
using CartIQ.Application.Receipts.DTOs;
using MediatR;

namespace CartIQ.Application.Receipts.Queries.GetReceipts;

public class GetReceiptsQueryHandler : IRequestHandler<GetReceiptsQuery, Result<PaginationResult<ReceiptDto>>>
{
    private readonly IReceiptsRepository _receiptsRepository;
    private readonly IMapper _mapper;
    public GetReceiptsQueryHandler(IReceiptsRepository  receiptsRepository,  IMapper mapper)
    {
        _receiptsRepository = receiptsRepository;
        _mapper = mapper;
    }
    
    public async Task<Result<PaginationResult<ReceiptDto>>> Handle(GetReceiptsQuery request, CancellationToken cancellationToken)
    {
        var totalReceipts = await _receiptsRepository.CountAsync(cancellationToken);
        var receiptsFromDb = await _receiptsRepository.GetReceiptsAsync(request.PageIndex, request.PageSize, cancellationToken);
        var totalPages = (int)Math.Ceiling(totalReceipts / (double)request.PageSize);
        var receipts =  _mapper.Map<List<ReceiptDto>>(receiptsFromDb);
        
        var paginationResult = new PaginationResult<ReceiptDto>(
            pageIndex: request.PageIndex,
            pageSize: request.PageSize,
            itemsCount: totalReceipts,
            items: receipts,
            totalPages: totalPages
        );

        return Result<PaginationResult<ReceiptDto>>.Success(paginationResult);
    }
}