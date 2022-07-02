using HovyFridge.Api.Data.Entity;
using HovyFridge.Api.Services;
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
        private readonly ILogger<FridgesController> _logger;

        public FridgesController(ProductsService productsService, FridgesService fridgesService, ILogger<FridgesController> logger)
        {
            _productsService = productsService;
            _fridgesService = fridgesService;
            _logger = logger;
        }

        [HttpGet]
        [Route("fridges/{id}")]
        public async Task<IActionResult> GetFridge([FromRoute] long id)
        {
            var result = await _fridgesService.GetByIdAsync(id);

            if(result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return Problem(string.Join(',', result.Errors));
        }

        [HttpPut]
        [Route("fridges")]
        public async Task<IActionResult> UpdateFridge([FromBody] Fridge fridge)
        {
            var result = await _fridgesService.UpdateFridge(fridge);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return Problem(string.Join(',', result.Errors));
        }

        [HttpGet]
        [Route("fridges")]
        public async Task<IActionResult> GetFridges()
        {
            var result = await _fridgesService.GetList();

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return Problem(string.Join(',', result.Errors));
        }

        [HttpPost]
        [Route("fridges")]
        public async Task<IActionResult> AddFridge([FromBody] Fridge fridge)
        {
            var result = await _fridgesService.AddFridgeAsync(fridge);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return Problem(string.Join(',', result.Errors));
        }

        [HttpPut]
        [Route("fridges/{id}/product")]
        public async Task<IActionResult> PutProductToFridge([FromRoute] long id, [FromBody] long productId)
        {
            var result = await _fridgesService.PutProduct(id, productId);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return Problem(string.Join(',', result.Errors));
        }

        [HttpPut]
        [Route("fridges/{id}/product/restore")]
        public async Task<IActionResult> RestoreProductInFridge([FromRoute] long id, [FromBody] long productId)
        {
            var result = await _fridgesService.RestoreProductInFridge(id, productId);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return Problem(string.Join(',', result.Errors));
        }

        [HttpDelete]
        [Route("fridges/{id}/product/{productId}")]
        public async Task<IActionResult> RemoveProductFromFridge([FromRoute] long id, [FromRoute] long productId)
        {
            var result = await _fridgesService.RemoveProductFromFridge(id, productId);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return Problem(string.Join(',', result.Errors));
        }

        [HttpDelete]
        [Route("fridges/{id}")]
        public async Task<IActionResult> DeleteFridge([FromRoute] long id)
        {
            var result = await _fridgesService.DeleteFridge(id);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return Problem(string.Join(',', result.Errors));
        }

        [HttpPut]
        [Route("fridges/{id}/restore")]
        public async Task<IActionResult> RestoreFridge([FromRoute] long id)
        {

            var result = await _fridgesService.RestoreFridge(id);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return Problem(string.Join(',', result.Errors));
        }

        [HttpGet]
        [Route("fridges/{fridgeId}/access")]
        public async Task<IActionResult> GetAccessLevels([FromRoute] long fridgeId)
        {
            var result = _fridgesService.GetFridgeAccessLevels(fridgeId);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return Problem(string.Join(',', result.Errors));
        }

        [HttpPost]
        [Route("fridges/{fridgeId}/access")]
        public async Task<IActionResult> AddAccessLevel([FromRoute] long fridgeId, [FromBody] FridgeAccessLevel accessLevel)
        {
            var result = _fridgesService.AddFridgeAccessLevel(fridgeId, accessLevel);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return Problem(string.Join(',', result.Errors));
        }

        [HttpPut]
        [Route("fridges/{fridgeId}/access")]
        public async Task<IActionResult> UpdateAccessLevel([FromRoute] long fridgeId, [FromBody] FridgeAccessLevel accessLevel)
        {
            var result = _fridgesService.UpdateFridgeAccessLevel(fridgeId, accessLevel);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return Problem(string.Join(',', result.Errors));
        }

        [HttpDelete]
        [Route("fridges/{fridgeId}/access/{accessLevelId}")]
        public IActionResult DeleteAccessLevel([FromRoute] long fridgeId, [FromRoute] long accessLevelId)
        {
            var result = _fridgesService.DeleteFridgeAccessLevel(accessLevelId);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return Problem(string.Join(',', result.Errors));
        }
    }
}