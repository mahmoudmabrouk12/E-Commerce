using E_Commerce.Core.Entites.User;
using E_Commerce.Core.InterFaces;
using E_Commerce.Core.Services;
using E_Commerce.InfraStructure.Data;
using E_Commerce.InfraStructure.Repository;
using E_Commerce.InfraStructure.Repository.Service;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using System.Text;

namespace E_Commerce.InfraStructure
{
    public static class TheInFraStrutureRegisteration 
    {
        public static IServiceCollection InfraStructure(this IServiceCollection Service , IConfiguration Configuration)
        {
            Service.AddScoped(typeof( IGenericRepository<> ), typeof(GenericRepository<>));
            Service.AddScoped<IUnitOfWork, UnitOfWork>();
            Service.AddScoped<IEmailService , EmailService>();
            Service.AddScoped<IGenerateToken, GenerateToken>();
            Service.AddScoped<IImageManagementService, ImageManagementService>();
            Service.AddScoped<IOrderService, OrderService>();
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

            Service.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

            Service.AddAuthentication(op =>
            {
                op.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                op.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                op.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
                   .AddCookie(op=> 
                {
                    op.Cookie.Name = "token";
                    op.Events.OnRedirectToLogin = context =>
                    {
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        return Task.CompletedTask;

                    };
                
                })
                   .AddJwtBearer(op =>
                   {
                       op.RequireHttpsMetadata = false;
                       op.SaveToken = true;
                       op.TokenValidationParameters = new TokenValidationParameters
                       {
                           ValidateIssuer = true,
                           ValidateAudience = false,
                           ValidateIssuerSigningKey = true,
                           ValidIssuer = Configuration["Token:Issuer"],
                           ClockSkew = TimeSpan.Zero,
                           IssuerSigningKey = new SymmetricSecurityKey(
                          Encoding.UTF8.GetBytes(Configuration["Token:Secret"]))
                       };

                       op.Events = new JwtBearerEvents()
                       {
                           OnMessageReceived = context =>
                           {
                               context.Token = context.Request.Cookies["token"];
                               return Task.CompletedTask;
                           }
                       };




                   });
           
            return Service;
        }
    }
}
