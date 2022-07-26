using FluentResults;
using HovyFridge.Entity;
using System.IdentityModel.Tokens.Jwt;

namespace HovyFridge.Services
{
    public interface IAuthService
    {
        Task<Result<User>> GetCurrentUser();
        Result InitInstanceWithToken(JwtSecurityToken token);
    }
}