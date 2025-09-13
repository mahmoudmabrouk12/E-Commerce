using AutoMapper;
using E_Commerce.Api.Helper;
using E_Commerce.Core.DTOs;
using E_Commerce.Core.Entites.User;
using E_Commerce.Core.InterFaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace E_Commerce.Api.Controllers
{

    public class AccountController : BaseController
    {
        public AccountController(IUnitOfWork work, IMapper mapper) : base(work, mapper)
        {
        }
        [HttpGet]
        [Route("Get-Address-For-User")]
        public async Task<IActionResult> GetAddress()
        {

            var adress = await work.Auth.GetUserAddress( User.FindFirst(ClaimTypes.Email)?.Value);
            var result = mapper.Map<ShippAdressDTO>(adress);
            return Ok(result);
        }
        [HttpGet]
        [Route("Is-User-Auth")]
        public async Task<IActionResult> ISAuth()
        {
            return User.Identity.IsAuthenticated  ? Ok(new ResponseApi(200)) :
                BadRequest(new ResponseApi(400));
        }
        [HttpPut]
        [Route("Update-Address")]
        public async Task<IActionResult> Update( ShippAdressDTO shippAdressDTO)
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            var address = mapper.Map<Address>(shippAdressDTO);
            var result = await work.Auth.UpdateAddress(email , address);
            return result ? Ok(new ResponseApi(200)) :
                BadRequest(new ResponseApi(400));
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        { 
            var result  = await work .Auth .RegisterAsync(registerDTO);
            if (result != "done")
            {
                return BadRequest(new ResponseApi(400, result));
            }
            return Ok(new ResponseApi(200, result));
        }
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            var result = await work.Auth.LoginAsync(loginDTO);
            if(result == "Please")
                return BadRequest(new ResponseApi(400 ));
            Response.Cookies.Append("token", result, new CookieOptions
            {
                Secure = true,
                HttpOnly = true,
                Domain = "localhost",
                Expires = DateTime.Now.AddDays(1),
                IsEssential = true,
                SameSite = SameSiteMode.Strict
            });
          return Ok(new ResponseApi(200));
        }
        [HttpPost]
        [Route("Active-Account")]
        public async Task<IActionResult> Active(ActiveAccountDTO activeAccountDTO)
        {
            var result = await work.Auth.ActiveAccount(activeAccountDTO);

            return result ? Ok(new ResponseApi(200)) :
                BadRequest(new ResponseApi(400));
        }
        [HttpGet]
        [Route("Send-Email-Forget-Password")]
        public async Task<IActionResult> Forget( string email)
        {
            var result = await work.Auth.SendEmailForForgetPassWord(email);

            return result ? Ok(new ResponseApi(200)) :
                BadRequest(new ResponseApi(400));
        }
        [HttpPost]
        [Route("Reset-Password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDTO resetPasswordDTO)
        {
            var result = await work.Auth.ResetPassword(resetPasswordDTO);

            if (result is null)
                return NotFound(new ResponseApi(404, "User not found."));

            if (result == "The password has been reset successfully.")
                return Ok(new ResponseApi(200, result));

            return BadRequest(new ResponseApi(400, result));
        }
    }
}
