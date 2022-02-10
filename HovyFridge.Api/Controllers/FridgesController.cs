using HovyFridge.Api.Entity;
using Microsoft.AspNetCore.Mvc;

namespace HovyFridge.Api.Controllers;

[ApiController]
public class FridgesController : ControllerBase
{
    private readonly ILogger<FridgesController> _logger;

    public FridgesController(ILogger<FridgesController> logger)
    {
        _logger = logger;
    }


    [HttpGet]
    [Route("[controller]")]
    public ICollection<Fridge> GetAll()
    {
        return new List<Fridge>()
            {
                new Fridge() { Name = "Anchovy"}
            };
    }

    [HttpGet]
    [Route("[controller]/{id}")]
    public Fridge GetFridge([FromRoute] int id)
    {
        return new Fridge() { Name = "Anchovy" };
    }

    [HttpDelete]
    [Route("[controller]/{id}")]
    public bool DeleteFridge([FromRoute] int id)
    {
        return true;
    }

    [HttpPut]
    [Route("[controller]/{id}")]
    public Fridge UpdateFridge([FromBody] Fridge product)
    {
        return product;
    }

    [HttpPost]
    [Route("[controller]")]
    public Fridge AddFridge([FromBody] Fridge product)
    {
        return product;
    }
}