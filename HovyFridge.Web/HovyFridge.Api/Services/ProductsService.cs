using FluentResults;
using HovyFridge.Data;
using HovyFridge.Data.Entity;
using HovyFridge.Data.Repository.GenericRepositoryPattern;

namespace HovyFridge.Api.Services;

public class ProductsService
{
    private readonly FridgesRepository _fridgesRepository;
    private readonly ProductsRepository _productsRepository;

    public ProductsService(ApplicationContext applicationContext, FridgesRepository fridgesRepository, ProductsRepository productsRepository)
    {
        _fridgesRepository = fridgesRepository;
        _productsRepository = productsRepository;
    }

    public async Task<Result<List<Product>>> GetAllAsync()
    {
        try
        {
            var products = await _productsRepository.GetAll();

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
            var product = await _productsRepository.GetProductWithBarcode(barcode);

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
            var product = await _productsRepository.GetById(id);

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
            var productInList = await _productsRepository.GetProductWithBarcode(product.BarCode);

            if (productInList == null)
            {

                if (product.FridgeId == 0)
                    product.FridgeId = null;

                await _productsRepository.Add(product);

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
            var product = await _productsRepository.DeleteById(productId);

            if (product == null)
                return Result.Fail($"Product with id {productId} is not found");

            return Result.Ok(product);
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
            var product = await _productsRepository.RestoreById(productId);

            if (product == null)
                return Result.Fail($"Product with id {productId} is not found");

            return Result.Ok(product);
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

            var updatedProduct = await _productsRepository.Update(product);

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
            var result = await _productsRepository.GetGroupedByFridgeId();

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

            var result = await _productsRepository.GetSuggestions(searchQuery);

            return Result.Ok(result);
        }
        catch (Exception ex)
        {
            return Result.Fail(ex.Message);
        }
    }
}
