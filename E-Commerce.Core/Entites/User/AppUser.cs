using Microsoft.AspNetCore.Identity;
namespace E_Commerce.Core.Entites.User
{
    public class AppUser : IdentityUser
    {
        public string DisplayName { get; set; }
        public Address Address { get; set; }
    }
}
