using E_Commerce.Core.DTOs;
using E_Commerce.Core.Entites.Product;
using E_Commerce.Core.Sharing;


namespace E_Commerce.Core.InterFaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        public Task<ReturnProductDTO> GetAllAsync(ProductParams productParams );
        public Task<bool> AddAsync(AddProductDTO product);
        public Task<bool> UpdateAsync(UpdateProductDTO productDTO);
        public Task  DeleteAsync(Product product);

    }
}
