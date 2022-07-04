using HovyFridge.Data.Entity;
using HovyFridge.Data.Repository.GenericRepositoryPattern.Abstract;

namespace HovyFridge.Data.Repository.GenericRepositoryPattern
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