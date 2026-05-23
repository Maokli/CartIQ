using AutoMapper;
using CartIQ.Application.Common.Interfaces;
using CartIQ.Application.Common.Models;
using CartIQ.Application.Receipts.DTOs;
using MediatR;

namespace CartIQ.Application.Receipts.Queries.GetReceipt;

public class GetReceiptDetailsQueryHandler : IRequestHandler<GetReceiptDetailsQuery, Result<ReceiptDetailsDto>>
{
    private readonly IReceiptsRepository _receiptsRepository;
    private readonly IMapper _mapper;
    public GetReceiptDetailsQueryHandler(IReceiptsRepository  receiptsRepository, IMapper mapper)
    {
        _receiptsRepository = receiptsRepository;
        _mapper = mapper;
    }
    
    public async Task<Result<ReceiptDetailsDto>> Handle(GetReceiptDetailsQuery request, CancellationToken cancellationToken)
    {
        var receiptFromDb = await _receiptsRepository.GetReceiptAsync(request.Id, cancellationToken);
        var receiptDetailsDto = _mapper.Map<ReceiptDetailsDto>(receiptFromDb);
        
        return Result<ReceiptDetailsDto>.Success(receiptDetailsDto);
    }
}