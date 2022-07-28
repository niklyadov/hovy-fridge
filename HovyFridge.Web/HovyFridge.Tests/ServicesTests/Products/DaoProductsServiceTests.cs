﻿using HovyFridge.DAO.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HovyFridge.Tests.ServicesTests.Products
{
    internal class DaoProductsServiceTests : ProductsServiceTests
    {
        [SetUp]
        public void SetUp()
        {
            SetupDatabase("DaoProductsServiceTests");
            ProductsService = new ProductsServiceRepository(Context);
        }

        [TearDown]
        public void TearDown()
        {
            DropDatabase();
        }

        [Test]
        public override Task ProductCreateTest()
        {
            return base.ProductCreateTest();
        }

        [Test]
        public override Task ProductGetByIdTest()
        {
            return base.ProductGetByIdTest();
        }

        [Test]
        public override Task ProductGetByBarcodeTest()
        {
            return base.ProductGetByBarcodeTest();
        }

        [Test]
        public override Task GetSuggestionsNamesTest()
        {
            return base.GetSuggestionsNamesTest();
        }

        [Test]
        public override Task GetGroupedByFridgeIdTest()
        {
            return base.GetGroupedByFridgeIdTest();
        }

        [Test]
        public override Task ProductUpdateTest()
        {
            return base.ProductUpdateTest();
        }

        [Test]
        public override Task ProductDeleteTest()
        {
            return base.ProductDeleteTest();
        }

        [Test]
        public override Task ProductRestoreTest()
        {
            return base.ProductRestoreTest();
        }
    }
}
