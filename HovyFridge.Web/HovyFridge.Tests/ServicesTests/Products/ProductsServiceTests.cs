using HovyFridge.Entity;
using HovyFridge.Services;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HovyFridge.Tests.ServicesTests.Products
{
    internal class ProductsServiceTests : ServiceTests
    {
        protected IProductsService ProductsService;

        public virtual async Task ProductCreateTest() 
        {
            var testProduct = new Product()
            {
                Name = "Test product for ProductCreateTest"
            };

            var addedProduct = (await ProductsService.AddAsync(testProduct)).ValueOrDefault;
            var addedProductActuallyInDB = await Context.Products
                .Where(p => p.Id == addedProduct.Id)
                .SingleAsync();

            Assert.AreEqual(addedProduct.Id, addedProductActuallyInDB.Id);
            Assert.AreEqual(testProduct.Name, addedProductActuallyInDB.Name);
        }

        public virtual async Task ProductGetByIdTest() 
        {
            var testProduct = new Product()
            {
                Name = "Test product for ProductCreateTest"
            };

            var addedProduct = (await ProductsService.AddAsync(testProduct)).ValueOrDefault;
            var productById = (await ProductsService.GetByIdAsync(addedProduct.Id)).ValueOrDefault;
            var addedProductActuallyInDB = await Context.Products
                .Where(p => p.Id == addedProduct.Id)
                .SingleAsync();

            Assert.AreEqual(addedProduct.Id, addedProductActuallyInDB.Id);
            Assert.AreEqual(addedProduct.Id, productById.Id);
            Assert.AreEqual(productById.Name, addedProductActuallyInDB.Name);
        }

        public virtual async Task ProductGetByBarcodeTest() 
        {
            var testProduct = new Product()
            {
                BarCode = "0942",
                Name = "Test product for ProductCreateTest"
            };

            var addedProduct = (await ProductsService.AddAsync(testProduct)).ValueOrDefault;
            var productById = (await ProductsService.GetByBarcodeAsync(addedProduct.BarCode)).ValueOrDefault;
            var addedProductActuallyInDB = await Context.Products
                .Where(p => p.Id == addedProduct.Id)
                .SingleAsync();

            Assert.AreEqual(addedProduct.Id, addedProductActuallyInDB.Id);
            Assert.AreEqual(addedProduct.Id, productById.Id);
            Assert.AreEqual(productById.BarCode, addedProductActuallyInDB.BarCode);
        }

        public virtual async Task ProductUpdateTest() 
        {
            var testProduct = new Product()
            {
                BarCode = "0942",
                Name = "Test product for ProductCreateTest"
            };

            var addedProduct = (await ProductsService.AddAsync(testProduct)).ValueOrDefault;
            var productById = (await ProductsService.GetByBarcodeAsync(addedProduct.BarCode)).ValueOrDefault;

            addedProduct.BarCode = "49392";

            var updatedProductResult = await ProductsService.UpdateAsync(addedProduct);

            var updatedProductActuallyInDB = await Context.Products
                .Where(p => p.Id == addedProduct.Id)
                .SingleAsync();

            Assert.AreEqual(addedProduct.Id, updatedProductActuallyInDB.Id);
            Assert.AreEqual(addedProduct.Id, productById.Id);
            Assert.AreEqual(addedProduct.BarCode, updatedProductActuallyInDB.BarCode);
        }

        public virtual async Task ProductDeleteTest()
        {
            var testProduct = new Product()
            {
                BarCode = "0942",
                Name = "Test product for ProductCreateTest"
            };

            var addedProduct = (await ProductsService.AddAsync(testProduct)).ValueOrDefault;
            var productById = (await ProductsService.GetByBarcodeAsync(addedProduct.BarCode)).ValueOrDefault;

            var daletedProductResult = await ProductsService.DeleteByIdAsync(addedProduct.Id);
            var deletedProduct = daletedProductResult.ValueOrDefault;

            var deletedProductActuallyInDB = await Context.Products
                .Where(p => p.Id == addedProduct.Id)
                .SingleAsync();

            Assert.AreEqual(addedProduct.Id, deletedProductActuallyInDB.Id);
            Assert.IsTrue(deletedProductActuallyInDB.IsDeleted);
            Assert.LessOrEqual(deletedProductActuallyInDB.DeletedDateTime, DateTime.UtcNow);
        }

        public virtual async Task ProductRestoreTest() 
        {
            var testProduct = new Product()
            {
                BarCode = "0942",
                Name = "Test product for ProductCreateTest"
            };

            var addedProduct = (await ProductsService.AddAsync(testProduct)).ValueOrDefault;
            var productById = (await ProductsService.GetByBarcodeAsync(addedProduct.BarCode)).ValueOrDefault;

            var restoredProductResult = await ProductsService.RestoreByIdAsync(addedProduct.Id);
            var restoredProduct = restoredProductResult.ValueOrDefault;

            var restoredProductActuallyInDB = await Context.Products
                .Where(p => p.Id == addedProduct.Id)
                .SingleAsync();

            Assert.AreEqual(addedProduct.Id, restoredProductActuallyInDB.Id);
            Assert.IsFalse(restoredProductActuallyInDB.IsDeleted);
            Assert.IsNull(restoredProductActuallyInDB.DeletedDateTime);
        }


        public virtual async Task GetGroupedByFridgeIdTest() 
        {
        }

        public virtual async Task GetSuggestionsNamesTest() 
        {
        }
    }
}
