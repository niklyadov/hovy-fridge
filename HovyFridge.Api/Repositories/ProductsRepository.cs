using HovyFridge.Api.Entity;
using HovyFridge.Api.Entity.Common;
using Microsoft.EntityFrameworkCore;

namespace HovyFridge.Api.Repositories
{
    public class ProductsRepository : BaseRepository<Product, ApplicationContext>
    {
        public ProductsRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<Fridge>?> GetLocatedInById(int productId)
        {
            var product = await Context.Set<Product>()
                .Where(x => x.Id == productId)
                .Include(x => x.LocatedIn)
                    .ThenInclude(x => x.Products)
                .FirstOrDefaultAsync();

            return product == null ? null : product.LocatedIn;
        }
    }
}
