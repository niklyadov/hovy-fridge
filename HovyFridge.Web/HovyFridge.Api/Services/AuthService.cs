using FluentResults;
using HovyFridge.Data.Entity;
using HovyFridge.Data.Repository.GenericRepositoryPattern;
using System.IdentityModel.Tokens.Jwt;

namespace HovyFridge.Api.Services;

public class AuthService
{
    private int? _userId = null;

    private readonly UsersRepository _usersRepostiory;

    public AuthService(UsersRepository usersRepostiory)
    {
        _usersRepostiory = usersRepostiory;
    }
    public Result InitInstanceWithToken(JwtSecurityToken token)
    {
        try
        {
            if (!int.TryParse(token.Issuer, out int userId))
                throw new Exception("The token is not attached on any user.");

            _userId = userId;

            return Result.Ok();
        }
        catch (Exception ex)
        {
            return Result.Fail(ex.Message);
        }
    }

    public async Task<Result<User>> GetCurrentUser()
    {
        try
        {
            if (!_userId.HasValue || _userId.Value == 0)
                throw new Exception("User id is not assigned!");

            var user = await _usersRepostiory.GetById(_userId.Value);

            if (user == null)
                throw new Exception("User is not found!");

            return Result.Ok(user);
        }
        catch (Exception ex)
        {
            return Result.Fail(ex.Message);
        }
    }
}