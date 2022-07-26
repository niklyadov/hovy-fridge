using HovyFridge.Entity;

namespace HovyFridge.QueryBuilder.QueryBuilders;

public class ProductSuggestionsQueryBuilder : QueryBuilder<ProductSuggestion, ApplicationContext>
{
    public ProductSuggestionsQueryBuilder(ApplicationContext context) : base(context)
    {
    }
}