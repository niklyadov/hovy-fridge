using HovyFridge.Api.Data.Entity;
using HovyFridge.Api.Data.Repository.GenericRepositoryPattern.Abstract;
using Microsoft.EntityFrameworkCore;

namespace HovyFridge.Api.Data.Repository.GenericRepositoryPattern
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
    }
}