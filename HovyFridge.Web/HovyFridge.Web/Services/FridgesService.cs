using FluentResults;
using HovyFridge.Data;
using HovyFridge.Data.Entity;
using HovyFridge.Data.Repository.GenericRepositoryPattern;

namespace HovyFridge.Web.Services;

public class FridgesService
{
    private readonly FridgesRepository _fridgesRepository;
    private readonly ProductsRepository _productsRepository;
    public FridgesService(ApplicationContext applicationContext, FridgesRepository fridgesRepository, ProductsRepository productsRepository, FridgeAccessLevelsRepository fridgeAccessLevelsRepository)
    {
        _fridgesRepository = fridgesRepository;
        _productsRepository = productsRepository;
    }

    public async Task<Result<Fridge>> GetByIdAsync(long id)
    {
        try
        {
            var fridge = await _fridgesRepository.GetById(id);

            if (fridge == null)
                return Result.Fail($"Fridge with id {id} is not found!");

            fridge.Products = fridge.Products.Where(p => !p.IsDeleted).ToList();

            return Result.Ok(fridge);

        }
        catch (Exception ex)
        {
            return Result.Fail(ex.Message);
        }
    }

    public async Task<Result<Fridge>> UpdateAsync(Fridge fridge)
    {
        try
        {
            var updatedFridge = await _fridgesRepository.Update(fridge);

            return Result.Ok(updatedFridge);
        }
        catch (Exception ex)
        {
            return Result.Fail(ex.Message);
        }
    }


    public async Task<Result<List<Fridge>>> GetListAsync()
    {
        try
        {
            return Result.Ok(await _fridgesRepository.GetAll());
        }
        catch (Exception ex)
        {
            return Result.Fail(ex.Message);
        }
    }

    public async Task<Result<Fridge>> AddAsync(Fridge fridge)
    {
        try
        {
            return Result.Ok(await _fridgesRepository.Add(fridge));
        }
        catch (Exception ex)
        {
            return Result.Fail(ex.Message);
        }
    }

    public async Task<Result<Product>> PutProductAsync(long id, long productId)
    {
        try
        {
            var fridge = await _fridgesRepository.GetById(id);
            var product = await _productsRepository.GetById(productId);

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

            newProduct = await _productsRepository.Add(newProduct);

            fridge.Products.Add(newProduct);

            await _fridgesRepository.Update(fridge);

            return Result.Ok(newProduct);
        }
        catch (Exception ex)
        {
            return Result.Fail(ex.Message);
        }
    }

    public async Task<Result<Product>> RemoveProductAsync(long id, long productId)
    {
        try
        {
            var fridge = await _fridgesRepository.GetById(id);
            var product = await _productsRepository.GetById(productId);

            if (fridge == null) throw new InvalidOperationException("Fridge is not found!");
            if (product == null) throw new InvalidOperationException("Product is not found!");

            var deletedProduct = await _productsRepository.Delete(product);

            return Result.Ok(deletedProduct);
        }
        catch (Exception ex)
        {
            return Result.Fail(ex.Message);
        }
    }

    public async Task<Result<Product>> RestoreProductAsync(long id, long productId)
    {
        try
        {
            var fridge = await _fridgesRepository.GetById(id);
            var product = await _productsRepository.GetById(productId);

            if (fridge == null) throw new InvalidOperationException("Fridge is not found!");
            if (product == null) throw new InvalidOperationException("Product is not found!");

            var restoredProduct = await _productsRepository.Restore(product);

            return Result.Ok(restoredProduct);
        }
        catch (Exception ex)
        {
            return Result.Fail(ex.Message);
        }
    }

    public async Task<Result<Fridge>> DeleteByIdAsync(long fridgeId)
    {
        try
        {
            return Result.Ok(await _fridgesRepository.DeleteById(fridgeId));
        }
        catch (Exception ex)
        {
            return Result.Fail(ex.Message);
        }
    }

    public async Task<Result<Fridge>> RestoreByIdAsync(long fridgeId)
    {
        try
        {
            var fridge = await _fridgesRepository.RestoreById(fridgeId);

            return Result.Ok(fridge);
        }
        catch (Exception ex)
        {
            return Result.Fail(ex.Message);
        }
    }
}