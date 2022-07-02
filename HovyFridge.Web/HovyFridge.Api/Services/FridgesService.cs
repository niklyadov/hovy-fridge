using FluentResults;
using HovyFridge.Api.Data;
using HovyFridge.Api.Data.Entity;
using HovyFridge.Api.Data.Repository.GenericRepositoryPattern;
using Microsoft.EntityFrameworkCore;

namespace HovyFridge.Api.Services;

public class FridgesService
{
    private readonly FridgesRepository _fridgesRepository;
    private readonly ProductsRepository _productsRepository;
    private readonly FridgeAccessLevelsRepository _fridgeAccessLevelsRepository;

    private ApplicationContext _db;
    private DbSet<FridgeAccessLevel> _fridgeAccessLevels;
    public FridgesService(ApplicationContext applicationContext, FridgesRepository fridgesRepository, ProductsRepository productsRepository, FridgeAccessLevelsRepository fridgeAccessLevelsRepository)
    {
        _db = applicationContext;
        _fridgeAccessLevels = applicationContext.FridgeAccessLevels;

        _fridgesRepository = fridgesRepository;
        _productsRepository = productsRepository;
        _fridgeAccessLevelsRepository = fridgeAccessLevelsRepository;
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

        } catch (Exception ex)
        {
            return Result.Fail(ex.Message);
        }
    }

    public async Task<Result<Fridge>> UpdateFridge(Fridge fridge)
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


    public async Task<Result<List<Fridge>>> GetList()
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

    public async Task<Result<Fridge>> AddFridgeAsync(Fridge fridge)
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

    public async Task<Result<Product>> PutProduct(long id, long productId)
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

    public async Task<Result<Product>> RemoveProductFromFridge(long id, long productId)
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

    public async Task<Result<Product>> RestoreProductInFridge(long id, long productId)
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

    public async Task<Result<Fridge>> DeleteFridge(long fridgeId)
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

    public async Task<Result<Fridge>> RestoreFridge(long fridgeId)
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

    public Result<List<FridgeAccessLevel>> GetFridgeAccessLevels(long fridgeId)
    {
        var accessLevels = GetFridgeAccessLevelsByFridgeId(fridgeId);

        return Result.Ok(accessLevels);
    }

    public Result<FridgeAccessLevel> AddFridgeAccessLevel(long fridgeId, FridgeAccessLevel newAccessLevel)
    {
        var fridge = GetByIdAsync(fridgeId);

        if (fridge == null)
            return Result.Fail($"Fridge with id {fridgeId} is not found!");

        var accessLevel = GetFridgeAccessLevelByUserId(newAccessLevel.UserId);

        if (accessLevel != null)
            return Result.Fail($"Fridge access level with id {accessLevel} already exists for fridge with id {fridgeId}!");

        var addedAccessLevelResult = _fridgeAccessLevels.Add(newAccessLevel);

        _db.SaveChanges();

        return Result.Ok(addedAccessLevelResult.Entity);
    }

    public Result<FridgeAccessLevel> UpdateFridgeAccessLevel(long fridgeId, FridgeAccessLevel newAccessLevel)
    {
        var fridge = GetByIdAsync(fridgeId);

        if (fridge == null)
            return Result.Fail($"Fridge with id {fridgeId} is not found!");

        var accessLevel = GetFridgeAccessLevelByUserId(newAccessLevel.UserId);

        if (accessLevel == null)
            return Result.Fail($"Fridge access level with id {accessLevel} is not found for fridge with id {fridgeId}!");

        var addedAccessLevelResult = _fridgeAccessLevels.Update(newAccessLevel);

        _db.SaveChanges();

        return Result.Ok(addedAccessLevelResult.Entity);
    }

    public Result<FridgeAccessLevel> DeleteFridgeAccessLevel(long accessLevelId)
    {
        var accessLevel = GetFridgeAccessLevelById(accessLevelId);

        if (accessLevel == null)
            return Result.Fail($"Fridge access level with id {accessLevelId} is not found!");

        var addedAccessLevelResult = _fridgeAccessLevels.Remove(accessLevel);

        _db.SaveChanges();

        return Result.Ok(addedAccessLevelResult.Entity);
    }

    private List<FridgeAccessLevel> GetFridgeAccessLevelsByFridgeId(long fridgeId)
    {
        var fridge = GetByIdAsync(fridgeId);

        if (fridge == null)
            throw new Exception($"Fridge with id {fridgeId} is not found!");

        var accessLevel = _fridgeAccessLevels.Where(a => a.FridgeId == fridgeId).ToList();

        if (accessLevel.Count > 0)
            return accessLevel;

        return new List<FridgeAccessLevel>();
    }

    private FridgeAccessLevel? GetFridgeAccessLevelById(long accessToFridgeId)
    {
        var accessLevel = _fridgeAccessLevels.Where(a => a.Id == accessToFridgeId).FirstOrDefault();

        if (accessLevel != null)
            return accessLevel;

        return null;

        //throw new Exception($"Fridge access level with id {accessToFridgeId} is not found!");
    }

    private FridgeAccessLevel? GetFridgeAccessLevelByUserId(long userId)
    {
        var accessLevel = _fridgeAccessLevels.Where(a => a.UserId == userId).FirstOrDefault();

        if (accessLevel != null)
            return accessLevel;

        return null;

        //throw new Exception($"Fridge access level as associated with user id {userId} is not found!");
    }
}