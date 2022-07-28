using Microsoft.EntityFrameworkCore;

namespace HovyFridge.Tests.ServicesTests
{
    internal class ServiceTests
    {
        protected ApplicationContext Context;
        private string _databaseName = "TEST_DB";

        protected void SetupDatabase(string databaseName)
        {
            _databaseName = databaseName;

            var dbContextOptions =
                new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(databaseName).Options;

            Context = new ApplicationContext(dbContextOptions);
            Context.Database.EnsureCreated();
        }
        protected void DropDatabase()
        {
            Context.Database.EnsureDeleted();
        }

        protected void ReCreateDatabase()
        {
            DropDatabase();
            SetupDatabase(_databaseName);
        }
    }
}
