using HovyFridge.Api.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace HovyFridge.Api.Controllers;

[ApiController]
public class ProductsController : BaseController<FridgesController>
{
    public ProductsController(IServiceProvider serviceProvider, IOptions<Configuration> configuration, 
        ILoggerFactory loggerFactory) : base(serviceProvider, configuration, loggerFactory)
    {
    }

    [HttpGet]
    [Route("[controller]")]
    public async Task<ICollection<Product>> GetAll()
    {
        var status = await ProductsService.GetProductsList();
        if(status.Success && status.Result != null)
        {
            return status.Result;
        }

        return new List<Product>();
    }

    [HttpGet]
    [Route("[controller]/{id}")]
    public Product GetProduct([FromRoute] int id)
    {
        return new Product() { Name = "Anchovy" };
    }

    [HttpDelete]
    [Route("[controller]/{id}")]
    public async Task<ICollection<Product>> DeleteProduct([FromRoute] int id)
    {
        var status = await ProductsService.DeleteProductById(id);
        if (status.Success && status.Result != null)
        {
            return status.Result;
        }

        return new List<Product>();
    }

    [HttpPut]
    [Route("[controller]/{id}")]
    public Product UpdateProduct([FromBody] Product product)
    {
        return product;
    }

    [HttpPost]
    [Route("[controller]")]
    public async Task<ICollection<Product>> AddProduct([FromBody] Product product)
    {
        var status = await ProductsService.AddProduct(product);
        if (status.Success && status.Result != null)
        {
            return status.Result;
        }

        return new List<Product>();
    }
}