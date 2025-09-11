using AutoMapper;
using E_Commerce.Api.Helper;
using E_Commerce.Core.DTOs;
using E_Commerce.Core.InterFaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Api.Controllers
{

    public class AccountController : BaseController
    {
        public AccountController(IUnitOfWork work, IMapper mapper) : base(work, mapper)
        {
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
        [HttpPost]
        [Route("Send-Email-Forget-Password")]
        public async Task<IActionResult> Forget( string email)
        {
            var result = await work.Auth.SendEmailForForgetPassWord(email);

            return result ? Ok(new ResponseApi(200)) :
                BadRequest(new ResponseApi(400));
        }

    }
}
