using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Entities.Identity;

namespace API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product,ProductToReturn>().
            ForMember(t => t.ProductBrand,o => o.MapFrom(s => s.ProductBrand.Name)).
            ForMember(t => t.ProductType,o => o.MapFrom(s => s.ProductType.Name)).
             ForMember(t => t.ProductState,o => o.MapFrom(s => s.ProductState.Name)).
            ForMember(t =>t.PictureUrl,o =>o.MapFrom<ProductUrlResolver>());
            CreateMap<Address,AddressDto>().ReverseMap();
            CreateMap<CustomerBasketDto,CustomerBasket>();
            CreateMap<BasketItemDto,BasketItem>();
            CreateMap<AddressDto, Core.Entities.OrderAggregate.Address>();
        }
    }
}