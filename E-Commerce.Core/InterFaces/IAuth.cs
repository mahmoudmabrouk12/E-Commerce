
using E_Commerce.Core.DTOs;
using E_Commerce.Core.Entites.User;

namespace E_Commerce.Core.InterFaces
{
    public interface IAuth
    {
        public  Task<string> RegisterAsync(RegisterDTO registerDTO);
        public  Task<string> LoginAsync(LoginDTO login);
        public  Task<bool> SendEmailForForgetPassWord(string email);
        public  Task<string> ResetPassword(ResetPasswordDTO resetPasswordDTO);
        public  Task<bool> ActiveAccount(ActiveAccountDTO activeAccountDTO);
        public Task<bool> UpdateAddress(string email , Address address);
        public Task<Address> GetUserAddress( string email);


    }
}
