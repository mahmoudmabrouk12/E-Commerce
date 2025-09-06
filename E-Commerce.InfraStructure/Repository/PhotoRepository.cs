using E_Commerce.Core.Entites.Product;
using E_Commerce.Core.InterFaces;
using E_Commerce.InfraStructure.Data;
namespace E_Commerce.InfraStructure.Repository
{
    public class PhotoRepository : GenericRepository<Photo>, IPhotoRepository
    {
        public PhotoRepository(AppDbContext context) : base(context)
        {
        }
    }
}
