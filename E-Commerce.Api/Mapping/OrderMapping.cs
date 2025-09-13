using AutoMapper;
using E_Commerce.Core.DTOs;
using E_Commerce.Core.Entites.Order;
using E_Commerce.Core.Entites.User;

namespace E_Commerce.Api.Mapping
{
    public class OrderMapping : Profile
    {
        public OrderMapping() 
        {
            CreateMap<Orders, OrderToReturnDTO>().ForMember(d=>d.DelivaryMethod , o=>o.MapFrom(
                s=>s.delivaryMethod.Name)).ReverseMap();
            CreateMap<OrderItems, OrderItemsDTO>().ReverseMap();
            CreateMap<ShippingAddress , ShippAdressDTO>().ReverseMap();

            CreateMap<Address, ShippAdressDTO>().ReverseMap();

        }
    }
}
