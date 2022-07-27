using FluentResults;
using HovyFridge.Entity;
using HovyFridge.Services;
using Microsoft.EntityFrameworkCore;

namespace HovyFridge.DAO.Services
{
    public class RecipesServiceRepository : IRecipesService
    {
        private readonly ApplicationContext _applicationContext;
        public RecipesServiceRepository(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public async Task<Result<List<Recipe>>> GetAllAsync()
        {
            try
            {
                return Result.Ok(await _applicationContext.Recipes.ToListAsync());
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }

        public async Task<Result<Recipe>> GetByIdAsync(long id)
        {
            try
            {
                var recipe = await _applicationContext.Recipes
                    .FirstOrDefaultAsync(r => r.Id == id);

                if (recipe == null)
                    throw new Exception("Recipe is not found!");

                return Result.Ok();
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }

        public async Task<Result<Recipe>> AddAsync(Recipe recipe)
        {
            try
            {
                await _applicationContext.Recipes.AddAsync(recipe);
                _applicationContext.Entry(recipe).State = EntityState.Added;
                await _applicationContext.SaveChangesAsync();

                return Result.Ok(recipe);
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }

        public async Task<Result<Recipe>> UpdateAsync(Recipe recipe)
        {
            try
            {
                _applicationContext.Recipes.Update(recipe);
                _applicationContext.Entry(recipe).State = EntityState.Modified;
                await _applicationContext.SaveChangesAsync();

                return Result.Ok(recipe);
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }

        public async Task<Result<Recipe>> DeleteByIdAsync(long id)
        {
            try
            {
                var recipe = await _applicationContext.Recipes
                    .FirstOrDefaultAsync(r => r.Id == id);

                if (recipe == null)
                    throw new Exception("Recipe is not found!");

                recipe.IsDeleted = true;
                recipe.DeletedDateTime = DateTime.UtcNow;

                return await UpdateAsync(recipe);
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }

        public async Task<Result<Recipe>> RestoreByIdAsync(long id)
        {
            try
            {
                var recipe = await _applicationContext.Recipes
                    .FirstOrDefaultAsync(r => r.Id == id);

                if (recipe == null)
                    throw new Exception("Recipe is not found!");

                recipe.IsDeleted = false;
                recipe.DeletedDateTime = null;

                return await UpdateAsync(recipe);
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }
    }
}
