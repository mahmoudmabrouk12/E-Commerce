
using E_Commerce.Core.DTOs;
using E_Commerce.Core.Entites.User;
using E_Commerce.Core.InterFaces;
using E_Commerce.Core.Services;
using E_Commerce.Core.Sharing;
using E_Commerce.InfraStructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.InfraStructure.Repository
{
    public class AuthRepository : IAuth
    {
        private readonly UserManager<AppUser> userManager;
        private readonly IEmailService emailService;
        private readonly SignInManager<AppUser> signInManager;
        private readonly IGenerateToken generateToken;
        private readonly AppDbContext context;

        public AuthRepository(UserManager<AppUser> userManager, IEmailService emailService, SignInManager<AppUser> signInManager, IGenerateToken generateToken = null, AppDbContext context = null)
        {
            this.userManager = userManager;
            this.emailService = emailService;
            this.signInManager = signInManager;
            this.generateToken = generateToken;
            this.context = context;
        }
        public async Task<string> RegisterAsync(RegisterDTO registerDTO)
        {
            if (registerDTO == null)
                return null;

            if(await userManager.FindByNameAsync(registerDTO.Username) is not null)
                return "This Username is already registered";

            if (await userManager.FindByEmailAsync(registerDTO.Email) is not null)
                return "This Email is already registered";

            AppUser user = new AppUser ()
            { 
             Email = registerDTO.Email,
             UserName = registerDTO.Username,
             DisplayName = registerDTO.DisplayName

            };

            var result = await userManager.CreateAsync(user , registerDTO.Pass);
            if (result.Succeeded is not true)
                return result.Errors.ToList()[0].Description;
            string token = await userManager.GenerateEmailConfirmationTokenAsync(user);
             await SendEmail(user.Email , token, "Active" , "ActiveEmail" , "Please Active Your Email and Click on button to Active");
            return "done";



        }
        public async Task SendEmail(string email ,string code , string component , string subject , string message) 
        {
            var result = new EmailDTO(email, "mabroukmahmoud411@gmail.com", subject,
                EmailStringBody.Send(email , code , component , message));
             await emailService.SendEmail(result);
        
        
        }
        public async Task <string> LoginAsync(LoginDTO login)
        {
            if (login == null)
                return null;
            var FindUser = await userManager.FindByEmailAsync(login.Email);
            if(!FindUser.EmailConfirmed)
            {
                string token = await userManager.GenerateEmailConfirmationTokenAsync(FindUser);
                await SendEmail(FindUser.Email , token, "Active" , "ActiveEmail" , "Please Active Your Email and Click on button to Active");
                return "Please Confirm Your Email First And We Have Sent Active To Your Email ";
            }
            var result = await signInManager.CheckPasswordSignInAsync(FindUser, login.Pass, true);
            if (result.Succeeded)
                return generateToken.GetAndCreateToken(FindUser);
            return "Please Check Your Email And Password";




        }
        public async Task<bool> SendEmailForForgetPassWord(string email)
        {
            var FindUser = await userManager.FindByEmailAsync (email);
            if (FindUser == null) return false;
            var Token = 
                await userManager.GeneratePasswordResetTokenAsync(FindUser);
            await SendEmail(FindUser.Email, Token, "Reset-Password", "Reset-Password", "Click on button to Reset Your Password");
            return true;
        }
        public async Task<string> ResetPassword(ResetPasswordDTO resetPasswordDTO) 
        {
            var Find_User = await userManager.FindByEmailAsync(resetPasswordDTO.Email);
            if (Find_User is null)
                return null;
            var result = await userManager .ResetPasswordAsync(Find_User , resetPasswordDTO.Token , resetPasswordDTO.Pass);
            if(result.Succeeded)
                return   "The password has been reset successfully.";
            return result .Errors.ToList()[0].Description;


        }
        public async Task<bool> ActiveAccount(ActiveAccountDTO activeAccountDTO)
        {
            var Find_User = await userManager.FindByEmailAsync(activeAccountDTO.Email);
            if (Find_User is null)
                return false;
            var result = await userManager.ConfirmEmailAsync(Find_User, activeAccountDTO.Token );
            if (result.Succeeded)
                return true;
            string token = await userManager.GenerateEmailConfirmationTokenAsync(Find_User);
            await SendEmail(Find_User.Email, token, "Active", "ActiveEmail", "Please Active Your Email and Click on button to Active");
            return  false;                          

        }

        public async Task<bool> UpdateAddress(string email, Address address)
        {
            var Find_User = await userManager.FindByEmailAsync(email);
            if (Find_User is null)
                return false;
            var My_address = await context.Addresses.FirstOrDefaultAsync(o => o.AppUserId == Find_User.Id);
            if (My_address is null)
            {
                await context.Addresses.AddAsync(address);
            }
            else
            {
                address.Id = My_address.Id;
                context.Addresses.Update(address);
            }
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<Address> GetUserAddress(string email)
        {
            var Find_User =await  userManager.FindByEmailAsync(email);
            var adrress = await context.Addresses.FirstOrDefaultAsync(m => m.AppUserId == Find_User.Id);
            return adrress;
        }
    }
}
