using HovyFridge.GenericRepository.Services;
using NUnit.Framework;
using System.Threading.Tasks;
using HovyFridge.GenericRepository.Repository;

namespace HovyFridge.Tests.ServicesTests.Products
{
    internal class GenericRepositoryFridgeServiceTests : ProductsServiceTests
    {
        [SetUp]
        public void SetUp()
        {
            SetupDatabase("GenericRepositoryFridgeServiceTests");
            ProductsService = new ProductsService(new ProductsRepository(Context));
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
