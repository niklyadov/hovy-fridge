using HovyFridge.Api.Entity;
using HovyFridge.Api.Repositories;
using HovyFridge.Api.Services.Etc;

namespace HovyFridge.Api.Services
{
    public class FridgesService
    {
        private readonly FridgesRepository _fridgesRepository;
        private readonly ProductsRepository _productsRepository;
        private readonly ProductsHistoryRepository _productsHistoryRepository;

        public FridgesService(IServiceProvider serviceProvider)
        {
            _fridgesRepository          = serviceProvider.GetRequiredService<FridgesRepository>();
            _productsRepository         = serviceProvider.GetRequiredService<ProductsRepository>();
            _productsHistoryRepository  = serviceProvider.GetRequiredService<ProductsHistoryRepository>();
        }

        public async Task<ServiceResult<Product?>> PushProductIntoFridgeAsync(int fridgeId, int productId)
        {
            var product = await _productsRepository.GetById(productId);

            if(product == null) return new ServiceResult<Product?>();

            var fridge = await _fridgesRepository.PushProduct(fridgeId, product);
            if (fridge == null)
            {
                return new ServiceResult<Product?> { Success = false };
            }

            await _productsHistoryRepository.Add(new ProductHistory()
            {
                ProductHistoryOperation = ProductHistoryOperation.Added,
                Product = product,
                Fridge = fridge,
                OperationDate = DateTime.Now
            });

            return new ServiceResult<Product?> { 
                Success = true, 
                Result = product 
            };
        }

        public async Task<ServiceResult<Product?>> PopProductFromFridgeAsync(int fridgeId, int productId)
        {
            var product = await _productsRepository.GetById(productId);

            if (product == null) return new ServiceResult<Product?>();

            var fridge = await _fridgesRepository.PopProduct(fridgeId, product);
            if (fridge == null)
            {
                return new ServiceResult<Product?> { Success = false };
            }

            await _productsHistoryRepository.Add(new ProductHistory()
            {
                ProductHistoryOperation = ProductHistoryOperation.Deleted,
                Product = product,
                Fridge = fridge,
                OperationDate = DateTime.Now
            });

            return new ServiceResult<Product?> { Success = true, Result = product };
        }

        public async Task<ServiceResult<Fridge?>> GetFridgeByIdAsync(int fridgeId)
        {
            var fridge = await _fridgesRepository.GetDetailedFridgeById(fridgeId);

            return new ServiceResult<Fridge?> { 
                Success = fridge != null, 
                Result = fridge 
            };
        }

        public async Task<ServiceResult<ICollection<Fridge>>> GetFridgesListAsync()
        {
            return new ServiceResult<ICollection<Fridge>>
            {
                Success = true,
                Result = await _fridgesRepository.GetFridgesListAsync()
            };
        }

        public async Task<ServiceResult<ICollection<Fridge>>> DeleteFridgeByIdAsync(int fridgeId)
        {
            var fridge = await _fridgesRepository.Delete(fridgeId);

            return new ServiceResult<ICollection<Fridge>>
            {
                Success = fridge != null,
                Result = await _fridgesRepository.GetFridgesListAsync()
            };
        }

        public async Task<ServiceResult<ICollection<Fridge>>> CreateFridgeAsync(Fridge fridge)
        {
            fridge = await _fridgesRepository.Add(fridge);

            return new ServiceResult<ICollection<Fridge>>
            {
                Success = fridge != null,
                Result = await _fridgesRepository.GetFridgesListAsync()
            };
        }
    }
}
