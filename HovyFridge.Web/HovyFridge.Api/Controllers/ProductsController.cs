using HovyFridge.Api.Data.Entity;
using HovyFridge.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace HovyFridge.Api.Controllers
{
    [ApiController]
    //[ApiVersion("1.0")]
    //[Route("api/v{version:apiVersion}/[controller]")]\
    [Route("[controller]")]
    public class ProductsController : BaseController
    {
        private readonly ProductsService _productsService;
        private readonly FridgesService _fridgesService;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(ProductsService productsService, FridgesService fridgesService, ILogger<ProductsController> logger)
        {
            _productsService = productsService;
            _fridgesService = fridgesService;
            _logger = logger;
        }

        [HttpGet("products/")]
        public async Task<IActionResult> GetProducts([FromQuery] string? searchQuery = null)
        {
            var result = await _productsService.GetProducts(searchQuery);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return Problem(string.Join(',', result.Errors));
        }

        [HttpGet("products/{id}")]
        public async Task<IActionResult> GetProduct([FromRoute] int id)
        {
            var result = await _productsService.GetProductWithId(id);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return Problem(string.Join(',', result.Errors));
        }

        [HttpGet("products/barcode/{barcode}")]
        public async Task<IActionResult> GetProductWithBarcode([FromRoute] string barcode)
        {
            var result = await _productsService.GetProductWithBarcode(barcode);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return Problem(string.Join(',', result.Errors));

        }

        [HttpPost("products/")]
        public async Task<IActionResult> AddProduct([FromBody] Product product)
        {

            var result = await _productsService.AddProduct(product);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return Problem(string.Join(',', result.Errors));

        }

        [HttpPut("products/")]
        public async Task<IActionResult> UpdateProduct([FromBody] Product product)
        {

            var result = await _productsService.UpdateProduct(product);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return Problem(string.Join(',', result.Errors));

        }

        [HttpDelete("products/{id}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] int id)
        {
            var result = await _productsService.DeleteProduct(id);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return Problem(string.Join(',', result.Errors));

        }

        [HttpPut("products/{id}/restore")]
        public async Task<IActionResult> RestoreProduct([FromRoute] int id)
        {
            var result = await _productsService.RestoreProduct(id);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return Problem(string.Join(',', result.Errors));
        }
    }
}