using FluentResults;
using HovyFridge.Entity;
using HovyFridge.Services;
using Microsoft.EntityFrameworkCore;

namespace HovyFridge.DAO.Services
{
    public class FridgeAccessLevelsServiceRepository : IFridgeAccessLevelsService
    {
        private readonly ApplicationContext _context;
        public FridgeAccessLevelsServiceRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Result<List<FridgeAccessLevel>>> GetByFridgeIdAsync(long fridgeId)
        {
            try
            {
                var fridge = await _context.Fridges
                    .Where(f => f.Id == fridgeId)
                    .FirstOrDefaultAsync();

                if (fridge == null)
                    throw new Exception($"Fridge with id {fridgeId} is not found!");

                var accessLevels = await _context.FridgeAccessLevels
                    .Where(fal => fal.FridgeId == fridgeId)
                    .ToListAsync();

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
                var accessLevels = await _context.FridgeAccessLevels
                    .Where(fal => fal.FridgeId == fridgeId)
                    .ToListAsync();

                if (accessLevels.Count > 0)
                    throw new Exception($"Fridge access levels with id {accessLevels} already exists for fridge with id {fridgeId}!");

                await _context.FridgeAccessLevels.AddAsync(newAccessLevel);
                _context.Entry(accessLevels).State = EntityState.Added;
                await _context.SaveChangesAsync();

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
                var accessLevels = await _context.FridgeAccessLevels
                    .Where(fal => fal.FridgeId == fridgeId)
                    .ToListAsync();

                if (accessLevels.Count > 0)
                    throw new Exception($"Fridge access level with id {accessLevels} already exists for fridge with id {fridgeId}!");

                _context.FridgeAccessLevels.Update(updatedAccessLevel);
                _context.Entry(updatedAccessLevel).State = EntityState.Modified;
                await _context.SaveChangesAsync();

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
                var accessLevel = await _context.FridgeAccessLevels
                    .Where(fal => fal.Id == accessLevelId)
                    .FirstOrDefaultAsync();

                if (accessLevel == null)
                    throw new Exception($"Fridge access level with id {accessLevelId} is not found!");


                accessLevel.IsDeleted = true;
                accessLevel.DeletedDateTime = DateTime.UtcNow;

                _context.FridgeAccessLevels.Update(accessLevel);
                _context.Entry(accessLevel).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return Result.Ok(accessLevel);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}