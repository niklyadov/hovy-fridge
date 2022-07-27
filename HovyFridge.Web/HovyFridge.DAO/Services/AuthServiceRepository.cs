using FluentResults;
using HovyFridge.Entity;
using HovyFridge.Services;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;

namespace HovyFridge.DAO.Services;

public class AuthServiceRepository : IAuthService
{
    private readonly ApplicationContext _context;

    private int? _userId = null;
    public AuthServiceRepository(ApplicationContext context)
    {
        _context = context;
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

            var user = await _context.Users
                .Where(u => u.Id == _userId.Value)
                .FirstOrDefaultAsync();

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