using ECommerce.SharedLibirary.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductApi.Application.Conversions;
using ProductApi.Application.DTOs;
using ProductApi.Application.Interfaces;

namespace ProductApi.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(IProduct productInterface) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllProducts()
        {
            var products = await productInterface.GetAllAsync();
            if (!products.Any())
            {
                return NotFound("No Products Found in the database");
            }
            var mappedProducts = ProductConversion.ToDtos(products);

            if (!mappedProducts.Any())
            {

                return NotFound("No Products Found in the database");

            }

            return Ok(mappedProducts);
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductDto>> GetProductById(int id)
        {
            var product = await productInterface.FindByIdAsync(id);
            if (product == null)
            {
                return NotFound($"No Product with this id : {id} Found in the database");
            }
            var mappedProduct = ProductConversion.ToDto(product);
            if (!mappedProduct.Equals(null))
            {
                return NotFound($"No Product with this id : {id} Found in the database");

            }
            return Ok(product);

        }
        [HttpPost]
        public async Task<ActionResult<Response>> CreateProduct(ProductDto productDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var mappedProduct = ProductConversion.ToEntity(productDto);
            if (mappedProduct is null)
            {
                return BadRequest("Invalid Product Data");
            }
            var response = await productInterface.CreateAsync(mappedProduct);

            return response.flag is true ? Ok(response) : BadRequest(response);

        }
        [HttpPut]
        public async Task<ActionResult<Response>> UpdateProduct(ProductDto productDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var mappedProduct = ProductConversion.ToEntity(productDto);
            if (mappedProduct is null)
            {
                return BadRequest("Invalid Product Data");
            }
            var response = await productInterface.UpdateAsync(mappedProduct);
            return response.flag is true ? Ok(response) : BadRequest(response);
        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Response>> DeleteProduct(int  id)
        {
            

            var getEntity = await productInterface.FindByIdAsync(id);
            var response = await productInterface.DeleteAsync(getEntity);
            return response.flag is true ? Ok(response) : BadRequest(response);

        }
    }
}