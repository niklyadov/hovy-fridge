using FluentResults;
using HovyFridge.Entity;
using Microsoft.AspNetCore.Http;

namespace HovyFridge.Services
{
    public interface IProductSuggestionsService
    {
        Task<Result<ProductSuggestion>> AddAsync(ProductSuggestion ProductSuggestion);
        Task<Result<ProductSuggestion>> DeleteByIdAsync(long id);
        Task<Result<List<ProductSuggestion>>> GetAllAsync();
        Task<Result<ProductSuggestion>> GetByIdAsync(long id);
        Task<Result<List<ProductSuggestion>>> InsertFromFileAsync(IFormFile file);
        Task<Result<ProductSuggestion>> RestoreByIdAsync(long id);
        Task<Result<List<ProductSuggestion>>> SearchAsync(string suggestionQuery);
        Task<Result<ProductSuggestion>> UpdateAsync(ProductSuggestion ProductSuggestion);
    }
}