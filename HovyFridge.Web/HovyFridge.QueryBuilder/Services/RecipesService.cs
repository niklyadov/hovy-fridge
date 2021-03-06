using FluentResults;
using HovyFridge.Entity;
using HovyFridge.QueryBuilder.QueryBuilders;

namespace HovyFridge.QueryBuilder.Services
{
    public class RecipesService
    {
        private readonly ApplicationContext _context;

        private RecipesQueryBuilder _recipesQueryBuilder
        {
            get => new RecipesQueryBuilder(_context);
        }

        public RecipesService(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Result<List<Recipe>>> GetAllAsync()
        {
            try
            {
                var recipesList = await _recipesQueryBuilder
                    .WhereNotDeleted()
                    .ToListAsync();

                return Result.Ok(recipesList);
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
                var recipe = await _recipesQueryBuilder
                    .WhereNotDeleted()
                    .WithId(id)
                    .SingleAsync();

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
                return Result.Ok(await _recipesQueryBuilder.AddAsync(recipe));
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
                return Result.Ok(await _recipesQueryBuilder.UpdateAsync(recipe));
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
                var recipeToDelete = await _recipesQueryBuilder
                    .WhereNotDeleted()
                    .WithId(id)
                    .SingleAsync();

                return Result.Ok(await _recipesQueryBuilder.DeleteAsync(recipeToDelete));
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
                var recipeToRestore = await _recipesQueryBuilder
                    .WhereNotDeleted()
                    .WithId(id)
                    .SingleAsync();

                return Result.Ok(await _recipesQueryBuilder.UndoDeleteAsync(recipeToRestore));
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }
    }
}
