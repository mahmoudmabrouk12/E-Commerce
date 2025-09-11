using E_Commerce.Core.Entites.User;
using E_Commerce.Core.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace E_Commerce.InfraStructure.Repository.Service
{
    public class GenerateToken : IGenerateToken
    {
        private readonly IConfiguration configuration;

        public GenerateToken(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public  string GetAndCreateToken(AppUser user)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name , user.UserName),
                new Claim( ClaimTypes.Email , user.Email)
            };

            var Security = configuration["Token:Secret"];
            var Key = Encoding.ASCII.GetBytes(Security);
            SigningCredentials credentials = new SigningCredentials(new SymmetricSecurityKey(Key), SecurityAlgorithms.HmacSha256);

            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = credentials,
                Issuer = configuration["Token:Issuer"],
                NotBefore = DateTime.Now


            };
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            var token = handler.CreateToken(descriptor);
            return handler.WriteToken(token);
        }
    }
}