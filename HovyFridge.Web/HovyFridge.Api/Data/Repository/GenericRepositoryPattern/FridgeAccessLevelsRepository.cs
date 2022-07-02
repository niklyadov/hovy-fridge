using HovyFridge.Api.Data.Entity;
using HovyFridge.Api.Data.Repository.GenericRepositoryPattern.Abstract;

namespace HovyFridge.Api.Data.Repository.GenericRepositoryPattern
{
    public class FridgeAccessLevelsRepository : BaseRepository<FridgeAccessLevel, ApplicationContext>
    {
        private readonly ApplicationContext _dbContext;
        public FridgeAccessLevelsRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
