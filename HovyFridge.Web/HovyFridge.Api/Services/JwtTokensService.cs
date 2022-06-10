namespace HovyFridge.Api.Services;

using HovyFridge.Api.Data.Entity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

public class JwtTokensService
{
    private SymmetricSecurityKey GetSymmetricSecurityKey()
    {
        return new SymmetricSecurityKey(Encoding.ASCII.GetBytes("B?E(H+MbQeThWmZq"));
    }

    public string GenerateNewTokenForUser(User user, string audience = "default")
    {
        var generationDate = DateTime.UtcNow;
        var jwt = new JwtSecurityToken(
                issuer: user.Id.ToString(),
                audience: audience,
                notBefore: generationDate,
                expires: generationDate.Add(TimeSpan.FromMinutes(10)),
                signingCredentials: new SigningCredentials(GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }

    public async Task<bool> IsValidTokenAsync(User user, string jwtBase64, string audience = "default")
    {
        var validationResult = await new JwtSecurityTokenHandler().ValidateTokenAsync(jwtBase64,
            new TokenValidationParameters()
            {
                ValidIssuer = user.Id.ToString(),
                ValidateIssuer = true,

                IssuerSigningKey = GetSymmetricSecurityKey(),
                ValidateIssuerSigningKey = true,

                ValidateLifetime = true,

                ValidAudience = audience,
                ValidateAudience = true,
            }
         );

        return validationResult.IsValid;
    }

    public JwtSecurityToken ParseToken(string jwtBase64)
    {
        return new JwtSecurityToken(jwtBase64);
    }
}