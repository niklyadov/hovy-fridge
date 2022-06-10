using HovyFridge.Api.Data.Entity;
using HovyFridge.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace HovyFridge.Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
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
        public IActionResult GetProducts([FromQuery] string? searchQuery = null)
        {
            var result = _productsService.GetProducts(searchQuery);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return Problem(string.Join(',', result.Errors));
        }

        [HttpGet("products/{id}")]
        public IActionResult GetProduct([FromRoute] int id)
        {
            var result = _productsService.GetProductWithId(id);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return Problem(string.Join(',', result.Errors));
        }

        [HttpGet("products/barcode/{barcode}")]
        public IActionResult GetProductWithBarcode([FromRoute] string barcode)
        {
            var result = _productsService.GetProductWithBarcode(barcode);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return Problem(string.Join(',', result.Errors));

        }

        [HttpPost("products/")]
        public IActionResult AddProduct([FromBody] Product product)
        {

            var result = _productsService.AddProduct(product);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return Problem(string.Join(',', result.Errors));

        }

        [HttpPut("products/")]
        public IActionResult UpdateProduct([FromBody] Product product)
        {

            var result = _productsService.UpdateProduct(product);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return Problem(string.Join(',', result.Errors));

        }

        [HttpDelete("products/{id}")]
        public IActionResult DeleteProduct([FromRoute] int id)
        {
            var result = _productsService.DeleteProduct(id);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return Problem(string.Join(',', result.Errors));

        }

        [HttpPut("products/{id}/restore")]
        public IActionResult RestoreProduct([FromRoute] int id)
        {
            var result = _productsService.RestoreProduct(id);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return Problem(string.Join(',', result.Errors));
        }
    }
}