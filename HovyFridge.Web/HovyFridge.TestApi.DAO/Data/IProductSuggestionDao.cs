using HovyFridge.Data.Entity;

namespace HovyFridge.TestApi.DAO.Data;

public interface IProductSuggestionDao
{
    public ProductSuggestion GetProductSuggestionById(long suggestionId);
    public List<ProductSuggestion> GetProductSuggestionsList();
    public List<ProductSuggestion> PutProductSuggestionsList(List<ProductSuggestion> suggestions);
}