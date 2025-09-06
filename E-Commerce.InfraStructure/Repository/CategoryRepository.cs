using E_Commerce.Core.Entites.Product;
using E_Commerce.Core.InterFaces;
using E_Commerce.InfraStructure.Data;

namespace E_Commerce.InfraStructure.Repository
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context)
        {
        }
    }
}
