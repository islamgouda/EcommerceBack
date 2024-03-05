using api.Dtos;
using AutoMapper;
using Core.Entities;

namespace api.Helper
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Product,ProductToReturnDto>()
                .ForMember(dest=>dest.productBrand,src=>src.MapFrom(e=>e.productBrand.Name))
                .ForMember(dest => dest.ProductType, src => src.MapFrom(e => e.ProductType.Name))
                .ForMember(dest => dest.PictureUrl, src => src.MapFrom<ProductUrlResolver>());
        }
    }
}
