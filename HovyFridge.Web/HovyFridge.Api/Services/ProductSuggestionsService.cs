using FluentResults;
using HovyFridge.Api.Data.Entity;
using HovyFridge.Api.Data.Repository.GenericRepositoryPattern;
using Newtonsoft.Json;

namespace HovyFridge.Api.Services
{
    public class ProductSuggestionsService
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
                var ProductSuggestion = await _productSuggestionsRepository.GetById(id);

                if (ProductSuggestion == null)
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
                var createdProductSuggestion = await _productSuggestionsRepository.Add(ProductSuggestion);

                return Result.Ok();
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
                var createdProductSuggestion = await _productSuggestionsRepository.Update(ProductSuggestion);

                return Result.Ok();
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
                var createdProductSuggestion = await _productSuggestionsRepository.DeleteById(id);

                return Result.Ok();
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
                var createdProductSuggestion = await _productSuggestionsRepository.DeleteById(id);

                return Result.Ok();
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

        public async Task<Result<List<ProductSuggestion>>> InsertFromFile(IFormFile file)
        {
            if (file.ContentType != "application/json")
                throw new FileLoadException("Content type of loaded file is not match application/json");

            using var fileStream = file.OpenReadStream();

            using var streamReader = new StreamReader(fileStream);

            var fileContent = streamReader.ReadToEnd();

            return await InsertFromJSON(fileContent);
        }
    }
}
