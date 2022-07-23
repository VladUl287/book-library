using System.Text;
using DataAccess.Models;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace BookLibraryApi.Helpers;

public static class JwtHelper
{
    public static string Generate(User user, string key, string issuer, string audience, DateTime expires)
    {
        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var credentialsAccess = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
        var claims = new Claim[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role.Name)
        };

        var securityToken = new JwtSecurityToken(issuer, audience, claims, expires: expires, signingCredentials: credentialsAccess);

        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }

    public static bool ValidateToken(string token, string key, string issuer, string audience)
    {
        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var tokenHandler = new JwtSecurityTokenHandler();
        try
        {
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = issuer,
                ValidateAudience = true,
                ValidAudience = audience,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = symmetricSecurityKey
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;

            return true;
        }
        catch
        {
            return false;
        }
    }
}
