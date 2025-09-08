using AutoMapper;
using E_Commerce.Core.DTOs;
using E_Commerce.Core.Entites.Product;

namespace E_Commerce.Api.Mapping
{
    public class CategoryMapping : Profile
    {
        public CategoryMapping()
        {
            CreateMap<CategoryDTO,Category>().ReverseMap();
            CreateMap<UpdateCatgoryDTO , Category>().ReverseMap();
        }
    }
}
