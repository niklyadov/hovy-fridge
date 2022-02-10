using HovyFridge.Api.Entity;
using Microsoft.AspNetCore.Mvc;

namespace HovyFridge.Api.Controllers;

[ApiController]
public class ProductsController : ControllerBase
{
    private readonly ILogger<FridgesController> _logger;

    public ProductsController(ILogger<FridgesController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    [Route("[controller]")]
    public ICollection<Product> GetAll()
    {
        return new List<Product>()
            {
                new Product() { Name = "Anchovy"}
            };
    }

    [HttpGet]
    [Route("[controller]/{id}")]
    public Product GetProduct([FromRoute] int id)
    {
        return new Product() { Name = "Anchovy" };
    }

    [HttpDelete]
    [Route("[controller]/{id}")]
    public bool DeleteProduct([FromRoute] int id)
    {
        return true;
    }

    [HttpPut]
    [Route("[controller]/{id}")]
    public Product UpdateProduct([FromBody] Product product)
    {
        return product;
    }

    [HttpPost]
    [Route("[controller]")]
    public Product AddProduct([FromBody] Product product)
    {
        return product;
    }
}