using HovyFridge.Data.Entity;
using HovyFridge.TestApi.QueryBuilder.Data;
using Microsoft.AspNetCore.Mvc;

namespace HovyFridge.TestApi.QueryBuilder.Controllers;

[Route("[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly ProductsQueryBuilder _productsQueryBuilder;
    
    public ProductsController(ProductsQueryBuilder productsQueryBuilder)
    {
        _productsQueryBuilder = productsQueryBuilder;
    }
    
    public List<Product> GetWithBarcodes(string barcode)
    {
        return _productsQueryBuilder.WithBarcode(barcode).ToList();
    }
}