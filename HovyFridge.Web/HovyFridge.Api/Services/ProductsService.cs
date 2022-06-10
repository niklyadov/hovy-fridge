using FluentResults;
using HovyFridge.Api.Data;
using HovyFridge.Api.Data.Entity;
using Microsoft.EntityFrameworkCore;


namespace HovyFridge.Api.Services;

public class ProductsService
{
    private ApplicationContext _db;
    private DbSet<Product> _products;
    public ProductsService(ApplicationContext applicationContext)
    {
        _db = applicationContext;
        _products = applicationContext.Products;
    }

    public Result<List<Product>> GetProducts(string? searchQuery)
    {
        try
        {
            var products = _products.Where(p => !p.FridgeId.HasValue && !p.IsDeleted).ToList();

            return Result.Ok(products);
        }
        catch (Exception ex)
        {
            return Result.Fail(ex.Message);
        }
    }

    public Result<Product> GetProductWithBarcode(string barcode)
    {
        try
        {
            var product = _products.FirstOrDefault(p => p.BarCode == barcode
                        && !p.FridgeId.HasValue && !p.IsDeleted);

            if (product == null) return Result.Fail("Product is not found");

            return Result.Ok(product);
        }
        catch (Exception ex)
        {
            return Result.Fail(ex.Message);
        }
    }

    public Result<Product> GetProductWithId(int id)
    {
        try
        {
            var product = _products.FirstOrDefault(p => p.Id == id);

            if (product == null) return Result.Fail("Product is not found");

            return Result.Ok(product);
        }
        catch (Exception ex)
        {
            return Result.Fail(ex.Message);
        }
    }

    public Result<Product> AddProduct(Product product)
    {
        try
        {
            var productInList = _products.FirstOrDefault(p => p.BarCode == product.BarCode);

            if (productInList == null)
            {

                if (product.FridgeId == 0)
                    product.FridgeId = null;

                _products.Add(product);

                _db.SaveChanges();

                return Result.Ok(product);
            }

            if (productInList != null && productInList.IsDeleted == true)
            {
                productInList.FridgeId = null;
                productInList.IsDeleted = false;
                productInList.Name = product.Name;

                return UpdateProduct(productInList);
            }

            return Result.Fail($"Product with barcode {product.BarCode} already exists in product list.");
        }
        catch (Exception ex)
        {
            return Result.Fail(ex.Message);
        }
    }

    public Result<Product> DeleteProduct(int productId)
    {
        try
        {
            var product = _products.FirstOrDefault(p => p.Id == productId);
            if (product == null)
                return Result.Fail($"Product with id {productId} is not found");

            product.IsDeleted = true;

            _products.Update(product);
            _db.SaveChanges();

            return Result.Ok(product);
        }
        catch (Exception ex)
        {
            return Result.Fail(ex.Message);
        }
    }

    public Result<Product> RestoreProduct(int productId)
    {
        try
        {
            var product = _products.FirstOrDefault(p => p.Id == productId);
            if (product == null)
                return Result.Fail($"Product with id {productId} is not found");

            product.IsDeleted = false;

            _products.Update(product);
            _db.SaveChanges();

            return Result.Ok(product);
        }
        catch (Exception ex)
        {
            return Result.Fail(ex.Message);
        }
    }

    public Result<Product> UpdateProduct(Product product)
    {
        try
        {
            if (product.FridgeId == 0)
                product.FridgeId = null;

            var updatedProduct = _products.Update(product).Entity;
            _db.SaveChanges();

            return Result.Ok(updatedProduct);
        }
        catch (Exception ex)
        {
            return Result.Fail(ex.Message);
        }
    }
}
