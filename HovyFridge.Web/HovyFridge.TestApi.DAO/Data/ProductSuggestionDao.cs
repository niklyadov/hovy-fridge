using HovyFridge.Data;
using HovyFridge.Data.Entity;

namespace HovyFridge.TestApi.DAO.Data;

public class ProductSuggestionDao : DaoBase
{
    private readonly ApplicationContext _db;

    public ProductSuggestionDao(ApplicationContext db)
    {
        _db = db;
    }

    public ProductSuggestion? GetProductSuggestionById(long suggestionId)
    {
        return _db.ProductSuggestion.SingleOrDefault(p => p.Id == suggestionId);
    }

    public List<ProductSuggestion> GetProductSuggestionsList()
    {
        return _db.ProductSuggestion.ToList();
    }

    public bool PutProductSuggestionsList(List<ProductSuggestion> suggestions)
    {
        _db.ProductSuggestion.AddRange(suggestions);

        _db.SaveChanges();

        return true;
    }
}