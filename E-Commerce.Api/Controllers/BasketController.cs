using AutoMapper;
using E_Commerce.Api.Helper;
using E_Commerce.Core.Entites.Basket;
using E_Commerce.Core.InterFaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Api.Controllers
{

    public class BasketController : BaseController
    {
        public BasketController(IUnitOfWork work, IMapper mapper) : base(work, mapper)
        {
        }

        [HttpGet]
        [Route("Get-Basket-Item/{Id}")]
        public async Task<IActionResult> GetBasketItem(string Id)
        {
            var result = await work.CustomerBasket.GetBasketAsync(Id);
            if (result is null)
                return Ok ( new CustomerBasket());
            return Ok( result );    
        }
        [HttpPut]
        [Route("Update-Basket-Item")]
        public async Task<IActionResult> Update(CustomerBasket customerBasket )
        {
            var result = await work.CustomerBasket.UpdateBasketAsync(customerBasket);
            return Ok( result );

        }
        [HttpDelete]
        [Route("Delete-Basket-Item")]
        public async Task<IActionResult> Delete(string  Id)
        {
            var result = await work.CustomerBasket.DeleteBasketAsync(Id);
            return result ? Ok( new ResponseApi(200 , "The Item Is Deleted") ) 
                : BadRequest(new ResponseApi(400 , "The Item Not Deleted"));    

        }



    }

}
