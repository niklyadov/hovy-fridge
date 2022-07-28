using FluentResults;
using HovyFridge.Entity;
using HovyFridge.QueryBuilder.QueryBuilders;

namespace HovyFridge.QueryBuilder.Services
{
    public class ShoppingListsService
    {
        private readonly ApplicationContext _context;

        private ShoppingListsQueryBuilder _shoppingListsQueryBuilder
        {
            get => new ShoppingListsQueryBuilder(_context);
        }

        public ShoppingListsService(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Result<List<ShoppingList>>> GetAllAsync()
        {
            try
            {
                var shoppingLists = await _shoppingListsQueryBuilder
                    .WhereNotDeleted()
                    .ToListAsync();

                return Result.Ok(shoppingLists);
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
                var shoppingList = await _shoppingListsQueryBuilder
                    .WhereNotDeleted()
                    .WithId(id)
                    .SingleAsync();

                if (shoppingList == null)
                    throw new Exception("Shopping List is not found!");

                return Result.Ok();
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
                return Result.Ok(await _shoppingListsQueryBuilder.AddAsync(shoppingList));
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
                return Result.Ok(await _shoppingListsQueryBuilder.UpdateAsync(shoppingList));
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
                var shoppingListToDelete = await _shoppingListsQueryBuilder
                    .WhereNotDeleted()
                    .WithId(id)
                    .SingleAsync();

                var deletedRecipe = await _shoppingListsQueryBuilder.DeleteAsync(shoppingListToDelete);

                return Result.Ok(deletedRecipe);
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
                var shoppingListToRestore = await _shoppingListsQueryBuilder
                    .WhereNotDeleted()
                    .WithId(id)
                    .SingleAsync();

                var restoredShoppingList = await _shoppingListsQueryBuilder
                    .UndoDeleteAsync(shoppingListToRestore);

                return Result.Ok(restoredShoppingList);
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }
    }
}