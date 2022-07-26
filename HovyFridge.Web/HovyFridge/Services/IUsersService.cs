using FluentResults;
using HovyFridge.Entity;

namespace HovyFridge.Services
{
    public interface IUsersService
    {
        Task<Result<User>> AddAsync(User user);
        Task<Result<User>> DeleteByIdAsync(long id);
        Task<Result<List<User>>> GetAllAsync();
        Task<Result<User>> GetByIdAsync(long id);
        Task<Result<User>> RestoreByIdAsync(long id);
        Task<Result<User>> UpdateAsync(User user);
    }
}