using AutoMapper;
using E_Commerce.Core.InterFaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Api.Controllers
{

    public class BugController : BaseController
    {
        public BugController(IUnitOfWork work, IMapper mapper) : base(work, mapper)
        {
        }
        [HttpGet("not-found")]
        public async Task<IActionResult> GetNotFound()
        {
            var category = await work.CategoryRepository.GetByIdAsync(10000000);
            if (category == null)
                return NotFound(); 
            return Ok(category);
        }

        [HttpGet("server-error")]
        public async Task<IActionResult> GetServerError()
        {
            var category = await work.CategoryRepository.GetByIdAsync(10000000);
            if (category == null)
                return StatusCode(500, "Server error occurred: Category not found.");

            category.Name = "Error";
            return Ok(category);
        }

        [HttpGet("bad-request/{id}")]
        public IActionResult GetBadRequestWithId(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid ID.");
            return Ok($"You passed ID: {id}");
        }

        [HttpGet("bad-request")]
        public IActionResult GetBadRequest()
        {
            return BadRequest("This is a bad request test.");
        }


    }
}
