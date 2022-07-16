using HovyFridge.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace HovyFridge.TestApi.DAO.Data.Imp;

public class ProductSuggestionDao : IProductSuggestionDao
{
    private readonly DbContext _dbContext;
    
    public ProductSuggestionDao(DbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public ProductSuggestion GetProductSuggestionById(long suggestionId)
    {
        throw new NotImplementedException();
    }

    public List<ProductSuggestion> GetProductSuggestionsList()
    {
        throw new NotImplementedException();
    }

    public List<ProductSuggestion> PutProductSuggestionsList(List<ProductSuggestion> suggestions)
    {
        throw new NotImplementedException();
    }
}