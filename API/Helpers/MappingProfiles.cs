using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Entities.Identity;
using Core.Entities.OrderAggregate;

namespace API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductToReturn>().
            ForMember(t => t.ProductBrand, o => o.MapFrom(s => s.ProductBrand.Name)).
            ForMember(t => t.ProductType, o => o.MapFrom(s => s.ProductType.Name)).
             ForMember(t => t.ProductState, o => o.MapFrom(s => s.ProductState.Name)).
            ForMember(t => t.PictureUrl, o => o.MapFrom<ProductUrlResolver>());
            CreateMap<Core.Entities.Identity.Address, AddressDto>().ReverseMap();
            CreateMap<CustomerBasketDto, CustomerBasket>();
            CreateMap<BasketItemDto, BasketItem>();
            CreateMap<AddressDto, Core.Entities.OrderAggregate.Address>();
            CreateMap<Order, OrderToReturnDto>().
            ForMember(t => t.DeliveryMethod, o => o.MapFrom(s => s.DeliveryMethod.ShortName)).
            ForMember(t => t.ShippingPrice, o => o.MapFrom(s => s.DeliveryMethod.Price));
            CreateMap<OrderItem, OrderItemDto>().
            ForMember(o=>o.ProductId, t => t.MapFrom(s =>s.ItemOrdered.ProductItemId)).
            ForMember(o=>o.ProductName, t => t.MapFrom(s =>s.ItemOrdered.ProductName)).
            ForMember(o=>o.PictureUrl, t => t.MapFrom(s =>s.ItemOrdered.PictureUrl)).
            ForMember(o=>o.PictureUrl, d=> d.MapFrom<OrderItemUrlResolved>());
        }
    }
}