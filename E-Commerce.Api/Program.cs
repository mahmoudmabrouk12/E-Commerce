
using E_Commerce.Api.Middleware;
using E_Commerce.InfraStructure;
namespace E_Commerce.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("CORSPolicy", policyBuilder =>
                {
                    policyBuilder.AllowAnyHeader()
                                 .AllowAnyMethod()
                                 .AllowCredentials()
                                 .WithOrigins("http://localhost:4200"); 
                });
            });

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddMemoryCache();
            builder.Services.InfraStructure(builder.Configuration);
            builder.Services.AddAutoMapper(cfg => { }, AppDomain.CurrentDomain.GetAssemblies());

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors("CORSPolicy");
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseStatusCodePagesWithReExecute("/Errors/{0}");
            app.UseHttpsRedirection();
            app.MapControllers();

            app.Run();
        }
    }
}
