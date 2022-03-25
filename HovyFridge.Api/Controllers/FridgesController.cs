using HovyFridge.Api.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace HovyFridge.Api.Controllers;

[ApiController]
public class FridgesController : BaseController<FridgesController>
{
    public FridgesController(IServiceProvider serviceProvider, IOptions<Configuration> configuration, 
        ILoggerFactory loggerFactory) : base(serviceProvider, configuration, loggerFactory)
    {
    }

    [HttpGet]
    [Route("[controller]")]
    public async Task<ICollection<Fridge>> GetAll()
    {
        var status = await FridgesService.GetFridgesListAsync();
        if (status.Success && status.Result != null)
        {
            return status.Result;
        }

        return new List<Fridge>();
    }

    [HttpGet]
    [Route("[controller]/{id}")]
    public async Task<Fridge?> GetFridge([FromRoute] int id)
    {
        var status = await FridgesService.GetFridgeByIdAsync(id);
        if (status.Success && status.Result != null)
        {
            return status.Result;
        }

        return null;
    }

    [HttpDelete]
    [Route("[controller]/{id}")]
    public async Task<ICollection<Fridge>> DeleteFridge([FromRoute] int id)
    {
        var status = await FridgesService.DeleteFridgeByIdAsync(id);
        if (status.Success && status.Result != null)
        {
            return status.Result;
        }

        return new List<Fridge>();
    }

    [HttpPut]
    [Route("[controller]/{id}")]
    public Fridge UpdateFridge([FromBody] Fridge product)
    {
        return product;
    }

    [HttpPost]
    [Route("[controller]")]
    public async Task<ICollection<Fridge>> AddFridge([FromBody] Fridge fridge)
    {
        var status = await FridgesService.CreateFridgeAsync(fridge);
        if (status.Success && status.Result != null)
        {
            return status.Result;
        }

        return new List<Fridge>();
    }

    [HttpPost]
    [Route("[controller]/{fridgeId}/products")]
    public async Task<IActionResult> PushProductToFridge([FromQuery] int fridgeId, [FromBody] int productId)
    {
        var status = await FridgesService.PushProductIntoFridgeAsync(fridgeId, productId);

        if (status.Success) return Ok(status.Result);

        return NotFound();
    }

    [HttpDelete]
    [Route("[controller]/{fridgeId}/products")]
    public async Task<IActionResult> PopProductFromFridge([FromQuery] int fridgeId, [FromBody] int productId)
    {
        var status = await FridgesService.PopProductFromFridgeAsync(fridgeId, productId);

        if (status.Success) return Ok(status.Result);

        return NotFound();
    }
}