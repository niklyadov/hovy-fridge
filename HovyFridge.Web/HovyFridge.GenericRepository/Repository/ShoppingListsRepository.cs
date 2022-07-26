using HovyFridge.Entity;
using HovyFridge.GenericRepository.Repository.Abstract;

namespace HovyFridge.GenericRepository.Repository
{
    public class ShoppingListsRepository : BaseRepository<ShoppingList, ApplicationContext>
    {
        private readonly ApplicationContext _dbContext;
        public ShoppingListsRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}