using HovyFridge.Api.Data.Entity;
using HovyFridge.Api.Data.Repository.GenericRepositoryPattern.Abstract;

namespace HovyFridge.Api.Data.Repository.GenericRepositoryPattern
{
    public class ProductSuggestionsRepository : BaseRepository<ProductSuggestion, ApplicationContext>
    {
        private readonly ApplicationContext _dbContext;
        public ProductSuggestionsRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}