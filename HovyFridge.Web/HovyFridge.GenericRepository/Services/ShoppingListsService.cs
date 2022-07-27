using FluentResults;
using HovyFridge.Entity;
using HovyFridge.GenericRepository.Repository;
using HovyFridge.Services;

namespace HovyFridge.GenericRepository.Services
{
    public class ShoppingListsService : IShoppingListsService
    {
        private ShoppingListsRepository _shoppingListsRepository;

        public ShoppingListsService(ShoppingListsRepository shoppingListsRepository)
        {
            _shoppingListsRepository = shoppingListsRepository;
        }

        public async Task<Result<List<ShoppingList>>> GetAllAsync()
        {
            try
            {
                return Result.Ok(await _shoppingListsRepository.GetAll());
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
                var shoppingList = await _shoppingListsRepository.GetById(id);

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
                var createdShoppingList = await _shoppingListsRepository.Add(shoppingList);

                return Result.Ok(createdShoppingList);
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
                var updatedShoppingList = await _shoppingListsRepository.Update(shoppingList);

                return Result.Ok(updatedShoppingList);
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
                var deletedShoppingList = await _shoppingListsRepository.DeleteById(id);

                return Result.Ok(deletedShoppingList);
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
                var restoredShoppingList = await _shoppingListsRepository.DeleteById(id);

                return Result.Ok(restoredShoppingList);
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }
    }
}
