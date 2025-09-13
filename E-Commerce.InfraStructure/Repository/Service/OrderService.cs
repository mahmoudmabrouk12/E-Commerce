using AutoMapper;
using E_Commerce.Core.DTOs;
using E_Commerce.Core.Entites.Order;
using E_Commerce.Core.InterFaces;
using E_Commerce.Core.Services;
using E_Commerce.InfraStructure.Data;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.InfraStructure.Repository.Service
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public OrderService(IUnitOfWork unitOfWork, AppDbContext context, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _context = context;
            _mapper = mapper;
        }

        public async Task<Orders> CreateOrderAsync(OrderDTO orderDTO, string BuyerEmail)
        {
            var basket = await _unitOfWork.CustomerBasket.GetBasketAsync(orderDTO.basketId);

            List<OrderItems> orderItems = new List<OrderItems>();

            foreach (var item in basket.BaketItems)
            {
                var product = await _unitOfWork.ProductRepository.GetByIdAsync(item.Id);

                var orderItem = new OrderItems(product.Name, item.Price, item.Quentity, product.Id, item.Image);
                orderItems.Add(orderItem);
            }

            var delivaryMethod = await _context.DelivaryMethods.FirstOrDefaultAsync(l=>l.Id == orderDTO .delivaryMethodeId);

            var SubTotal = orderItems.Sum(m => m.Price * m.Quentity);

            var Ship = _mapper.Map<ShippingAddress>(orderDTO.shippAdressDTO);

            var order = new Orders(BuyerEmail , SubTotal, Ship,  orderItems , delivaryMethod);

            await _context.Orders.AddAsync(order);

            await  _context.SaveChangesAsync();

           await _unitOfWork.CustomerBasket.DeleteBasketAsync(orderDTO.basketId);    

            return order;
        }

        public async Task<IReadOnlyList<OrderToReturnDTO>> GetAllOrdersForUserAsync(string BuyerEmail)
        {
            var orders = await _context.Orders.Where(z => z.BuyerEmail == BuyerEmail)
                .Include(l => l.orderItems).Include(d => d.delivaryMethod).ToListAsync();
            var result = _mapper.Map<IReadOnlyList<OrderToReturnDTO>>(orders);
            return result;
        }

        public async Task<IReadOnlyList<DelivaryMethod>> GetDelivaryMethodAsync()
        => await _context.DelivaryMethods.AsNoTracking().ToListAsync();

        public async Task<OrderToReturnDTO> GetOrdersByIdAsync(int id, string BuyerEmail)
        {
            var order = await _context.Orders.Where(l => l.Id == id && l.BuyerEmail == BuyerEmail).
                Include(l => l.orderItems).Include(d => d.delivaryMethod).FirstOrDefaultAsync();
            var result = _mapper.Map<OrderToReturnDTO>(order);
            return result;
        }
    }
}
