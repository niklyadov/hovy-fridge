using FluentResults;
using HovyFridge.Entity;
using HovyFridge.QueryBuilder.QueryBuilders;
using HovyFridge.Services;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace HovyFridge.QueryBuilder.Services
{
    public class ProductSuggestionsService : IProductSuggestionsService
    {
        private readonly ProductSuggestionsQueryBuilder _productSuggestionsQueryBuilder;

        public ProductSuggestionsService(ProductSuggestionsQueryBuilder productSuggestionsQueryBuilder)
        {
            _productSuggestionsQueryBuilder = productSuggestionsQueryBuilder;
        }

        public async Task<Result<List<ProductSuggestion>>> GetAllAsync()
        {
            try
            {
                return Result.Ok(await _productSuggestionsQueryBuilder.WhereNotDeleted().ToListAsync());
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }

        public async Task<Result<ProductSuggestion>> GetByIdAsync(long id)
        {
            try
            {
                var productSuggestion = await _productSuggestionsQueryBuilder
                    .WhereNotDeleted()
                    .WithId(id)
                    .FirstOrDefaultAsync();

                if (productSuggestion == null)
                    throw new Exception("ProductSuggestion is not found!");

                return Result.Ok();
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }

        public async Task<Result<ProductSuggestion>> AddAsync(ProductSuggestion ProductSuggestion)
        {
            try
            {
                var createdProductSuggestion = await _productSuggestionsQueryBuilder.AddAsync(ProductSuggestion);

                return Result.Ok(createdProductSuggestion);
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }

        public async Task<Result<ProductSuggestion>> UpdateAsync(ProductSuggestion ProductSuggestion)
        {
            try
            {
                var updatedProductSuggestion = await _productSuggestionsQueryBuilder.UpdateAsync(ProductSuggestion);

                return Result.Ok(updatedProductSuggestion);
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }

        public async Task<Result<ProductSuggestion>> DeleteByIdAsync(long id)
        {
            try
            {
                var suggestionToDelete = await _productSuggestionsQueryBuilder.WhereNotDeleted().WithId(id).SingleAsync();
                var deletedProductSuggestion = await _productSuggestionsQueryBuilder.DeleteAsync(suggestionToDelete);

                return Result.Ok(deletedProductSuggestion);
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }

        public async Task<Result<ProductSuggestion>> RestoreByIdAsync(long id)
        {
            try
            {
                var suggestionToRestore = await _productSuggestionsQueryBuilder.WhereDeleted().WithId(id).SingleAsync();
                var restoredProductSuggestion = await _productSuggestionsQueryBuilder.UndoDeleteAsync(suggestionToRestore);

                return Result.Ok(restoredProductSuggestion);
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }

        private async Task<Result<List<ProductSuggestion>>> InsertFromJSON(string json)
        {
            try
            {
                var insertedSuggestions = new List<ProductSuggestion>();
                var suggestionsToInsert = JsonConvert.DeserializeObject<List<ProductSuggestion>>(json);

                if (suggestionsToInsert == null || suggestionsToInsert.Count == 0)
                    throw new FileLoadException("No suggestions in file!");

                foreach (var item in suggestionsToInsert)
                {
                    var insertResult = await _productSuggestionsQueryBuilder.AddAsync(item);
                    insertedSuggestions.Add(insertResult);
                }

                return Result.Ok(insertedSuggestions);
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }

        public async Task<Result<List<ProductSuggestion>>> InsertFromFileAsync(IFormFile file)
        {
            try
            {
                if (file.ContentType != "application/json")
                    throw new FileLoadException("Content type of loaded file is not match application/json");

                using var fileStream = file.OpenReadStream();

                using var streamReader = new StreamReader(fileStream);

                var fileContent = streamReader.ReadToEnd();

                return await InsertFromJSON(fileContent);
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }

        public async Task<Result<List<ProductSuggestion>>> SearchAsync(string suggestionQuery)
        {
            try
            {
                suggestionQuery = new Regex("[^a-zA-Zа-яА-Я0-9 -_]").Replace(suggestionQuery, "");
                suggestionQuery = suggestionQuery.ToLower().Trim();

                if (string.IsNullOrEmpty(suggestionQuery))
                    throw new Exception();

                var searchResult = await _productSuggestionsQueryBuilder
                    .SearchQuery(suggestionQuery)
                    .WhereNotDeleted()
                    .ToListAsync();

                return Result.Ok(searchResult);
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }
    }
}