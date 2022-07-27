using FluentResults;
using HovyFridge.Entity;
using HovyFridge.Services;
using Microsoft.EntityFrameworkCore;

namespace HovyFridge.DAO.Services;

public class ProductsServiceRepository : IProductsService
{
    private readonly ApplicationContext _applicationContext;
    public ProductsServiceRepository(ApplicationContext applicationContext)
    {
        _applicationContext = applicationContext;
    }

    public async Task<Result<List<Product>>> GetAllAsync()
    {
        try
        {
            var products = await _applicationContext.Products
                .ToListAsync();

            return Result.Ok(products);
        }
        catch (Exception ex)
        {
            return Result.Fail(ex.Message);
        }
    }

    public async Task<Result<Product>> GetByBarcodeAsync(string barcode)
    {
        try
        {
            var product = await _applicationContext.Products
                .FirstOrDefaultAsync(p => p.BarCode == barcode);

            if (product == null) return Result.Fail("Product is not found");

            return Result.Ok(product);
        }
        catch (Exception ex)
        {
            return Result.Fail(ex.Message);
        }
    }

    public async Task<Result<Product>> GetByIdAsync(long id)
    {
        try
        {
            var product = await _applicationContext.Products
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null) return Result.Fail("Product is not found");

            return Result.Ok(product);
        }
        catch (Exception ex)
        {
            return Result.Fail(ex.Message);
        }
    }

    public async Task<Result<Product>> AddAsync(Product product)
    {
        try
        {
            var productInList = await _applicationContext.Products
                .FirstOrDefaultAsync(p => p.BarCode == product.BarCode);

            if (productInList == null)
            {

                if (product.FridgeId == 0)
                    product.FridgeId = null;

                await _applicationContext.Products.AddAsync(product);
                _applicationContext.Entry(product).State = EntityState.Added;
                await _applicationContext.SaveChangesAsync();

                return Result.Ok(product);
            }

            if (productInList != null && productInList.IsDeleted == true)
            {
                productInList.FridgeId = null;
                productInList.IsDeleted = false;
                productInList.Name = product.Name;

                return await UpdateAsync(productInList);
            }

            return Result.Fail($"Product with barcode {product.BarCode} already exists in product list.");
        }
        catch (Exception ex)
        {
            return Result.Fail(ex.Message);
        }
    }

    public async Task<Result<Product>> DeleteByIdAsync(long productId)
    {
        try
        {
            var product = await _applicationContext.Products
                .SingleAsync(f => f.Id == productId);

            product.IsDeleted = true;
            product.DeletedDateTime = DateTime.UtcNow;

            return await UpdateAsync(product);
        }
        catch (Exception ex)
        {
            return Result.Fail(ex.Message);
        }
    }

    public async Task<Result<Product>> RestoreByIdAsync(long productId)
    {
        try
        {
            var product = await _applicationContext.Products
                .SingleAsync(f => f.Id == productId);

            product.IsDeleted = false;
            product.DeletedDateTime = null;

            return await UpdateAsync(product);
        }
        catch (Exception ex)
        {
            return Result.Fail(ex.Message);
        }
    }

    public async Task<Result<Product>> UpdateAsync(Product product)
    {
        try
        {
            if (product.FridgeId == 0)
                product.FridgeId = null;

            _applicationContext.Products.Update(product);
            _applicationContext.Entry(product).State = EntityState.Modified;
            await _applicationContext.SaveChangesAsync();


            return Result.Ok(product);
        }
        catch (Exception ex)
        {
            return Result.Fail(ex.Message);
        }
    }

    public async Task<Result<List<CountedGroupBy<long?>>>> GetGroupedByFridgeIdAsync()
    {
        try
        {
            var result = await _applicationContext.Products
                .GroupBy(p => p.FridgeId)
                .Select(p => new CountedGroupBy<long?>(p.Key.HasValue ? p.Key.Value : null, p.Count()))
                .ToListAsync();

            return Result.Ok(result);
        }
        catch (Exception ex)
        {
            return Result.Fail(ex.Message);
        }
    }

    public async Task<Result<List<string>>> GetSuggestionsNamesAsync(string searchQuery)
    {
        try
        {
            if (string.IsNullOrEmpty(searchQuery))
                throw new ArgumentNullException(nameof(searchQuery));

            searchQuery = searchQuery.Trim().ToLower();

            var products = _applicationContext.Products
                .Where(p => p.IsDeleted == false && (
                            p.Name.ToLower().Contains(searchQuery) ||
                            p.Description != null && p.Description.ToLower().Contains(searchQuery) ||
                            p.BarCode.ToLower().Contains(searchQuery)));

            var productSuggestions = _applicationContext.ProductSuggestion
                .Where(ps => ps.Name.ToLower().Contains(searchQuery) ||
                             ps.Description != null && ps.Description.ToLower().Contains(searchQuery) ||
                             ps.BarCode.ToLower().Contains(searchQuery))
                .Union(products.Select(p => new ProductSuggestion()
                {
                    Name = p.Name,
                    BarCode = p.BarCode,
                    Description = p.Description
                }));

            var result = await productSuggestions
                .Select(ps => ps.Name)
                .ToListAsync();

            return Result.Ok(result);
        }
        catch (Exception ex)
        {
            return Result.Fail(ex.Message);
        }
    }
}
