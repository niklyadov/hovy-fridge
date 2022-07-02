using HovyFridge.Api.Data.Entity;
using HovyFridge.Api.Data.Repository.GenericRepositoryPattern.Abstract;

namespace HovyFridge.Api.Data.Repository.GenericRepositoryPattern
{
    public class RecipesRepository : BaseRepository<Recipe, ApplicationContext>
    {
        private readonly ApplicationContext _dbContext;
        public RecipesRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
