using HovyFridge.Api.Services;
using HovyFridge.Data.Entity;
using Microsoft.AspNetCore.Mvc;

namespace HovyFridge.Api.Controllers
{
    [ApiController]
    //[ApiVersion("1.0")]
    //[Route("api/v{version:apiVersion}/[controller]")]
    [Route("[controller]")]
    public class FridgesController : BaseController
    {
        private readonly ProductsService _productsService;
        private readonly FridgesService _fridgesService;
        private readonly FridgeAccessLevelsService _fridgeAccessLevelsService;
        private readonly ILogger<FridgesController> _logger;

        public FridgesController(ProductsService productsService, FridgesService fridgesService, FridgeAccessLevelsService fridgeAccessLevelsService, ILogger<FridgesController> logger)
        {
            _productsService = productsService;
            _fridgesService = fridgesService;
            _fridgeAccessLevelsService = fridgeAccessLevelsService;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFridge([FromRoute] long id)
        {
            var result = await _fridgesService.GetByIdAsync(id);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return Problem(string.Join(',', result.Errors));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateFridge([FromBody] Fridge fridge)
        {
            var result = await _fridgesService.UpdateAsync(fridge);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return Problem(string.Join(',', result.Errors));
        }

        [HttpGet]
        public async Task<IActionResult> GetFridges()
        {
            var result = await _fridgesService.GetListAsync();

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return Problem(string.Join(',', result.Errors));
        }

        [HttpPost]
        public async Task<IActionResult> AddFridge([FromBody] Fridge fridge)
        {
            var result = await _fridgesService.AddAsync(fridge);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return Problem(string.Join(',', result.Errors));
        }

        [HttpPut("{id}/product")]
        public async Task<IActionResult> PutProductToFridge([FromRoute] long id, [FromBody] long productId)
        {
            var result = await _fridgesService.PutProductAsync(id, productId);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return Problem(string.Join(',', result.Errors));
        }

        [HttpPut("{id}/product/restore")]
        public async Task<IActionResult> RestoreProductInFridge([FromRoute] long id, [FromBody] long productId)
        {
            var result = await _fridgesService.RestoreProductAsync(id, productId);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return Problem(string.Join(',', result.Errors));
        }

        [HttpDelete("{id}/product/{productId}")]
        public async Task<IActionResult> RemoveProductFromFridge([FromRoute] long id, [FromRoute] long productId)
        {
            var result = await _fridgesService.RemoveProductAsync(id, productId);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return Problem(string.Join(',', result.Errors));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFridge([FromRoute] long id)
        {
            var result = await _fridgesService.DeleteByIdAsync(id);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return Problem(string.Join(',', result.Errors));
        }

        [HttpPut("{id}/restore")]
        public async Task<IActionResult> RestoreFridge([FromRoute] long id)
        {

            var result = await _fridgesService.RestoreByIdAsync(id);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return Problem(string.Join(',', result.Errors));
        }

        [HttpGet("{fridgeId}/access")]
        public async Task<IActionResult> GetAccessLevels([FromRoute] long fridgeId)
        {
            var result = await _fridgeAccessLevelsService.GetByFridgeIdAsync(fridgeId);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return Problem(string.Join(',', result.Errors));
        }

        [HttpPost("{fridgeId}/access")]
        public async Task<IActionResult> AddAccessLevel([FromRoute] long fridgeId, [FromBody] FridgeAccessLevel accessLevel)
        {
            var result = await _fridgeAccessLevelsService.AddAsync(fridgeId, accessLevel);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return Problem(string.Join(',', result.Errors));
        }

        [HttpPut("{fridgeId}/access")]
        public async Task<IActionResult> UpdateAccessLevel([FromRoute] long fridgeId, [FromBody] FridgeAccessLevel accessLevel)
        {
            var result = await _fridgeAccessLevelsService.UpdateAsync(fridgeId, accessLevel);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return Problem(string.Join(',', result.Errors));
        }

        [HttpDelete("{fridgeId}/access/{accessLevelId}")]
        public async Task<IActionResult> DeleteAccessLevel([FromRoute] long fridgeId, [FromRoute] long accessLevelId)
        {
            var result = await _fridgeAccessLevelsService.DeleteByIdAsync(accessLevelId);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return Problem(string.Join(',', result.Errors));
        }
    }
}