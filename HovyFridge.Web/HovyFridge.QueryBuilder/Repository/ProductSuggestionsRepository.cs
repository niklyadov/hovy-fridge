using HovyFridge.Entity;
using HovyFridge.QueryBuilder.Repository.Abstract;
using Microsoft.EntityFrameworkCore;

namespace HovyFridge.QueryBuilder.Repository
{
    public class ProductSuggestionsRepository : BaseRepository<ProductSuggestion, ApplicationContext>
    {
        private readonly ApplicationContext _dbContext;
        public ProductSuggestionsRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<List<ProductSuggestion>> SearchProductSuggestion(string suggestionQuery)
        {
            //var param = new SqlParameter("@name", suggestionQuery);

            var suggestionsResult = _dbContext.ProductSuggestion
                .FromSqlRaw($"select * from search_suggestions_func('{suggestionQuery}');");

            return await suggestionsResult.ToListAsync();
        }
    }
}