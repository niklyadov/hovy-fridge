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

        public async Task<List<CountedGroupBy<long?>>> GetGroupedByFridgeId()
        {
            return await _dbContext.Products
                .GroupBy(p => p.FridgeId)
                .Select(p => new CountedGroupBy<long?>(p.Key.HasValue ? p.Key.Value : null, p.Count())).ToListAsync();
        }

        public async Task<List<string>> GetSuggestions(string searchQuery)
        {
            searchQuery = searchQuery.Trim().ToLower();

            var products = _dbContext.Products
                .Where(p => p.IsDeleted == false && (
                            p.Name.ToLower().Contains(searchQuery) || 
                            p.Description != null && p.Description.ToLower().Contains(searchQuery) ||
                            p.BarCode.ToLower().Contains(searchQuery)));

            var productSuggestions = _dbContext.ProductSuggestion
                .Where(ps => ps.Name.ToLower().Contains(searchQuery) ||
                             ps.Description != null && ps.Description.ToLower().Contains(searchQuery) ||
                             ps.BarCode.ToLower().Contains(searchQuery))
                .Union(products.Select(p => new ProductSuggestion()
                {
                    Name = p.Name,
                    BarCode = p.BarCode,
                    Description = p.Description
                }));

            return await productSuggestions
                .Select(ps => ps.Name)
                .ToListAsync();
        }
    }
}