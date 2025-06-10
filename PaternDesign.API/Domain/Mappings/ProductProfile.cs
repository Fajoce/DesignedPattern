using AutoMapper;
using PaternDesign.API.Domain.DTOs;
using PaternDesign.API.Domain.Entities;

namespace PaternDesign.API.Domain.Mappings
{
    public class ProductProfile: Profile
    {
        public ProductProfile()
        {
            CreateMap<Products, ProductDTO>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.ProductName))
                .ForMember(dest => dest.ProductDescription, opt => opt.MapFrom(src => src.ProductDescription))
                .ForMember(dest => dest.ProductPrice, opt => opt.MapFrom(src => src.ProductPrice));

            CreateMap<CreateProductDTO, Products>();
            CreateMap<UpdateProductDTO, Products>()
                .ForMember(dest => dest.ProductDescription, opt => opt.MapFrom(src => src.ProductDescription)); 
        }
    }
}
