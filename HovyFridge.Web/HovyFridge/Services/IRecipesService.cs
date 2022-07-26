using FluentResults;
using HovyFridge.Entity;

namespace HovyFridge.Services
{
    public interface IRecipesService
    {
        Task<Result<Recipe>> AddAsync(Recipe recipe);
        Task<Result<Recipe>> DeleteByIdAsync(long id);
        Task<Result<List<Recipe>>> GetAllAsync();
        Task<Result<Recipe>> GetByIdAsync(long id);
        Task<Result<Recipe>> RestoreByIdAsync(long id);
        Task<Result<Recipe>> UpdateAsync(Recipe recipe);
    }
}