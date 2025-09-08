using AutoMapper;
using Azure.Core;
using E_Commerce.Api.Helper;
using E_Commerce.Core.DTOs;
using E_Commerce.Core.Entites.Product;
using E_Commerce.Core.InterFaces;
using E_Commerce.InfraStructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace E_Commerce.Api.Controllers
{

    public class CategoriesController : BaseController
    {
        public CategoriesController(IUnitOfWork work, IMapper mapper) : base(work, mapper)
        {
        }

        [HttpGet]
        [Route("Get-All")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var Categories = await work.CategoryRepository.GetAllAsync();
                if(Categories is null)
                    return BadRequest(new ResponseApi(  400 , "Invalid request data." ));
                return Ok(Categories);
            }
            catch (Exception Ex ) 
            {
                return BadRequest(Ex.Message);  
            }
      
        }
        [HttpGet]
        [Route("Get-By-Id/{Id}")]
        public async Task<IActionResult> GetById(int Id)
        {
            try
            {
                var Category = await work.CategoryRepository.GetByIdAsync(Id);
                if (Category is null)
                    return BadRequest(new ResponseApi(400, $"Invalid request data. Of Id {Id}"));
                return Ok(Category);
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }

        }
        [HttpPost]
        [Route("Add-Category")]
        public async Task<IActionResult> Add(CategoryDTO categoryDTO)
        {
            try
            {
                var Category = mapper.Map<Category>(categoryDTO);
              
               await  work.CategoryRepository.AddAsync(Category);
                return Ok(new ResponseApi(200 , "Category added successfully"));
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }

        }
        [HttpPut]
        [Route("Update-Category")]
        public async Task<IActionResult> Update(UpdateCatgoryDTO UpdateCatgoryDTO)
        {
            try
            {
                var Category = mapper.Map<Category>(UpdateCatgoryDTO);
                await work.CategoryRepository.UpdateAsync(Category);
                return Ok(new ResponseApi(200, "Category Updated successfully"));

            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }

        }
        [HttpDelete]
        [Route("Delete-Category/{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            try
            {
                await work.CategoryRepository.DeleteAsync(Id);
                return Ok(new ResponseApi(200, "Category Deleted successfully"));

            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }

        }


    }
}
