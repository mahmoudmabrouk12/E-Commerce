using AutoMapper;
using E_Commerce.Api.Helper;
using E_Commerce.Core.DTOs;
using E_Commerce.Core.InterFaces;
using E_Commerce.Core.Sharing;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Api.Controllers
{

    public class ProductController : BaseController
    {
        public ProductController(IUnitOfWork work, IMapper mapper) : base(work, mapper)
        {
        }

        [HttpGet]
        [Route("Get-All")]
        public async Task<IActionResult> GetAll([FromQuery]ProductParams productParams)
        {
            try
            {

                var Products = await work.ProductRepository.GetAllAsync(productParams);


                return Ok(new Pagination<ProductDTO>(productParams.PageSize , productParams.PageNumber, Products.TotalCount , Products.Products));
            }
            catch (Exception Ex)
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

                var Product = await work.ProductRepository.GetByIdAsync(Id,l => l.category,
                    l => l.photos);
                var result = mapper.Map<ProductDTO>(Product);

                if (Product is null)
                    return BadRequest(new ResponseApi(400, "Invalid request data."));
                return Ok(result);
            }
            catch (Exception Ex)
            {
                return BadRequest(new ResponseApi(400,Ex.Message));
            }

        }
        [HttpPost]
        [Route("Add-Product")]
        public async Task<IActionResult> Add(AddProductDTO addProductDTO)
        {
            try
            {
                await work.ProductRepository.AddAsync(product: addProductDTO);
                return Ok(value: new ResponseApi(200, "Item Has Been Added"));
            }
            catch (Exception Ex)
            {

                return BadRequest(error: new ResponseApi(StatusCode: 400, Ex.Message));
            }

        }
        [HttpPut]
        [Route("Update-Product")]
        public async Task<IActionResult> Update(UpdateProductDTO updateProductDTO)
        {
            try
            {
                await work.ProductRepository.UpdateAsync(updateProductDTO);
                return Ok(value: new ResponseApi(200, "Item Has Been Updated"));

            }
            catch (Exception Ex)
            {

                return BadRequest(error: new ResponseApi(StatusCode: 400, Ex.Message));
            }
        }
        [HttpDelete]
        [Route("Delete-Product/{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            try
            {
                var Product = await work.ProductRepository.GetByIdAsync
                    (Id, l => l.category, l => l.photos);
                await work.ProductRepository.DeleteAsync(Product);

                return Ok(value: new ResponseApi(200, "The Item Is Deleted"));

            }
            catch (Exception ex)
            {

                return BadRequest(error: new ResponseApi(StatusCode: 400, ex.Message));
            }

        }




    }
}
