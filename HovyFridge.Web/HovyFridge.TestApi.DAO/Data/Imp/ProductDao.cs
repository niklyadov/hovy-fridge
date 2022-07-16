using HovyFridge.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace HovyFridge.TestApi.DAO.Data.Imp;

public class ProductDao : IProductDao
{
    private readonly DbContext _dbContext;
    
    public ProductDao(DbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public Product GetProductById(long productId)
    {
        throw new NotImplementedException();
    }

    public List<Product> GetProductsList()
    {
        throw new NotImplementedException();
    }

    public Product DeleteProductById(long productId)
    {
        throw new NotImplementedException();
    }

    public Product UpdateProduct(Product product)
    {
        throw new NotImplementedException();
    }

    public Product PutProduct(Product product)
    {
        throw new NotImplementedException();
    }
}