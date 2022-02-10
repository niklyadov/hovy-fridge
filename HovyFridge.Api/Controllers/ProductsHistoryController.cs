using HovyFridge.Api.Entity;
using Microsoft.AspNetCore.Mvc;

namespace HovyFridge.Api.Controllers;

[ApiController]
public class ProductsHistoryController : ControllerBase
{
    private readonly ILogger<FridgesController> _logger;

    public ProductsHistoryController(ILogger<FridgesController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    [Route("/products/history/")]
    public ICollection<ProductHistory> GetAll()
    {
        return new List<ProductHistory>()
            {
                new ProductHistory() { ProductHistoryOperation = ProductHistoryOperation.Added }
            };
    }

    [HttpPost]
    [Route("/products/history")]
    public ProductHistory AddHistoryEntry([FromBody] ProductHistory user)
    {
        return user;
    }
}