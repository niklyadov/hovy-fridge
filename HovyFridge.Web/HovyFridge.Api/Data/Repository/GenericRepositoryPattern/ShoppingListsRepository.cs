using HovyFridge.Api.Data.Entity;
using HovyFridge.Api.Data.Repository.GenericRepositoryPattern.Abstract;

namespace HovyFridge.Api.Data.Repository.GenericRepositoryPattern
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
