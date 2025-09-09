using AutoMapper;
using E_Commerce.Core.DTOs;
using E_Commerce.Core.Entites.Product;
using E_Commerce.Core.InterFaces;
using E_Commerce.Core.Services;
using E_Commerce.Core.Sharing;
using E_Commerce.InfraStructure.Data;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.InfraStructure.Repository
{
    public class ProductRepository : GenericRepository<Product> , IProductRepository
    {
        private readonly AppDbContext context;
        private readonly IMapper mapper;
        private readonly IImageManagementService imageManagementService;
        public ProductRepository(AppDbContext context, IMapper mapper, IImageManagementService imageManagementService) : base(context)
        {
            this.context = context;
            this.mapper = mapper;
            this.imageManagementService = imageManagementService;
        }

        public async Task<ReturnProductDTO> GetAllAsync(ProductParams productParams)
        {
            var query = context.Products.Include(l => l.category)
                .Include(l => l.photos).AsNoTracking();

            if (!string.IsNullOrEmpty(productParams.Search))
            {
                var searchOfWords = productParams.Search
                    .Split(' ', StringSplitOptions.RemoveEmptyEntries);

                query = query.Where(l =>
                    searchOfWords.All(word =>
                        l.Name.ToLower().Contains(word.ToLower()) ||
                        l.Description.ToLower().Contains(word.ToLower())
                    )
                );
            }



            if (productParams.categoryId.HasValue)
            {
                query = query.Where(l => l.CategoryId == productParams.categoryId);

            }

            if (!string.IsNullOrEmpty(productParams.Sort))
            {
                query = productParams.Sort switch
                {
                    "PriceAce" => query.OrderBy(l => l.NewPrice),
                    "PriceDce" => query.OrderByDescending(l => l.OldPrice),
                    _ => query.OrderBy(l => l.Name)
                };
            }

            ReturnProductDTO returnProductDTO = new ReturnProductDTO();
            returnProductDTO.TotalCount = query.Count();

             query = query.Skip((productParams.PageSize) * (productParams.PageNumber - 1) ).Take(productParams.PageSize);



            returnProductDTO.Products = mapper.Map<List<ProductDTO>>(query);
            return returnProductDTO;



        }
        public async Task<bool> AddAsync(AddProductDTO  productDTO)
        {
            if (productDTO == null) return false;
          
            var product = mapper.Map<Product>(productDTO);

            await context.Products.AddAsync(product);
            await context.SaveChangesAsync();

            var ImageSrc = await imageManagementService.AddImageAsync(productDTO.Photos , productDTO.Name);

            var photo = ImageSrc.Select(path => new Photo 
            {
            ImageName = path,
            ProductId = product.Id
            });
            return true;

        }

        public async Task DeleteAsync(Product product)
        {
            var Photo = await context.Photos.Where(l=>l.ProductId == product.Id).ToListAsync();
            foreach (var item in Photo)
            {
                imageManagementService.DeleteImageAsync(item.ImageName);
            }
            context.Products.Remove(product);
            await context.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(UpdateProductDTO productDTO)
        {
            if (productDTO == null) return false;

            var FindProduct = await context.Products
                .Include(l => l.category)
                .Include(l => l.photos)
                .FirstOrDefaultAsync(l => l.Id == productDTO.Id);

            if (FindProduct == null) return false;

            mapper.Map(productDTO, FindProduct);

            var FindPhoto = await context.Photos
                .Where(l => l.ProductId == productDTO.Id)
                .ToListAsync();

            foreach (var item in FindPhoto)
            {
                 imageManagementService.DeleteImageAsync(item.ImageName);
            }

            context.Photos.RemoveRange(FindPhoto);

            var ImagePath = await imageManagementService.AddImageAsync(productDTO.Photos, productDTO.Name);

            var Photo = ImagePath.Select(path => new Photo
            {
                ImageName = path,
                ProductId = productDTO.Id
            }).ToList();

            await context.AddRangeAsync(Photo);
            await context.SaveChangesAsync();

            return true;
        }
        
    }
    
}
