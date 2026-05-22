using AutoMapper;
using CartIQ.Application.Common.Utils;
using CartIQ.Application.Receipts.DTOs.Mappers;
using CartIQ.Domain.Entities;

namespace CartIQ.Application.Receipts.DTOs;

public class MappingProfile : Profile 
{
    public MappingProfile()
    {
        CreateMap<Receipt, ReceiptDto>()
            .ForMember(dest => dest.Merchant, opt => opt.MapFrom<MerchantResolver>())
            .ForMember(dest => dest.Nutrients, opt => opt.MapFrom<NutrientsResolver>())
            .ForMember(dest => dest.TopItems, opt => opt.MapFrom<TopItemsResolver>())
            .ForMember(dest => dest.Category, opt => opt.MapFrom<CategoryResolver>());
        CreateMap<ReceiptItem, ReceiptItemDto>();
        CreateMap<Receipt, ReceiptDetailsDto>()
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));
    }
}