using FluentResults;
using HovyFridge.Data.Entity;
using HovyFridge.Data.Repository.GenericRepositoryPattern;
using System.Security.Cryptography;
using System.Text;

namespace HovyFridge.Web.Services
{
    public class UsersService
    {
        private UsersRepository _usersRepository;

        public User? CurrentUser { get; internal set; }

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

        public async Task<Result<User>> RegisterAsync(string username, string password)
        {
            try
            {
                var createdUser = new User()
                {
                    Name = username,
                    PasswordHash = HashPassword(password)
                };

                createdUser = await _usersRepository.Add(createdUser);

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

        public async Task<Result<User>> GetByUsernameAsync(string username)
        {
            try
            {
                var createdUser = await _usersRepository.GetByUsername(username);

                return Result.Ok(createdUser);
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

        public bool IsPasswordValid(string password, string passwordHash) =>
            HashPassword(password).Equals(passwordHash);

        private string HashPassword(string unhashedDataStr)
        {
            using var sha256 = SHA256.Create();
            var unhashedData = Encoding.UTF8.GetBytes(unhashedDataStr);
            var hashedData = sha256.ComputeHash(unhashedData);
            var hashedStr = Encoding.UTF8.GetString(hashedData);

            return hashedStr;
        }
    }
}
