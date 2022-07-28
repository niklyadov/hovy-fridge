using NUnit.Framework;
using HovyFridge.Services;
using HovyFridge.Entity;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace HovyFridge.Tests.ServicesTests.Fridges
{
    internal class FridgesServiceTests : ServiceTests
    {
        protected IFridgesService FridgesService;
        protected IProductsService ProductsService;

        public virtual async Task FridgeCreateTest()
        {
            ReCreateDatabase();

            var testFridge = new Fridge()
            {
                Name = "Test fridge FridgeCreateTest",
                Description = "Description of the Test Fridge",
                IsDeleted = false
            };

            var addFridgeResultValue = await AddFridge(testFridge);

            var addedFridge = Context.Fridges.Where(f => f.Id == addFridgeResultValue.Id).Single();

            Assert.AreEqual(addedFridge.Name, addFridgeResultValue.Name);
            Assert.AreEqual(addedFridge.Name, testFridge.Name);
        }

        public virtual async Task GetFridgeByIdTest()
        {
            ReCreateDatabase();

            var testFridge = new Fridge()
            {
                Name = "Test fridge GetFridgeByIdTest",
                Description = "Description of the Test Fridge",
                IsDeleted = false
            };

            var addFridgeResultValue = await AddFridge(testFridge);

            var testProduct = new Product()
            {
                Name = "Test Product For test GetFridgeByIdTest"
            };

            var addedTestProduct = (await ProductsService.AddAsync(testProduct)).ValueOrDefault;
            await FridgesService.PutProductAsync(addFridgeResultValue.Id, addedTestProduct.Id);

            var fridgeById = (await FridgesService.GetByIdAsync(addFridgeResultValue.Id)).ValueOrDefault;

            Assert.AreEqual(fridgeById.Id, addFridgeResultValue.Id);
            Assert.AreEqual(fridgeById.Products[0].Name, testProduct.Name);
        }

        public virtual async Task FridgeUpdateTest()
        {
            ReCreateDatabase();

            var testFridge = new Fridge()
            {
                Name = "Test fridge FridgeUpdateTest",
                Description = "Description of the Test Fridge",
                IsDeleted = false
            };

            var addFridgeResultValue = await AddFridge(testFridge);

            testFridge.Name = "Test fridge FridgeCreateTest Updated";

            var updatedFridgeResult = await UpdateFridge(testFridge);

            var updatedFridgeActualInDatabase = Context.Fridges.Where(f => f.Id == updatedFridgeResult.Id).Single();

            Assert.AreEqual(updatedFridgeActualInDatabase.Id, addFridgeResultValue.Id);
            Assert.AreEqual(updatedFridgeActualInDatabase.Name, testFridge.Name);
        }

        public virtual async Task FridgeDeleteTest()
        {
            ReCreateDatabase();

            var testFridge = new Fridge()
            {
                Name = "Test fridge FridgeDeleteTest",
                Description = "Description of the Test Fridge",
                IsDeleted = false
            };
            
            var addFridgeResultValue = await AddFridge(testFridge);
            var deletedFridgeResult = await DeleteFridge(addFridgeResultValue);

            var deletedFridgeActualInDatabase = Context.Fridges.Where(f => f.Id == deletedFridgeResult.Id).Single();

            Assert.AreEqual(deletedFridgeActualInDatabase.Id, addFridgeResultValue.Id);
            Assert.IsTrue(deletedFridgeActualInDatabase.IsDeleted);
            Assert.LessOrEqual(deletedFridgeActualInDatabase.DeletedDateTime, DateTime.UtcNow);
        }

        public virtual async Task FridgeRestoreTest()
        {
            ReCreateDatabase();

            var testFridge = new Fridge()
            {
                Name = "Test fridge FridgeRestoreTest",
                Description = "Description of the Test Fridge",
                IsDeleted = false
            };

            var addFridgeResultValue = await AddFridge(testFridge);

            var deletedFridgeResult = await DeleteFridge(addFridgeResultValue);
            var restoredFridgeResult = await RestoreFridge(deletedFridgeResult);

            var restoredFridgeActualInDatabase = Context.Fridges
                .Where(f => f.Id == restoredFridgeResult.Id)
                .Single();

            Assert.AreEqual(restoredFridgeActualInDatabase.Id, restoredFridgeResult.Id);
            Assert.IsFalse(restoredFridgeActualInDatabase.IsDeleted);
            Assert.IsNull(restoredFridgeActualInDatabase.DeletedDateTime);
        }

        public virtual async Task PutProductIntoFridgeTest()
        {
            ReCreateDatabase();

            var testFridge = new Fridge()
            {
                Name = "Test fridge PutProductIntoFridgeTest",
                Description = "Description of the Test Fridge",
                IsDeleted = false
            };

            var addFridgeResultValue = await AddFridge(testFridge);

            var testProduct = new Product()
            {
                Name = "Test Product For test PutProductIntoFridgeTest"
            };

            var addedTestProduct = await ProductsService.AddAsync(testProduct);
            Assert.IsTrue(addedTestProduct.IsSuccess);

            var addedTestProductValue = addedTestProduct.ValueOrDefault;

            var putProductResult = await FridgesService
                .PutProductAsync(addFridgeResultValue.Id, addedTestProductValue.Id);
            Assert.IsTrue(putProductResult.IsSuccess);

            var fridgeById = await FridgesService.GetByIdAsync(addFridgeResultValue.Id);
            Assert.IsTrue(fridgeById.IsSuccess);
            var fridgeByIdValue = fridgeById.ValueOrDefault;

            Assert.AreEqual(fridgeByIdValue.Id, addFridgeResultValue.Id);
            Assert.AreEqual(fridgeByIdValue.Products[0].Name, testProduct.Name);
        }

        public virtual async Task RemoveProductFromFridgeTest()
        {
            ReCreateDatabase();

            var testFridge = new Fridge()
            {
                Name = "Test fridge PutProductIntoFridgeTest",
                Description = "Description of the Test Fridge",
                IsDeleted = false
            };

            var addFridgeResultValue = await AddFridge(testFridge);

            var testProduct = new Product()
            {
                Name = "Test Product For test PutProductIntoFridgeTest"
            };

            var addedTestProduct = await ProductsService.AddAsync(testProduct);
            Assert.IsTrue(addedTestProduct.IsSuccess);

            var addedTestProductValue = addedTestProduct.ValueOrDefault;

            var putProductResult = await FridgesService
                .PutProductAsync(addFridgeResultValue.Id, addedTestProductValue.Id);
            Assert.IsTrue(putProductResult.IsSuccess);

            var removeProductResult = await FridgesService
                .RemoveProductAsync(addFridgeResultValue.Id, addedTestProductValue.Id);
            Assert.IsTrue(removeProductResult.IsSuccess);

            var restoreProductResult = await FridgesService
                .RestoreProductAsync(addFridgeResultValue.Id, addedTestProductValue.Id);
            Assert.IsTrue(restoreProductResult.IsSuccess);

            var fridgeById = await FridgesService.GetByIdAsync(addFridgeResultValue.Id);
            Assert.IsTrue(fridgeById.IsSuccess);

            var fridgeByIdValue = fridgeById.ValueOrDefault;

            Assert.AreEqual(fridgeByIdValue.Id, addFridgeResultValue.Id);
            Assert.AreEqual(fridgeByIdValue.Products[0].Name, testProduct.Name);
        }

        public virtual async Task RestoreProductIntoFridgeTest()
        {
            ReCreateDatabase();

            var testFridge = new Fridge()
            {
                Name = "Test fridge PutProductIntoFridgeTest",
                Description = "Description of the Test Fridge",
                IsDeleted = false
            };

            var addFridgeResultValue = await AddFridge(testFridge);

            var testProduct = new Product()
            {
                Name = "Test Product For test PutProductIntoFridgeTest"
            };

            var addedTestProduct = await ProductsService.AddAsync(testProduct);
            Assert.IsTrue(addedTestProduct.IsSuccess);

            var addedTestProductValue = addedTestProduct.ValueOrDefault;

            var putProductResult = await FridgesService
                .PutProductAsync(addFridgeResultValue.Id, addedTestProductValue.Id);
            Assert.IsTrue(putProductResult.IsSuccess);

            var removeProductResult = await FridgesService
                .RemoveProductAsync(addFridgeResultValue.Id, addedTestProductValue.Id);
            Assert.IsTrue(removeProductResult.IsSuccess);

            var fridgeById = await FridgesService.GetByIdAsync(addFridgeResultValue.Id);
            Assert.IsTrue(fridgeById.IsSuccess);

            var fridgeByIdValue = fridgeById.ValueOrDefault;

            Assert.AreEqual(fridgeByIdValue.Id, addFridgeResultValue.Id);
        }

        private async Task<Fridge> AddFridge(Fridge testFridge)
        {
            var addFridgeResult = await FridgesService.AddAsync(testFridge);
            
            Assert.IsTrue(addFridgeResult.IsSuccess);

            return addFridgeResult.ValueOrDefault;
        }

        private async Task<Fridge> UpdateFridge(Fridge testFridge)
        {
            var updateFridgeResult = await FridgesService.UpdateAsync(testFridge);

            Assert.IsTrue(updateFridgeResult.IsSuccess);

            return updateFridgeResult.ValueOrDefault;
        }

        private async Task<Fridge> DeleteFridge(Fridge testFridge)
        {
            var deleteFridgeResult = await FridgesService.DeleteByIdAsync(testFridge.Id);

            Assert.IsTrue(deleteFridgeResult.IsSuccess);

            return deleteFridgeResult.ValueOrDefault;
        }

        private async Task<Fridge> RestoreFridge(Fridge testFridge)
        {
            var restoredFridgeResult = await FridgesService.RestoreByIdAsync(testFridge.Id);

            Assert.IsTrue(restoredFridgeResult.IsSuccess);

            return restoredFridgeResult.ValueOrDefault;
        }
    }
}
