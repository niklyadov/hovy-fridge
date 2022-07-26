using FluentResults;
using HovyFridge.Entity;

namespace HovyFridge.Services
{
    public interface IFridgeAccessLevelsService
    {
        Task<Result<FridgeAccessLevel>> AddAsync(long fridgeId, FridgeAccessLevel newAccessLevel);
        Task<Result<FridgeAccessLevel>> DeleteByIdAsync(long accessLevelId);
        Task<Result<List<FridgeAccessLevel>>> GetByFridgeIdAsync(long fridgeId);
        Task<Result<FridgeAccessLevel>> UpdateAsync(long fridgeId, FridgeAccessLevel updatedAccessLevel);
    }
}