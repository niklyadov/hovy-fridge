using HovyFridge.Api.Data;
using HovyFridge.Api.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace HovyFridge.Api.Services;

public class FridgesService
{
    private ApplicationContext _db;
    private DbSet<Product> _products;
    private DbSet<Fridge> _fridges;
    public FridgesService(ApplicationContext applicationContext)
    {
        _db = applicationContext;
        _products = applicationContext.Products;
        _fridges = applicationContext.Fridges;
    }

    public List<Product>? GetProducts(int fridgeId)
    {
        var fridge = _fridges.FirstOrDefault(f => f.Id == fridgeId);

        if (fridge == null)
        {
            return null;
        }


        return fridge.Products;
    }

    public Fridge? GetById(int id)
    {
        var fridge = _fridges.Include(f => f.Products)
            .FirstOrDefault(f => f.Id == id);


        fridge.Products = fridge.Products.Where(p => !p.IsDeleted).ToList();

        return fridge;
    }

    internal Fridge UpdateFridge(Fridge fridge)
    {
        var updatedFridge = _fridges.Update(fridge).Entity;

        _db.SaveChanges();

        return updatedFridge;
    }


    public List<Fridge> GetList()
    {
        return _fridges.Include(f => f.Products).Where(f => !f.IsDeleted).Select(o => new Fridge()
        {
            Id = o.Id,
            IsDeleted = o.IsDeleted,
            Name = o.Name,
            Products = new List<Product>(),
            ProductsAmount = o.Products.Where(p => !p.IsDeleted).ToList().Count
        }).ToList();
    }

    public Fridge? AddFridge(Fridge fridge)
    {
        var addedFridge = _fridges.Add(fridge);

        _db.SaveChanges();

        return addedFridge.Entity;
    }

    public Product PutProduct(int id, int productId)
    {
        var fridge = _fridges.FirstOrDefault(f => f.Id == id);
        var product = _products.FirstOrDefault(p => p.Id == productId);

        if (fridge == null) throw new InvalidOperationException("Fridge is not found!");
        if (product == null) throw new InvalidOperationException("Product is not found!");

        var newProduct = new Product()
        {
            FridgeId = id,
            BarCode = product.BarCode,
            Name = product.Name,
            IsDeleted = false,
            CreatedTimestamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds()
        };

        newProduct = _db.Products.Add(newProduct).Entity;

        fridge.Products.Add(newProduct);

        _db.Fridges.Update(fridge);
        _db.SaveChanges();

        return newProduct;
    }

    public Product RemoveProductFromFridge(int id, int productId)
    {
        var fridge = _fridges.FirstOrDefault(f => f.Id == id);
        var product = _products.FirstOrDefault(p => p.Id == productId);

        if (fridge == null) throw new InvalidOperationException("Fridge is not found!");
        if (product == null) throw new InvalidOperationException("Product is not found!");

        product.IsDeleted = true;

        var productUpdated = _db.Products.Update(product).Entity;

        //fridge.Products.Remove(productUpdated);
        //_db.Fridges.Update(fridge);

        _db.SaveChanges();

        return productUpdated;
    }

    public Product RestoreProductInFridge(int id, int productId)
    {
        var fridge = _fridges.FirstOrDefault(f => f.Id == id);
        var product = _products.FirstOrDefault(p => p.Id == productId);

        if (fridge == null) throw new InvalidOperationException("Fridge is not found!");
        if (product == null) throw new InvalidOperationException("Product is not found!");

        product.IsDeleted = false;

        var productUpdated = _db.Products.Update(product).Entity;

        //fridge.Products.Remove(productUpdated);
        //_db.Fridges.Update(fridge);

        _db.SaveChanges();

        return productUpdated;
    }

    public Fridge? DeleteFridge(int fridgeId)
    {
        var fridge = _fridges.FirstOrDefault(f => f.Id == fridgeId);
        if (fridge == null)
            return null;

        fridge.IsDeleted = true;

        _fridges.Update(fridge);
        _db.SaveChanges();

        return fridge;
    }

    public Fridge? RestoreFridge(int fridgeId)
    {
        var fridge = _fridges.FirstOrDefault(f => f.Id == fridgeId);
        if (fridge == null)
            return null;

        fridge.IsDeleted = false;

        _fridges.Update(fridge);
        _db.SaveChanges();

        return fridge;
    }
}