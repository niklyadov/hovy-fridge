using FluentResults;
using HovyFridge.Api.Data;
using HovyFridge.Api.Data.Entity;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;

namespace HovyFridge.Api.Services;

public class AuthService
{
    private int? _userId = null;
    private readonly ApplicationContext _db;
    private readonly DbSet<User>? _users;
    private User _user;

    public AuthService(ApplicationContext applicationContext)
    {
        _db = applicationContext;
        _users = applicationContext.Users;
    }
    public Result InitInstanceWithToken(JwtSecurityToken token)
    {
        if (!int.TryParse(token.Issuer, out int userId))
        {
            return Result.Fail("The token is not attached on any user.");
        }

        _userId = userId;

        return Result.Ok();
    }

    public async Task<Result<User>> GetCurrentUser()
    {
        if (_userId.HasValue && _userId.Value > 0)
        {
            _user = await _users?.FirstOrDefaultAsync(u => u.Id == _userId.Value);

            if (_user is not null)
            {
                return Result.Ok(_user);
            }
        }

        return Result.Fail("User not found");
    }
}