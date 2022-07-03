using HovyFridge.Api.Data.Entity;
using HovyFridge.Api.Data.Repository.GenericRepositoryPattern.Abstract;
using Microsoft.EntityFrameworkCore;

namespace HovyFridge.Api.Data.Repository.GenericRepositoryPattern
{
    public class FridgeAccessLevelsRepository : BaseRepository<FridgeAccessLevel, ApplicationContext>
    {
        private readonly ApplicationContext _dbContext;
        public FridgeAccessLevelsRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public List<User> GetWhichUsersHasAccessToFridgeWithId(long fridgeId)
        {
            return _dbContext.FridgeAccessLevels
                .Where(fal => fal.FridgeId == fridgeId)
                .Join(_dbContext.Users,
                        fal => fal.UserId, u => u.Id,
                        (fal, u) => u).ToList();
        }

        public FridgeAccessLevel? GetFridgeAccessLevelByUserId(long userId)
        {
            var accessLevel = _dbContext.FridgeAccessLevels
                .Where(a => a.UserId == userId)
                .FirstOrDefault();

            if (accessLevel != null)
                return accessLevel;

            return null;
        }

        public async Task<List<FridgeAccessLevel>> GetByFridgeIdAsync(long fridgeId)
        {
            var accessLevel = await _dbContext.FridgeAccessLevels
                .Where(a => a.FridgeId == fridgeId)
                .ToListAsync();

            if (accessLevel.Count > 0)
                return accessLevel;

            return new List<FridgeAccessLevel>();
        }
    }
}