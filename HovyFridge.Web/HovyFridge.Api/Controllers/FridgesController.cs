using HovyFridge.Api.Data.Entity;
using HovyFridge.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace HovyFridge.Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
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
        public IActionResult GetFridge([FromRoute] int id)
        {
            try
            {
                return Ok(_fridgesService.GetById(id));
            }
            catch (Exception ex)
            {
                return Problem($"{ex.Message} {ex.StackTrace}");
            }
        }

        [HttpPut]
        [Route("fridges")]
        public IActionResult UpdateFridge([FromBody] Fridge fridge)
        {
            try
            {
                return Ok(_fridgesService.UpdateFridge(fridge));
            }
            catch (Exception ex)
            {
                return Problem($"{ex.Message} {ex.StackTrace}");
            }
        }

        [HttpGet]
        [Route("fridges")]
        public IActionResult GetFridges()
        {
            try
            {
                var headers = Request.Headers.ToList();

                return Ok(_fridgesService.GetList());
            }
            catch (Exception ex)
            {
                return Problem($"{ex.Message} {ex.StackTrace}");
            }
        }

        [HttpPost]
        [Route("fridges")]
        public IActionResult AddFridge([FromBody] Fridge fridge)
        {
            try
            {
                return Ok(_fridgesService.AddFridge(fridge));
            }
            catch (Exception ex)
            {
                return Problem($"{ex.Message} {ex.StackTrace}");
            }
        }

        [HttpPut]
        [Route("fridges/{id}/product")]
        public IActionResult PutProductToFridge([FromRoute] int id, [FromBody] int productId)
        {
            try
            {
                return Ok(_fridgesService.PutProduct(id, productId));
            }
            catch (Exception ex)
            {
                return Problem($"{ex.Message} {ex.StackTrace}");
            }
        }

        [HttpPut]
        [Route("fridges/{id}/product/restore")]
        public IActionResult RestoreProductInFridge([FromRoute] int id, [FromBody] int productId)
        {
            try
            {
                return Ok(_fridgesService.RestoreProductInFridge(id, productId));
            }
            catch (Exception ex)
            {
                return Problem($"{ex.Message} {ex.StackTrace}");
            }
        }

        [HttpDelete]
        [Route("fridges/{id}/product/{productId}")]
        public IActionResult RemoveProductFromFridge([FromRoute] int id, [FromRoute] int productId)
        {
            try
            {
                return Ok(_fridgesService.RemoveProductFromFridge(id, productId));
            }
            catch (Exception ex)
            {
                return Problem($"{ex.Message} {ex.StackTrace}");
            }
        }

        [HttpDelete]
        [Route("fridges/{id}")]
        public IActionResult DeleteFridge([FromRoute] int id)
        {
            try
            {
                return Ok(_fridgesService.DeleteFridge(id));
            }
            catch (Exception ex)
            {
                return Problem($"{ex.Message} {ex.StackTrace}");
            }
        }

        [HttpPut]
        [Route("fridges/{id}/restore")]
        public IActionResult RestoreFridge([FromRoute] int id)
        {
            try
            {
                return Ok(_fridgesService.RestoreFridge(id));
            }
            catch (Exception ex)
            {
                return Problem($"{ex.Message} {ex.StackTrace}");
            }
        }
    }
}