using FluentResults;
using HovyFridge.Entity;
using HovyFridge.GenericRepository.Repository;
using HovyFridge.Services;

namespace HovyFridge.GenericRepository.Services
{
    public class RecipesService : IRecipesService
    {
        private RecipesRepository _recipesRepository;

        public RecipesService(RecipesRepository recipesRepository)
        {
            _recipesRepository = recipesRepository;
        }

        public async Task<Result<List<Recipe>>> GetAllAsync()
        {
            try
            {
                return Result.Ok(await _recipesRepository.GetAll());
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
                var recipe = await _recipesRepository.GetById(id);

                if (recipe == null)
                    throw new Exception("Recipe is not found!");

                return Result.Ok(recipe);
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
                var createdRecipe = await _recipesRepository.Add(recipe);

                return Result.Ok(createdRecipe);
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
                var updatedRecipe = await _recipesRepository.Update(recipe);

                return Result.Ok(updatedRecipe);
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
                var deletedRecipe = await _recipesRepository.DeleteById(id);

                return Result.Ok(deletedRecipe);
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
                var restoredRecipe = await _recipesRepository.DeleteById(id);

                return Result.Ok(restoredRecipe);
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }
    }
}
