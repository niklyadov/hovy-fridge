using HovyFridge.Data;
using HovyFridge.Data.Entity;

namespace HovyFridge.TestApi.QueryBuilder.Data;

public class ProductSuggestionsQueryBuilder : QueryBuilder<ProductSuggestion, ApplicationContext>
{
    public ProductSuggestionsQueryBuilder(ApplicationContext context) : base(context)
    {
    }
}