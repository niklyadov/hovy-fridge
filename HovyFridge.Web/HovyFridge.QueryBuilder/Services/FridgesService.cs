using FluentResults;
using HovyFridge.Entity;
using HovyFridge.QueryBuilder.QueryBuilders;
using HovyFridge.Services;

namespace HovyFridge.QueryBuilder.Services;

public class FridgesService : IFridgesService
{
    private readonly ApplicationContext _context;

    private FridgesQueryBuilder _fridgesQueryBuilder { 
        get => new FridgesQueryBuilder(_context); 
    }

    private ProductsQueryBuilder _productsQueryBuilder { 
        get => new ProductsQueryBuilder(_context); 
    }

    public FridgesService (ApplicationContext context)
    {
        _context = context;
    }

    public async Task<Result<Fridge>> GetByIdAsync(long id)
    {
        try
        {
            var fridge = await _fridgesQueryBuilder
                .IncludeProducts()
                .WithId(id)
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
            await _fridgesQueryBuilder.UpdateAsync(fridge);
            var updatedFridge = await _fridgesQueryBuilder.WithId(fridge.Id).SingleAsync();
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
            return Result.Ok(await _fridgesQueryBuilder.ToListAsync());
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
            return Result.Ok(await _fridgesQueryBuilder.AddAsync(fridge));
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
            var fridge = await _fridgesQueryBuilder
                .WithId(id)
                .SingleAsync();

            var product = await _productsQueryBuilder
                .WithId(productId)
                .SingleAsync();

            var newProduct = new Product()
            {
                FridgeId = id,
                BarCode = product.BarCode,
                Name = product.Name,
                IsDeleted = false,
                CreatedDateTime = DateTime.UtcNow
            };

            newProduct = await _productsQueryBuilder.AddAsync(newProduct);

            fridge.Products.Add(newProduct);

            await _fridgesQueryBuilder.UpdateAsync(fridge);

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
            var fridge = await _fridgesQueryBuilder.WithId(id).SingleAsync();
            var product = await _productsQueryBuilder.WithId(productId).SingleAsync();
            var deletedProduct = await _productsQueryBuilder.DeleteAsync(product);

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
            var fridge = await _fridgesQueryBuilder.WithId(id).SingleAsync();
            var product = await _productsQueryBuilder.WithId(productId).SingleAsync();
            var restoredProduct = await _productsQueryBuilder.UndoDeleteAsync(product);

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
            var fridge = await _fridgesQueryBuilder
                .WithId(fridgeId)
                //.WhereNotDeleted()
                .SingleAsync();

            var deletedFridge = await _fridgesQueryBuilder.DeleteAsync(fridge);

            return Result.Ok(deletedFridge);
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
            var fridge = await _fridgesQueryBuilder
                .WithId(fridgeId)
                //.WhereDeleted()
                .SingleAsync();

            var restoredFridge = await _fridgesQueryBuilder
                .UndoDeleteAsync(fridge);

            return Result.Ok(restoredFridge);
        }
        catch (Exception ex)
        {
            return Result.Fail(ex.Message);
        }
    }
}