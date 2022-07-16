using HovyFridge.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace HovyFridge.TestApi.QueryBuilder.Data;

public class ProductsQueryBuilder : QueryBuilder<Product>
{
    public ProductsQueryBuilder(DbContext context) : base(context)
    {
    }
    
    
}