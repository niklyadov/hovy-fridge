using HovyFridge.Data;
using HovyFridge.Data.Entity;

namespace HovyFridge.TestApi.DAO.Data;

public class ProductDao : DaoBase
{
    private readonly ApplicationContext _db;

    public ProductDao(ApplicationContext db)
    {
        _db = db;
    }

    public IEnumerable<Product> GetProductsListByBarcode(string barcode)
    {
        return _db.Products.Where(p => p.BarCode == barcode).ToList();
    }

    public Product? GetProductById(long productId)
    {
        return _db.Products.SingleOrDefault(p => p.Id == productId);
    }

    public IEnumerable<Product> GetProductsList()
    {
        return _db.Products.ToList();
    }

    public Product DeleteProductById(long productId)
    {
        var product = GetProductById(productId);

        if (product == null)
            throw new InvalidOperationException($"Product with id {productId} not found");

        var result = _db.Products.Remove(product);
        _db.SaveChanges();

        return result.Entity;
    }

    public Product UpdateProduct(Product product)
    {
        var result = _db.Products.Update(product);
        _db.SaveChanges();

        return result.Entity;
    }

    public Product PutProduct(Product product)
    {
        var result = _db.Products.Add(product);
        _db.SaveChanges();

        return result.Entity;
    }
}