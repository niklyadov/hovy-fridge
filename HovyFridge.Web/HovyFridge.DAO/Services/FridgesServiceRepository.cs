using FluentResults;
using HovyFridge.Entity;
using HovyFridge.Services;
using Microsoft.EntityFrameworkCore;

namespace HovyFridge.DAO.Services;

public class FridgesServiceRepository : IFridgesService
{
    private readonly ApplicationContext _applicationContext;
    public FridgesServiceRepository(ApplicationContext applicationContext)
    {
        _applicationContext = applicationContext;
    }

    public async Task<Result<Fridge>> GetByIdAsync(long id)
    {
        try
        {
            var fridge = await _applicationContext.Fridges
                .Where(f => f.Id == id)
                .FirstOrDefaultAsync();

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
            _applicationContext.Fridges.Update(fridge);
            _applicationContext.Entry(fridge).State = EntityState.Modified;
            await _applicationContext.SaveChangesAsync();

            return Result.Ok(fridge);
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
            return Result.Ok(await _applicationContext.Fridges.ToListAsync());
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
            await _applicationContext.Fridges.AddAsync(fridge);
            _applicationContext.Entry(fridge).State = EntityState.Added;
            await _applicationContext.SaveChangesAsync();

            return Result.Ok(fridge);
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
            var fridge = await _applicationContext.Fridges.FirstOrDefaultAsync(f => f.Id == id);
            var product = await _applicationContext.Products.FirstOrDefaultAsync(p => p.Id == productId);

            if (fridge == null) throw new InvalidOperationException("Fridge is not found!");
            if (product == null) throw new InvalidOperationException("Product is not found!");

            var newProduct = new Product()
            {
                FridgeId = id,
                BarCode = product.BarCode,
                Name = product.Name,
                IsDeleted = false,
                CreatedDateTime = DateTime.UtcNow
            };

            await _applicationContext.Products.AddAsync(newProduct);
            _applicationContext.Entry(newProduct).State = EntityState.Added;
            await _applicationContext.SaveChangesAsync();


            fridge.Products.Add(newProduct);

            await UpdateAsync(fridge);

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
            var fridge = await _applicationContext.Fridges.FirstOrDefaultAsync(f => f.Id == id);
            var product = await _applicationContext.Products.FirstOrDefaultAsync(p => p.Id == productId);

            if (fridge == null) throw new InvalidOperationException("Fridge is not found!");
            if (product == null) throw new InvalidOperationException("Product is not found!");

            product.IsDeleted = true;
            product.DeletedDateTime = DateTime.UtcNow;

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

    public async Task<Result<Product>> RestoreProductAsync(long id, long productId)
    {
        try
        {
            var fridge = await _applicationContext.Fridges.FirstOrDefaultAsync(f => f.Id == id);
            var product = await _applicationContext.Products.FirstOrDefaultAsync(p => p.Id == productId);

            if (fridge == null) throw new InvalidOperationException("Fridge is not found!");
            if (product == null) throw new InvalidOperationException("Product is not found!");

            product.IsDeleted = false;
            product.DeletedDateTime = null;

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

    public async Task<Result<Fridge>> DeleteByIdAsync(long fridgeId)
    {
        try
        {
            var fridge = await _applicationContext.Fridges.SingleAsync(f => f.Id == fridgeId);

            fridge.IsDeleted = true;
            fridge.DeletedDateTime = DateTime.UtcNow;

            return await UpdateAsync(fridge);
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
            var fridge = await _applicationContext.Fridges.SingleAsync(f => f.Id == fridgeId);

            fridge.IsDeleted = false;
            fridge.DeletedDateTime = null;

            return await UpdateAsync(fridge);
        }
        catch (Exception ex)
        {
            return Result.Fail(ex.Message);
        }
    }
}