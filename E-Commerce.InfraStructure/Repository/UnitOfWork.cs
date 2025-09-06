using E_Commerce.Core.InterFaces;
using E_Commerce.InfraStructure.Data;

namespace E_Commerce.InfraStructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public ICategoryRepository CategoryRepository { get; }

        public IProductRepository ProductRepository { get; }

        public IPhotoRepository photoRepository { get; }


        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            CategoryRepository = new CategoryRepository(_context);
            ProductRepository  = new ProductRepository(_context);
            photoRepository    = new PhotoRepository(_context);


        }
    }
}
