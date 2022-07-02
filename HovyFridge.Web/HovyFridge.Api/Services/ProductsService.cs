using FluentResults;
using HovyFridge.Api.Data;
using HovyFridge.Api.Data.Entity;
using HovyFridge.Api.Data.Repository.GenericRepositoryPattern;
using Microsoft.EntityFrameworkCore;


namespace HovyFridge.Api.Services;

public class ProductsService
{
    private readonly FridgesRepository _fridgesRepository;
    private readonly ProductsRepository _productsRepository;

    //private ApplicationContext _db;
    //private DbSet<Product> _products;
    public ProductsService(ApplicationContext applicationContext, FridgesRepository fridgesRepository, ProductsRepository productsRepository)
    {
        //_db = applicationContext;
        //_products = applicationContext.Products;


        _fridgesRepository = fridgesRepository;
        _productsRepository = productsRepository;
    }

    public async Task<Result<List<Product>>> GetProducts(string? searchQuery)
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

    public async Task<Result<Product>> GetProductWithBarcode(string barcode)
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

    public async Task<Result<Product>> GetProductWithId(long id)
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

    public async Task<Result<Product>> AddProduct(Product product)
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

                return await UpdateProduct(productInList);
            }

            return Result.Fail($"Product with barcode {product.BarCode} already exists in product list.");
        }
        catch (Exception ex)
        {
            return Result.Fail(ex.Message);
        }
    }

    public async Task<Result<Product>> DeleteProduct(long productId)
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

    public async Task<Result<Product>> RestoreProduct(long productId)
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

    public async Task<Result<Product>> UpdateProduct(Product product)
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
}
