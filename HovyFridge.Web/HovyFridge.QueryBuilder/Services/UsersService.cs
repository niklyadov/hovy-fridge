using FluentResults;
using HovyFridge.Entity;
using HovyFridge.QueryBuilder.QueryBuilders;
using HovyFridge.Services;

namespace HovyFridge.QueryBuilder.Services
{
    public class UsersService : IUsersService
    {
        private readonly ApplicationContext _context;

        private UsersQueryBuilder _usersQueryBuilder
        {
            get => new UsersQueryBuilder(_context);
        }

        public UsersService(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Result<List<User>>> GetAllAsync()
        {
            try
            {
                var usersList = await _usersQueryBuilder
                    .WhereNotDeleted()
                    .ToListAsync();

                return Result.Ok(usersList);
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
                var user = await _usersQueryBuilder
                    .WhereNotDeleted()
                    .WithId(id)
                    .FirstOrDefaultAsync();

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
                var createdUser = await _usersQueryBuilder.AddAsync(user);

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
                var createdUser = await _usersQueryBuilder.UpdateAsync(user);

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
                var user = await _usersQueryBuilder
                    .WhereNotDeleted()
                    .WithId(id)
                    .FirstOrDefaultAsync();

                if (user == null)
                    throw new Exception("User is not found!");

                var deletedUser = await _usersQueryBuilder.DeleteAsync(user);

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
                var user = await _usersQueryBuilder
                    .WhereDeleted()
                    .WithId(id)
                    .FirstOrDefaultAsync();

                if (user == null)
                    throw new Exception("User is not found!");

                var restoredUser = await _usersQueryBuilder.UndoDeleteAsync(user);

                return Result.Ok();
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }
    }
}
