using E_Commerce.Core.DTOs;
using E_Commerce.Core.Entites.Order;

namespace E_Commerce.Core.Services
{
    public interface IOrderService
    {
        Task<Orders> CreateOrderAsync(OrderDTO orderDTO  , string BuyerEmail);
        Task<IReadOnlyList<OrderToReturnDTO>> GetAllOrdersForUserAsync(string BuyerEmail);
        Task<OrderToReturnDTO> GetOrdersByIdAsync(int  id , string BuyerEmail) ;
        Task<IReadOnlyList<DelivaryMethod>> GetDelivaryMethodAsync();


    }
}
