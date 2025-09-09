using E_Commerce.Core.InterFaces;
using E_Commerce.Core.Services;
using E_Commerce.InfraStructure.Data;
using E_Commerce.InfraStructure.Repository;
using E_Commerce.InfraStructure.Repository.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using StackExchange.Redis;

namespace E_Commerce.InfraStructure
{
    public static class TheInFraStrutureRegisteration 
    {
        public static IServiceCollection InfraStructure(this IServiceCollection Service , IConfiguration Configuration)
        {
            Service.AddScoped(typeof( IGenericRepository<> ), typeof(GenericRepository<>));
            Service.AddScoped<IUnitOfWork, UnitOfWork>();
            Service.AddScoped<IImageManagementService, ImageManagementService>();
            Service.AddSingleton<IConnectionMultiplexer>(i =>
            {
                var config = Configuration.GetConnectionString("redis");
                return ConnectionMultiplexer.Connect(config);

            });

            Service.AddSingleton<IFileProvider>(
                new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"))
            );
            Service.AddDbContext<AppDbContext>(l =>
            {
                l.UseSqlServer(Configuration.GetConnectionString("Ecommerce"));
            });

            return Service;
        }
    }
}
