using FluentResults;
using HovyFridge.Entity;

namespace HovyFridge.Services
{
    public interface IFridgesService
    {
        Task<Result<Fridge>> AddAsync(Fridge fridge);
        Task<Result<Fridge>> DeleteByIdAsync(long fridgeId);
        Task<Result<Fridge>> GetByIdAsync(long id);
        Task<Result<List<Fridge>>> GetListAsync();
        Task<Result<Product>> PutProductAsync(long id, long productId);
        Task<Result<Product>> RemoveProductAsync(long id, long productId);
        Task<Result<Fridge>> RestoreByIdAsync(long fridgeId);
        Task<Result<Fridge>> UpdateAsync(Fridge fridge);
        Task<Result<Product>> RestoreProductAsync(long id, long productId);
    }
}