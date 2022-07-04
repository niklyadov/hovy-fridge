using HovyFridge.Data.Entity;
using HovyFridge.Data.Repository.GenericRepositoryPattern.Abstract;

namespace HovyFridge.Data.Repository.GenericRepositoryPattern
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