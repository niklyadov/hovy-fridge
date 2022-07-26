using HovyFridge.Entity;
using HovyFridge.QueryBuilder.Repository.Abstract;

namespace HovyFridge.QueryBuilder.Repository
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