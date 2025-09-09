using E_Commerce.Api.Helper;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Api.Controllers
{
    [Route("Errors/{SatusCode}")]
    [ApiController]
    public class ErrorController : ControllerBase
    {
        [HttpGet]
        public IActionResult Error(int SatusCode)
        {
            return new ObjectResult(new ResponseApi(SatusCode ));
        }
    }
}
