using AutoMapper;
using CartIQ.Domain.Entities;

namespace CartIQ.Application.Receipts.DTOs.Mappers;

public class TopItemsResolver : IValueResolver<Receipt, ReceiptDto, List<string>>
{
    public List<string> Resolve(Receipt source, ReceiptDto destination, List<string> destMember, ResolutionContext context)
    {
        return source.Items
            .OrderByDescending(i => i.ProductPrice)
            .Select(i => i.ProductName)
            .ToList();
    }
}