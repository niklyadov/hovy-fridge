using HovyFridge.Api.Services;
using HovyFridge.Data.Entity;
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
        private readonly ProductSuggestionsService _productSuggestionsService;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(ProductsService productsService, FridgesService fridgesService, ProductSuggestionsService productSuggestionsService, ILogger<ProductsController> logger)
        {
            _productsService = productsService;
            _fridgesService = fridgesService;
            _productSuggestionsService = productSuggestionsService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts([FromQuery] string? searchQuery = null)
        {
            var result = await _productsService.GetAllAsync(searchQuery);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return Problem(string.Join(',', result.Errors));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct([FromRoute] int id)
        {
            var result = await _productsService.GetByIdAsync(id);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return Problem(string.Join(',', result.Errors));
        }

        [HttpGet("barcode/{barcode}")]
        public async Task<IActionResult> GetProductWithBarcode([FromRoute] string barcode)
        {
            var result = await _productsService.GetByBarcodeAsync(barcode);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return Problem(string.Join(',', result.Errors));

        }

        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] Product product)
        {

            var result = await _productsService.AddAsync(product);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return Problem(string.Join(',', result.Errors));

        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct([FromBody] Product product)
        {

            var result = await _productsService.UpdateAsync(product);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return Problem(string.Join(',', result.Errors));

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] int id)
        {
            var result = await _productsService.DeleteByIdAsync(id);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return Problem(string.Join(',', result.Errors));

        }

        [HttpPut("{id}/restore")]
        public async Task<IActionResult> RestoreProduct([FromRoute] int id)
        {
            var result = await _productsService.RestoreByIdAsync(id);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return Problem(string.Join(',', result.Errors));
        }


        [HttpGet("suggestions")]
        public async Task<IActionResult> GetProductSuggestions()
        {
            var result = await _productSuggestionsService.GetAllAsync();
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return Problem(string.Join(',', result.Errors));
        }


        [HttpGet("suggestions/search")]
        public async Task<IActionResult> GetProductSuggestions([FromQuery] string searchQuery)
        {
            var result = await _productSuggestionsService.SearchAsync(searchQuery);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return Problem(string.Join(',', result.Errors));
        }

        //[RequestSizeLimit(1073741824)]
        [HttpPost("suggestions/upload")]
        public async Task<IActionResult> UploadProductSuggestions(IFormFile file)
        {
            var result = await _productSuggestionsService.InsertFromFileAsync(file);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return Problem(string.Join(',', result.Errors));
        }

        [HttpGet("statistic/fridge")]
        public async Task<IActionResult> GetProductsGroupedByFridgeId()
        {
            var result = await _productsService.GetProductsGroupedByFridgeIdAsync();
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return Problem(string.Join(',', result.Errors));
        }
    }
}