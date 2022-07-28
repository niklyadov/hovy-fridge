using HovyFridge.QueryBuilder.QueryBuilders;
using HovyFridge.QueryBuilder.Services;
using NUnit.Framework;
using System.Threading.Tasks;

namespace HovyFridge.Tests.ServicesTests.Products
{
    internal class QueryBuilderProductsServiceTests : ProductsServiceTests
    {
        [SetUp]
        public void SetUp()
        {
            SetupDatabase("QueryBuilderFridgeServiceTests");

            ProductsService = new ProductsService(Context);
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
    }
}
