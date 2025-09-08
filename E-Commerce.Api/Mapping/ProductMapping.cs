using AutoMapper;
using E_Commerce.Core.DTOs;
using E_Commerce.Core.Entites.Product;

namespace E_Commerce.Api.Mapping
{
    public class ProductMapping : Profile
    {
        public ProductMapping()
        {
            CreateMap<Product , ProductDTO>()
                .ForMember(l=>l.CategoryName , ops => ops.MapFrom(s=>s.category.Name))
                .ReverseMap();
            
            CreateMap<PhotoDTO , Photo> () .ReverseMap();

            CreateMap<AddProductDTO, Product>()
                .ForMember(l=>l.photos , Option => Option.Ignore()).ReverseMap();

            CreateMap<UpdateProductDTO, Product>()
               .ForMember(l => l.photos, Option => Option.Ignore()).ReverseMap();

        }
    }
}
