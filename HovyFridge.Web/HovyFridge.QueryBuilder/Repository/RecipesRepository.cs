using HovyFridge.Entity;
using HovyFridge.QueryBuilder.Repository.Abstract;

namespace HovyFridge.QueryBuilder.Repository
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