using HovyFridge.Api.Entity;
using HovyFridge.Api.Repositories;
using HovyFridge.Api.Services.Etc;

namespace HovyFridge.Api.Services
{
    public class ProductsService
    {
        private readonly FridgesRepository _fridgesRepository;
        private readonly ProductsRepository _productsRepository;
        private readonly ProductsHistoryRepository _productsHistoryRepository;
        private readonly FridgesService _fridgesService;

        public ProductsService(IServiceProvider serviceProvider)
        {
            _fridgesRepository = serviceProvider.GetRequiredService<FridgesRepository>();
            _productsRepository = serviceProvider.GetRequiredService<ProductsRepository>();
            _productsHistoryRepository = serviceProvider.GetRequiredService<ProductsHistoryRepository>();
            _fridgesService = serviceProvider.GetRequiredService<FridgesService>();
        }


        public async Task<ServiceResult<List<Product>>> AddProduct(Product product)
        {
            product = await _productsRepository.Add(product);
            return new ServiceResult<List<Product>>()
            {
                Success = product != null,
                Result = await _productsRepository.GetAll()
            };
        }

        public async Task<ServiceResult<List<Product>>> DeleteProductById(int productId)
        {
            var product = await _productsRepository.GetById(productId);

            if (product != null)
            {
                var locatedInFridges = await _productsRepository.GetLocatedInById(productId);
                if (locatedInFridges != null)
                {
                    foreach (var fridge in locatedInFridges)
                    {
                        await _fridgesService.PopProductFromFridgeAsync(fridge.Id, productId);
                    }
                }

                product = await _productsRepository.Delete(productId);
            }

            return new ServiceResult<List<Product>>()
            {
                Success = product != null,
                Result = await _productsRepository.GetAll()
            };
        }

        public async Task<ServiceResult<List<Product>>> GetProductsList()
        {
            return new ServiceResult<List<Product>>()
            {
                Success = true,
                Result = await _productsRepository.GetAll()
            };
        }
    }
}
