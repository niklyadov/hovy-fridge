using HovyFridge.Api.Entity;
using HovyFridge.Api.Entity.Common;


namespace HovyFridge.Api.Repositories
{
    public class ProductsHistoryRepository : BaseRepository<ProductHistory, ApplicationContext>
    {
        public ProductsHistoryRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }
    }
}
