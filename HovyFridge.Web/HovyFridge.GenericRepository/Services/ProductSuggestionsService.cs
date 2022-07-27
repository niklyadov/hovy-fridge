using FluentResults;
using HovyFridge.Entity;
using HovyFridge.GenericRepository.Repository;
using HovyFridge.Services;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace HovyFridge.GenericRepository.Services
{
    public class ProductSuggestionsService : IProductSuggestionsService
    {
        private readonly ProductSuggestionsRepository _productSuggestionsRepository;

        public ProductSuggestionsService(ProductSuggestionsRepository productSuggestionsRepository)
        {
            _productSuggestionsRepository = productSuggestionsRepository;
        }

        public async Task<Result<List<ProductSuggestion>>> GetAllAsync()
        {
            try
            {
                return Result.Ok(await _productSuggestionsRepository.GetAll());
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
                var productSuggestion = await _productSuggestionsRepository.GetById(id);

                if (productSuggestion == null)
                    throw new Exception("ProductSuggestion is not found!");

                return Result.Ok(productSuggestion);
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
                var createdProductSuggestion = await _productSuggestionsRepository.Add(ProductSuggestion);

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
                var updatedProductSuggestion = await _productSuggestionsRepository.Update(ProductSuggestion);

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
                var deletedProductSuggestion = await _productSuggestionsRepository.DeleteById(id);

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
                var restoredProductSuggestion = await _productSuggestionsRepository.DeleteById(id);

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
                    var insertResult = await _productSuggestionsRepository.Add(item);
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

                var searchResult = await _productSuggestionsRepository.SearchProductSuggestion(suggestionQuery);

                return Result.Ok(searchResult);
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }
    }
}
