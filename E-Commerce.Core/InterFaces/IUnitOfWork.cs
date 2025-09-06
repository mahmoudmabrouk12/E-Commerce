
namespace E_Commerce.Core.InterFaces
{
    public interface IUnitOfWork
    {
        public ICategoryRepository CategoryRepository {get;}
        public IProductRepository ProductRepository {get;}
        public IPhotoRepository photoRepository { get;} 

    }
}
