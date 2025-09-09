using E_Commerce.Core.Entites.Basket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.InterFaces
{
    public interface ICustomerBasketRepository
    {
        public Task<CustomerBasket> GetBasketAsync(string Id);
        public Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket);
        public Task<bool> DeleteBasketAsync(string Id);
    }
}
