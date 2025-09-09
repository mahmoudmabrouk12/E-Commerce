using AutoMapper;
using E_Commerce.Core.InterFaces;
using E_Commerce.Core.Services;
using E_Commerce.InfraStructure.Data;
using StackExchange.Redis;

namespace E_Commerce.InfraStructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private readonly IMapper mapper;
        private readonly IImageManagementService imageManagementService;
        private readonly IConnectionMultiplexer redis;

        public ICategoryRepository CategoryRepository { get; }

        public IProductRepository ProductRepository { get; }

        public IPhotoRepository photoRepository { get; }

        public ICustomerBasketRepository CustomerBasket { get; }

        public UnitOfWork(AppDbContext context, IMapper mapper,
            IImageManagementService imageManagementService, IConnectionMultiplexer redis)
        {
            _context = context;
            this.mapper = mapper;
            this.imageManagementService = imageManagementService;
            this.redis = redis;
            CategoryRepository = new CategoryRepository(_context);
            ProductRepository = new ProductRepository(_context, mapper, imageManagementService);
            photoRepository = new PhotoRepository(_context);
            CustomerBasket = new CustomerBasketRepository(redis);


        }
    }
}
