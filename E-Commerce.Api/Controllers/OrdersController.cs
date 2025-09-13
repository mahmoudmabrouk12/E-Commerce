using E_Commerce.Core.DTOs;
using E_Commerce.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace E_Commerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService orderService;

        public OrdersController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        [HttpPost]
        [Route("Create-Order")]
        public async Task<IActionResult> Create(OrderDTO orderDTO)
        {
            var email = User .FindFirst( ClaimTypes .Email )?.Value ;
            var order = await orderService .CreateOrderAsync(orderDTO, email);
            return Ok(order);
        }
        [HttpGet]
        [Route("Get-Orders-From-User")]
        public async Task<IActionResult> GetOrders(OrderDTO orderDTO)
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            var order = await orderService.GetAllOrdersForUserAsync( email);
            return Ok(order);
        }
        [HttpGet]
        [Route("Get-Orders-By-Id/{Id}")]
        public async Task<IActionResult> GetOrderById( int Id)
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            var order = await orderService.GetOrdersByIdAsync(Id,email);
            return Ok(order);
        }
        [HttpGet]
        [Route("Get-Delivary")]
        public async Task<IActionResult> GetDelivary()
            => Ok(await orderService.GetDelivaryMethodAsync());
       
    }
}
