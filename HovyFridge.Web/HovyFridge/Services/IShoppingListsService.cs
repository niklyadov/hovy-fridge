using FluentResults;
using HovyFridge.Entity;

namespace HovyFridge.Services
{
    public interface IShoppingListsService
    {
        Task<Result<ShoppingList>> AddAsync(ShoppingList shoppingList);
        Task<Result<ShoppingList>> DeleteByIdAsync(long id);
        Task<Result<List<ShoppingList>>> GetAllAsync();
        Task<Result<ShoppingList>> GetByIdAsync(long id);
        Task<Result<ShoppingList>> RestoreByIdAsync(long id);
        Task<Result<ShoppingList>> UpdateAsync(ShoppingList shoppingList);
    }
}