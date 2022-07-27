using HovyFridge.Entity;

namespace HovyFridge.QueryBuilder.QueryBuilders;

public class ProductSuggestionsQueryBuilder : QueryBuilder<ProductSuggestion, ApplicationContext>
{
    public ProductSuggestionsQueryBuilder(ApplicationContext context) : base(context)
    {
    }

    public ProductSuggestionsQueryBuilder SearchQuery(string query)
    {
        // search by name
        Query = Query.Where(ps => ps.Name.ToLower().Contains(query.ToLower()));

        // search by description
        Query = Query.Union(Query.Where(ps =>
            !string.IsNullOrEmpty(ps.Description) &&
            ps.Description.ToLower().Contains(query.ToLower())));

        // search by barcode
        Query = Query.Union(Query.Where(ps =>
            ps.BarCode.Contains(query)));

        return this;
    }
}