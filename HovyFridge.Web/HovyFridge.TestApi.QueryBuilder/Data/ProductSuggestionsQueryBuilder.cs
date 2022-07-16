using HovyFridge.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace HovyFridge.TestApi.QueryBuilder.Data;

public class ProductSuggestionsQueryBuilder : QueryBuilder<ProductSuggestion>
{
    public ProductSuggestionsQueryBuilder(DbContext context) : base(context)
    {
    }
}