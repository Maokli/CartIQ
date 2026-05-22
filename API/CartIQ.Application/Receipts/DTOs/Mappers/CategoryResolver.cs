using AutoMapper;
using CartIQ.Domain.Entities;

namespace CartIQ.Application.Receipts.DTOs.Mappers;

public class CategoryResolver : IValueResolver<Receipt, ReceiptDto, string>
{
    public string Resolve(Receipt source, ReceiptDto destination, string destMember, ResolutionContext context)
    {
        // TODO: implement some logic some time
        return "Groceries";
    }
}