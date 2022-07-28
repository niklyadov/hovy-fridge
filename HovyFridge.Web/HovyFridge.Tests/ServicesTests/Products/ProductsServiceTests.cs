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
        }

        public virtual async Task ProductUpdateTest() 
        {
        }

        public virtual async Task ProductDeleteTest() 
        {
        }

        public virtual async Task ProductRestoreTest() 
        {
        }


        public virtual async Task GetGroupedByFridgeIdTest() 
        {
        }

        public virtual async Task GetSuggestionsNamesTest() 
        {
        }
    }
}
