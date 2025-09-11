using AutoMapper;
using E_Commerce.Core.Entites.User;
using E_Commerce.Core.InterFaces;
using E_Commerce.Core.Services;
using E_Commerce.InfraStructure.Data;
using Microsoft.AspNetCore.Identity;
using StackExchange.Redis;

namespace E_Commerce.InfraStructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private readonly IMapper mapper;
        private readonly IImageManagementService imageManagementService;
        private readonly IConnectionMultiplexer redis;
        private readonly UserManager<AppUser>  userManager;
        private readonly IEmailService emailService;
        private readonly SignInManager<AppUser> signInManager;
        private readonly IGenerateToken token;





        public ICategoryRepository CategoryRepository { get; }

        public IProductRepository ProductRepository { get; }

        public IPhotoRepository photoRepository { get; }

        public ICustomerBasketRepository CustomerBasket { get; }

        public IAuth Auth { get; }



        public UnitOfWork(AppDbContext context, IMapper mapper,
            IImageManagementService imageManagementService, IConnectionMultiplexer redis, UserManager<AppUser> userManager, IEmailService emailService, SignInManager<AppUser> signInManager, IGenerateToken token)
        {
            _context = context;
            this.mapper = mapper;
            this.imageManagementService = imageManagementService;
            this.redis = redis;
            this.userManager = userManager;
            this.emailService = emailService;
            this.signInManager = signInManager;
            this.token = token;
            CategoryRepository = new CategoryRepository(_context);
            ProductRepository = new ProductRepository(_context, mapper, imageManagementService);
            photoRepository = new PhotoRepository(_context);
            CustomerBasket = new CustomerBasketRepository(redis);
            Auth = new AuthRepository(userManager, emailService, signInManager, token);
          
        }
    }
}
