using HovyFridge.Api.Entity;
using HovyFridge.Api.Entity.Common;

namespace HovyFridge.Api.Repositories
{
    public class ProductRepository : BaseRepository<Product, ApplicationContext>
    {
        public ProductRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }
    }
}
