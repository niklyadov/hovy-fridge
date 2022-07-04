using HovyFridge.Data.Entity;
using HovyFridge.Data.Repository.GenericRepositoryPattern.Abstract;
using Microsoft.EntityFrameworkCore;

namespace HovyFridge.Data.Repository.GenericRepositoryPattern
{
    public class ProductsRepository : BaseRepository<Product, ApplicationContext>
    {
        private readonly ApplicationContext _dbContext;
        public ProductsRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Product?> GetProductWithBarcode(string barcode, bool ignoreDeleted = true)
        {
            return await _dbContext.Products.FirstOrDefaultAsync(p => p.BarCode == barcode
                           && !ignoreDeleted || !p.FridgeId.HasValue && !p.IsDeleted);
        }

        public async Task<List<CountedGroupBy<long?>>> GetProductsGroupedByFridgeId()
        {
            return await _dbContext.Products
                .GroupBy(p => p.FridgeId)
                .Select(p => new CountedGroupBy<long?>(p.Key.HasValue ? p.Key.Value : null, p.Count())).ToListAsync();
        }
    }
}