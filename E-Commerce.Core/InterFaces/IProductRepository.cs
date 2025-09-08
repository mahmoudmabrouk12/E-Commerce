using E_Commerce.Core.DTOs;
using E_Commerce.Core.Entites.Product;
 

namespace E_Commerce.Core.InterFaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        public  Task<bool> AddAsync(AddProductDTO product);
        public Task<bool> UpdateAsync(UpdateProductDTO productDTO);
        public Task  DeleteAsync(Product product);

    }
}
