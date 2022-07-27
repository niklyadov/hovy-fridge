using FluentResults;
using HovyFridge.Entity;
using HovyFridge.Services;
using Microsoft.EntityFrameworkCore;

namespace HovyFridge.DAO.Services
{
    public class UsersServiceRepository : IUsersService
    {
        private readonly ApplicationContext _applicationContext;
        public UsersServiceRepository(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public async Task<Result<List<User>>> GetAllAsync()
        {
            try
            {
                return Result.Ok(await _applicationContext.Users
                    .ToListAsync());
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
                var user = await _applicationContext.Users
                    .FirstOrDefaultAsync(u => u.Id == id);

                if (user == null)
                    throw new Exception("User is not found!");

                return Result.Ok(user);
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
                await _applicationContext.Users.AddAsync(user);
                _applicationContext.Entry(user).State = EntityState.Added;
                await _applicationContext.SaveChangesAsync();

                return Result.Ok(user);
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
                await _applicationContext.Users.AddAsync(user);
                _applicationContext.Entry(user).State = EntityState.Modified;
                await _applicationContext.SaveChangesAsync();

                return Result.Ok(user);
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
                var user = await _applicationContext.Users
                    .FirstOrDefaultAsync(u => u.Id == id);

                if (user == null)
                    throw new Exception("User is not found!");

                user.IsDeleted = true;
                user.DeletedDateTime = DateTime.UtcNow;

                return await UpdateAsync(user);
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
                var user = await _applicationContext.Users
                    .FirstOrDefaultAsync(u => u.Id == id);

                if (user == null)
                    throw new Exception("User is not found!");

                user.IsDeleted = false;
                user.DeletedDateTime = null;

                return await UpdateAsync(user);
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }
    }
}
