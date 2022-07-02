using FluentResults;
using HovyFridge.Api.Data.Entity;
using HovyFridge.Api.Data.Repository.GenericRepositoryPattern;

namespace HovyFridge.Api.Services
{
    public class UsersService
    {
        private UsersRepository _usersRepository;

        public UsersService(UsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public async Task<Result<List<User>>> GetAllAsync()
        {
            try
            {
                return Result.Ok(await _usersRepository.GetAll());
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }

        public async Task<Result<User>> GetByIdAsync(long id)
        {
            try
            {
                var user = await _usersRepository.GetById(id);

                if (user == null)
                    throw new Exception("User is not found!");

                return Result.Ok();
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }

        public async Task<Result<User>> AddAsync(User user)
        {
            try
            {
                var createdUser = await _usersRepository.Add(user);

                return Result.Ok();
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }

        public async Task<Result<User>> UpdateAsync(User user)
        {
            try
            {
                var createdUser = await _usersRepository.Update(user);

                return Result.Ok();
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }

        public async Task<Result<User>> DeleteByIdAsync(long id)
        {
            try
            {
                var createdUser = await _usersRepository.DeleteById(id);

                return Result.Ok();
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }

        public async Task<Result<User>> RestoreByIdAsync(long id)
        {
            try
            {
                var createdUser = await _usersRepository.DeleteById(id);

                return Result.Ok();
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }
    }
}
