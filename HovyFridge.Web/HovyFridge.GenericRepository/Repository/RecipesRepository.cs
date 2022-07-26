using HovyFridge.Entity;
using HovyFridge.GenericRepository.Repository.Abstract;

namespace HovyFridge.GenericRepository.Repository
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