using HovyFridge.Data.Entity;

namespace HovyFridge.TestApi.DAO.Data;

public interface IProductDao
{
    public Product GetProductById(long productId);
    public List<Product> GetProductsList();
    public Product DeleteProductById(long productId);
    public Product UpdateProduct(Product product);
    public Product PutProduct(Product product);
}