using API.Dtos;
using AutoMapper;
using Core.Entities;

namespace API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product,ProductToReturn>().
            ForMember(t => t.ProductBrand,o => o.MapFrom(s => s.ProductBrand.Name)).
            ForMember(t => t.ProductType,o => o.MapFrom(s => s.ProductType.Name)).
            ForMember(t =>t.PictureUrl,o =>o.MapFrom<ProductUrlResolver>());
        }
    }
}