using E_Commerce.Core.InterFaces;
using E_Commerce.InfraStructure.Data;
using E_Commerce.InfraStructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace E_Commerce.InfraStructure
{
    public static class TheInFraStrutureRegisteration 
    {
        public static IServiceCollection InfraStructure(this IServiceCollection Service , IConfiguration Configuration)
        {
            Service.AddScoped(typeof( IGenericRepository<> ), typeof(GenericRepository<>));
            Service.AddScoped<IUnitOfWork, UnitOfWork>();
            Service.AddDbContext<AppDbContext>(l =>
            {
                l.UseSqlServer(Configuration.GetConnectionString("Ecommerce"));
            });

            return Service;
        }
    }
}
