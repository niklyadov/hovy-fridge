using HovyFridge.Api.Entity;
using HovyFridge.Api.Entity.Common;


namespace HovyFridge.Api.Repositories
{
    public class ProductHistoryRepository : BaseRepository<ProductHistory, ApplicationContext>
    {
        public ProductHistoryRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }
    }
}
