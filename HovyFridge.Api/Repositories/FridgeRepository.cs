using HovyFridge.Api.Entity;
using HovyFridge.Api.Entity.Common;

namespace HovyFridge.Api.Repositories
{
    public class FridgeRepository : BaseRepository<Fridge, ApplicationContext>
    {
        public FridgeRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }
    }
}
