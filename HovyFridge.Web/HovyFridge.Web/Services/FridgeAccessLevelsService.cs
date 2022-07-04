using FluentResults;
using HovyFridge.Data.Entity;
using HovyFridge.Data.Repository.GenericRepositoryPattern;

namespace HovyFridge.Web.Services
{
    public class FridgeAccessLevelsService
    {
        private FridgeAccessLevelsRepository _fridgeAccessLevelsRepository;
        private FridgesRepository _fridgesRepository;
        public FridgeAccessLevelsService(FridgeAccessLevelsRepository fridgeAccessLevelsRepository, FridgesRepository fridgesRepository)
        {
            _fridgeAccessLevelsRepository = fridgeAccessLevelsRepository;
            _fridgesRepository = fridgesRepository;
        }

        public async Task<Result<List<FridgeAccessLevel>>> GetByFridgeIdAsync(long fridgeId)
        {
            try
            {
                var fridge = await _fridgesRepository.GetById(fridgeId);

                if (fridge == null)
                    throw new Exception($"Fridge with id {fridgeId} is not found!");

                var accessLevels = await _fridgeAccessLevelsRepository.GetByFridgeIdAsync(fridgeId);

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
                var accessLevel = await _fridgeAccessLevelsRepository.GetByFridgeIdAsync(fridgeId);

                if (accessLevel.Count > 0)
                    throw new Exception($"Fridge access level with id {accessLevel} already exists for fridge with id {fridgeId}!");

                var addedAccessLevelResult = await _fridgeAccessLevelsRepository.Add(newAccessLevel);

                return Result.Ok(addedAccessLevelResult);
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
                var accessLevel = await _fridgeAccessLevelsRepository.GetByFridgeIdAsync(fridgeId);

                if (accessLevel.Count > 0)
                    throw new Exception($"Fridge access level with id {accessLevel} already exists for fridge with id {fridgeId}!");

                var updatedAccessLevelResult = await _fridgeAccessLevelsRepository.Update(updatedAccessLevel);

                return Result.Ok(updatedAccessLevelResult);
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
                var accessLevel = await _fridgeAccessLevelsRepository.GetById(accessLevelId);

                if (accessLevel == null)
                    throw new Exception($"Fridge access level with id {accessLevelId} is not found!");

                var addedAccessLevelResult = await _fridgeAccessLevelsRepository.Delete(accessLevel);

                return Result.Ok(addedAccessLevelResult);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}