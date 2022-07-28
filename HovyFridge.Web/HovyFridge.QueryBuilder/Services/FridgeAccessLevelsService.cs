using FluentResults;
using HovyFridge.Entity;
using HovyFridge.QueryBuilder.QueryBuilders;
using HovyFridge.Services;
using Microsoft.Extensions.DependencyInjection;

namespace HovyFridge.QueryBuilder.Services
{
    public class FridgeAccessLevelsService : IFridgeAccessLevelsService
    {
        private readonly ApplicationContext _context;

        private FridgesQueryBuilder _fridgesQueryBuilder
        {
            get => new FridgesQueryBuilder(_context);
        }

        private FridgeAccessLevelsQueryBuilder _fridgeAccessLevelsQueryBuilder
        {
            get => new FridgeAccessLevelsQueryBuilder(_context);
        }

        public FridgeAccessLevelsService(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Result<List<FridgeAccessLevel>>> GetByFridgeIdAsync(long fridgeId)
        {
            try
            {
                var fridge = await _fridgesQueryBuilder.WithId(fridgeId).SingleAsync();

                if (fridge == null)
                    throw new Exception($"Fridge with id {fridgeId} is not found!");

                var accessLevels = await _fridgeAccessLevelsQueryBuilder.WithFridgeId(fridgeId).ToListAsync();

                return Result.Ok(accessLevels);

            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }

        public async Task<Result<FridgeAccessLevel>> AddAsync(long fridgeId, FridgeAccessLevel newAccessLevel)
        {
            try
            {
                var accessLevel = await _fridgeAccessLevelsQueryBuilder.WithFridgeId(fridgeId).ToListAsync();

                if (accessLevel.Count > 0)
                    throw new Exception($"Fridge access level with id {accessLevel} already exists for fridge with id {fridgeId}!");

                await _fridgeAccessLevelsQueryBuilder.AddAsync(newAccessLevel);

                return Result.Ok(newAccessLevel);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Result<FridgeAccessLevel>> UpdateAsync(long fridgeId, FridgeAccessLevel updatedAccessLevel)
        {
            try
            {
                var accessLevel = await _fridgeAccessLevelsQueryBuilder.WithFridgeId(fridgeId).ToListAsync();

                if (accessLevel.Count > 0)
                    throw new Exception($"Fridge access level with id {accessLevel} already exists for fridge with id {fridgeId}!");

                await _fridgeAccessLevelsQueryBuilder.UpdateAsync(updatedAccessLevel);

                return Result.Ok(updatedAccessLevel);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Result<FridgeAccessLevel>> DeleteByIdAsync(long accessLevelId)
        {
            try
            {
                var accessLevel = await _fridgeAccessLevelsQueryBuilder.WithId(accessLevelId).SingleAsync();

                if (accessLevel == null)
                    throw new Exception($"Fridge access level with id {accessLevelId} is not found!");

                await _fridgeAccessLevelsQueryBuilder.DeleteAsync(accessLevel);

                return Result.Ok(accessLevel);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}