using FluentResults;
using HovyFridge.Entity;
using HovyFridge.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace HovyFridge.DAO.Services
{
    public class ProductSuggestionsServiceRepository : IProductSuggestionsService
    {
        private readonly ApplicationContext _applicationContext;
        public ProductSuggestionsServiceRepository(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public async Task<Result<List<ProductSuggestion>>> GetAllAsync()
        {
            try
            {
                return Result.Ok(await _applicationContext.ProductSuggestion
                    .ToListAsync());
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
                var productSuggestion = await _applicationContext.ProductSuggestion
                    .FirstOrDefaultAsync(ps => ps.Id == id);

                if (productSuggestion == null)
                    throw new Exception("ProductSuggestion is not found!");

                return Result.Ok(productSuggestion);
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }

        public async Task<Result<ProductSuggestion>> AddAsync(ProductSuggestion productSuggestion)
        {
            try
            {
                await _applicationContext.ProductSuggestion.AddAsync(productSuggestion);
                _applicationContext.Entry(productSuggestion).State = EntityState.Added;
                await _applicationContext.SaveChangesAsync();

                return Result.Ok(productSuggestion);
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }

        public async Task<Result<ProductSuggestion>> UpdateAsync(ProductSuggestion productSuggestion)
        {
            try
            {
                _applicationContext.ProductSuggestion.Update(productSuggestion);
                _applicationContext.Entry(productSuggestion).State = EntityState.Modified;
                await _applicationContext.SaveChangesAsync();

                return Result.Ok(productSuggestion);
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
                var productSuggestion = await _applicationContext.ProductSuggestion
                        .FirstOrDefaultAsync(ps => ps.Id == id && !ps.IsDeleted);

                if (productSuggestion == null)
                    throw new Exception("Not deleted ProductSuggestion is not found!");

                productSuggestion.IsDeleted = true;
                productSuggestion.DeletedDateTime = DateTime.UtcNow;

                return await UpdateAsync(productSuggestion);
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
                var productSuggestion = await _applicationContext.ProductSuggestion
                        .FirstOrDefaultAsync(ps => ps.Id == id && ps.IsDeleted);

                if (productSuggestion == null)
                    throw new Exception("Deleted ProductSuggestion is not found!");

                productSuggestion.IsDeleted = false;
                productSuggestion.DeletedDateTime = null;

                return await UpdateAsync(productSuggestion);
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
                    var insertResult = await AddAsync(item);

                    if (insertResult.IsFailed)
                        throw new Exception($"Cannot insert from json! {string.Join(",", insertResult.Errors)}");

                    insertedSuggestions.Add(insertResult.Value);
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
                    throw new InvalidOperationException("Suggestion query is empty");

                var searchResult = await _applicationContext.ProductSuggestion
                    .Where(ps => ps.Name.ToLower().Contains(suggestionQuery.ToLower()))

                    .Union(_applicationContext.ProductSuggestion.Where(ps =>
                                    !string.IsNullOrEmpty(ps.Description) &&
                                    ps.Description.ToLower().Contains(suggestionQuery.ToLower())))

                    .Union(_applicationContext.ProductSuggestion.Where(ps =>
                                    ps.BarCode.Contains(suggestionQuery))).ToListAsync();

                return Result.Ok(searchResult);
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }
    }
}
