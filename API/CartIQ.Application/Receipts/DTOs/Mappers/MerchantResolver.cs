using AutoMapper;
using CartIQ.Application.Common.Utils;
using CartIQ.Domain.Entities;

namespace CartIQ.Application.Receipts.DTOs.Mappers;


public class MerchantResolver : IValueResolver<Receipt, ReceiptDto, MerchantDto>
{
    public MerchantDto Resolve(Receipt source, ReceiptDto destination, MerchantDto destMember, ResolutionContext context)
    {
        var (backgroundColor, textColor) = AvatarColorGenerator.Generate(source.Merchant);

        return new MerchantDto()
        {
            BackgroundColor = backgroundColor,
            Color = textColor,
            Name = source.Merchant
        };
    }
}