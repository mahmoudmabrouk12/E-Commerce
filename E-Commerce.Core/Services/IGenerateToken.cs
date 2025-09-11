
using E_Commerce.Core.Entites.User;

namespace E_Commerce.Core.Services
{
    public  interface IGenerateToken
    {
        public string GetAndCreateToken(AppUser user);
    }
}
