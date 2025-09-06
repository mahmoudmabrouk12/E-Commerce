using E_Commerce.Core.Entites.Product;
using E_Commerce.Core.InterFaces;
using E_Commerce.InfraStructure.Data;

namespace E_Commerce.InfraStructure.Repository
{
    public class ProductRepository : GenericRepository<Product> , IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context)
        {

        }
    }
}
