using FluentResults;
using HovyFridge.Entity;
using HovyFridge.Services;
using Microsoft.EntityFrameworkCore;

namespace HovyFridge.DAO.Services
{
    public class ShoppingListsServiceRepository : IShoppingListsService
    {
        private readonly ApplicationContext _applicationContext;
        public ShoppingListsServiceRepository(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public async Task<Result<List<ShoppingList>>> GetAllAsync()
        {
            try
            {
                return Result.Ok(await _applicationContext.ShoppingLists.ToListAsync());
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }

        public async Task<Result<ShoppingList>> GetByIdAsync(long id)
        {
            try
            {
                var shoppingList = await _applicationContext.ShoppingLists
                    .FirstOrDefaultAsync(sl => sl.Id == id);

                if (shoppingList == null)
                    throw new Exception("ShoppingList is not found!");

                return Result.Ok(shoppingList);
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }

        public async Task<Result<ShoppingList>> AddAsync(ShoppingList shoppingList)
        {
            try
            {
                await _applicationContext.ShoppingLists.AddAsync(shoppingList);
                _applicationContext.Entry(shoppingList).State = EntityState.Added;
                await _applicationContext.SaveChangesAsync();

                return Result.Ok(shoppingList);
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }

        public async Task<Result<ShoppingList>> UpdateAsync(ShoppingList shoppingList)
        {
            try
            {
                await _applicationContext.ShoppingLists.AddAsync(shoppingList);
                _applicationContext.Entry(shoppingList).State = EntityState.Modified;
                await _applicationContext.SaveChangesAsync();

                return Result.Ok(shoppingList);
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }

        public async Task<Result<ShoppingList>> DeleteByIdAsync(long id)
        {
            try
            {
                var shoppingList = await _applicationContext.ShoppingLists
                    .FirstOrDefaultAsync(sl => sl.Id == id);

                if (shoppingList == null)
                    throw new Exception("ShoppingList is not found!");

                shoppingList.IsDeleted = true;
                shoppingList.DeletedDateTime = DateTime.UtcNow;

                return await UpdateAsync(shoppingList);
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }

        public async Task<Result<ShoppingList>> RestoreByIdAsync(long id)
        {
            try
            {
                var shoppingList = await _applicationContext.ShoppingLists
                    .FirstOrDefaultAsync(sl => sl.Id == id);

                if (shoppingList == null)
                    throw new Exception("ShoppingList is not found!");

                shoppingList.IsDeleted = false;
                shoppingList.DeletedDateTime = null;

                return await UpdateAsync(shoppingList);
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }
    }
}
