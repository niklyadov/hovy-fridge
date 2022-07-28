using HovyFridge.Entity;
using Microsoft.EntityFrameworkCore;

namespace HovyFridge.QueryBuilder.QueryBuilders;

public class ProductsQueryBuilder : QueryBuilder<Product, ApplicationContext>
{
    public ProductsQueryBuilder(ApplicationContext context) : base(context)
    {
    }

    public ProductsQueryBuilder WithBarcode(string? barcode)
    {
        Query = Query.Where(p => !string.IsNullOrEmpty(p.BarCode));

        if (string.IsNullOrEmpty(barcode))
            Query = Query.Where(p => p.BarCode.Equals(barcode));

        return this;
    }

    public ProductsQueryBuilder WithOwnerId(long ownerUserId)
    {
        Query = Query.Where(p => p.OwnerId == ownerUserId);

        return this;
    }

    public ProductsQueryBuilder WithOwner(User ownerUser)
    {
        if (ownerUser.Id == 0)
            throw new InvalidOperationException("Valid user owner id is not provided");

        return WithOwnerId(ownerUser.Id);
    }

    public async Task<List<CountedGroupBy<long?>>> GetGroupedByFridgeIdAsync()
    {
        return await Query.GroupBy(p => p.FridgeId)
            .Select(p => new CountedGroupBy<long?>(p.Key.HasValue ? p.Key.Value : null, p.Count())).ToListAsync();
    }

    public async Task<List<string>> GetSuggestionsAsync(string searchQuery)
    {
        searchQuery = searchQuery.Trim().ToLower();

        var products = Context.Products
            .Where(p => p.IsDeleted == false && (
                        p.Name.ToLower().Contains(searchQuery) ||
                        p.Description != null && p.Description.ToLower().Contains(searchQuery) ||
                        p.BarCode.ToLower().Contains(searchQuery)));

        var productSuggestions = Context.ProductSuggestion
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