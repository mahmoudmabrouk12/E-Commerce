using AutoMapper;
using E_Commerce.Core.InterFaces;
using E_Commerce.Core.Services;
using E_Commerce.InfraStructure.Data;

namespace E_Commerce.InfraStructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private readonly IMapper mapper;
        private readonly IImageManagementService imageManagementService;

        public ICategoryRepository CategoryRepository { get; }

        public IProductRepository ProductRepository { get; }

        public IPhotoRepository photoRepository { get; }


        public UnitOfWork(AppDbContext context, IMapper mapper, IImageManagementService imageManagementService)
        {
            _context = context;
            this.mapper = mapper;
            this.imageManagementService = imageManagementService;
            CategoryRepository = new CategoryRepository(_context);
            ProductRepository = new ProductRepository(_context, mapper, imageManagementService);
            photoRepository = new PhotoRepository(_context);
            
        }
    }
}
