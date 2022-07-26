using FluentResults;
using HovyFridge.Entity;

namespace HovyFridge.Services
{
    public interface IProductsService
    {
        Task<Result<Product>> AddAsync(Product product);
        Task<Result<Product>> DeleteByIdAsync(long productId);
        Task<Result<List<Product>>> GetAllAsync();
        Task<Result<Product>> GetByBarcodeAsync(string barcode);
        Task<Result<Product>> GetByIdAsync(long id);
        Task<Result<List<CountedGroupBy<long?>>>> GetGroupedByFridgeIdAsync();
        Task<Result<List<string>>> GetSuggestionsNamesAsync(string searchQuery);
        Task<Result<Product>> RestoreByIdAsync(long productId);
        Task<Result<Product>> UpdateAsync(Product product);
    }
}