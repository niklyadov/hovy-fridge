using HovyFridge.DAO.Services;
using NUnit.Framework;
using System.Threading.Tasks;

namespace HovyFridge.Tests.ServicesTests.Fridges
{
    internal class DaoFridgeServiceTests : FridgesServiceTests
    {
        [SetUp]
        public void SetUp()
        {
            SetupDatabase("DaoFridgeServiceTests");
            FridgesService = new FridgesServiceRepository(Context);
            ProductsService = new ProductsServiceRepository(Context);
        }

        [TearDown]
        public void TearDown()
        {
            DropDatabase();
        }

        [Test]
        public override Task FridgeCreateTest()
        {
            return base.FridgeCreateTest();
        }

        [Test]
        public override Task GetFridgeByIdTest()
        {
            return base.GetFridgeByIdTest();
        }

        [Test]
        public override Task FridgeUpdateTest()
        {
            return base.FridgeUpdateTest();
        }

        [Test]

        public override Task FridgeDeleteTest()
        {
            return base.FridgeDeleteTest();
        }

        [Test]

        public override Task FridgeRestoreTest()
        {
            return base.FridgeRestoreTest();
        }

        [Test]
        public override Task PutProductIntoFridgeTest()
        {
            return base.PutProductIntoFridgeTest();
        }

        [Test]
        public override Task RemoveProductFromFridgeTest()
        {
            return base.RemoveProductFromFridgeTest();
        }

        [Test]
        public override Task RestoreProductIntoFridgeTest()
        {
            return base.RestoreProductIntoFridgeTest();
        }
    }
}
