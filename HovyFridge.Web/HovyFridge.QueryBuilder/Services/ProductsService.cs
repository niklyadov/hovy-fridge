using FluentResults;
using HovyFridge.Entity;
using HovyFridge.QueryBuilder.QueryBuilders;
using HovyFridge.Services;

namespace HovyFridge.QueryBuilder.Services;

public class ProductsService : IProductsService
{
    private readonly ProductsQueryBuilder _productsQueryBuilder;

    public ProductsService(ProductsQueryBuilder productsQueryBuilder)
    {
        _productsQueryBuilder = productsQueryBuilder;
    }

    public async Task<Result<List<Product>>> GetAllAsync()
    {
        try
        {
            var products = await _productsQueryBuilder
                .WhereNotDeleted()
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
            var product = await _productsQueryBuilder
                .WithBarcode(barcode)
                .WhereNotDeleted()
                .FirstOrDefaultAsync();

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
            var product = await _productsQueryBuilder
                .WithId(id)
                .FirstOrDefaultAsync();

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
            var productInList = await _productsQueryBuilder
                .WithBarcode(product.BarCode)
                .WhereNotDeleted()
                .FirstOrDefaultAsync();

            if (productInList == null)
            {

                if (product.FridgeId == 0)
                    product.FridgeId = null;

                await _productsQueryBuilder.AddAsync(product);

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
            var product = await _productsQueryBuilder
                .WhereNotDeleted()
                .WithId(productId)
                .SingleAsync();

            return Result.Ok(await _productsQueryBuilder.DeleteAsync(product));
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
            var product = await _productsQueryBuilder
                .WhereDeleted()
                .WithId(productId)
                .SingleAsync();

            return Result.Ok(await _productsQueryBuilder.UndoDeleteAsync(product));
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

            var updatedProduct = await _productsQueryBuilder.UpdateAsync(product);

            return Result.Ok(updatedProduct);
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
            var result = await _productsQueryBuilder.GetGroupedByFridgeIdAsync();

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

            var result = await _productsQueryBuilder.GetSuggestionsAsync(searchQuery);

            return Result.Ok(result);
        }
        catch (Exception ex)
        {
            return Result.Fail(ex.Message);
        }
    }
}
