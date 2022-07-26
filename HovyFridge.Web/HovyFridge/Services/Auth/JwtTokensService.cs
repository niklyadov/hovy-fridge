namespace HovyFridge.Services.Auth;

using HovyFridge.Entity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;


public class JwtTokensService
{
    private readonly JwtTokensServiceConfiguration _configuration = new JwtTokensServiceConfiguration();
    private SymmetricSecurityKey GetSymmetricSecurityKey()
    {
        return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration.JwtSecret));
    }

    public string GenerateNewTokenForUser(User user, string audience = "default", DateTime? expires = null)
    {
        var generationDate = DateTime.UtcNow;
        var jwt = new JwtSecurityToken(
                issuer: user.Id.ToString(),
                audience: audience,
                notBefore: generationDate,
                expires: expires.HasValue ? expires.Value : generationDate.Add(_configuration.JwtDefaultLifetime),
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

    public JwtSecurityToken ParseToken(string jwtBase64) =>
        new JwtSecurityToken(jwtBase64);
}