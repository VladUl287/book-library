using OneOf;
using DataAccess;
using Domain.Dtos;
using Domain.Errors;
using DataAccess.Models;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using BookLibraryApi.Services.Contracts;
using BookLibraryApi.Configuration;
using BookLibraryApi.Helpers;

namespace BookLibraryApi.Services;

public class AuthService : IAuthService
{
    private readonly DatabaseContext dbContext;
    private readonly Config config;

    public AuthService(DatabaseContext dbContext, Config config)
    {
        this.dbContext = dbContext;
        this.config = config;
    }

    public async Task<OneOf<AuthSuccess, Error>> Login(AuthModel authModel)
    {
        var user = await dbContext.Users
            .Include(e => e.Role)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Email == authModel.Email);

        if (user is null)
        {
            return Errors.LoginFaild;
        }

        var hashPassword = HashHelper.GetHash(authModel.Password, config.HashSecret);

        if (user.Password != hashPassword)
        {
            return Errors.LoginFaild;
        }

        var lifeTime = int.Parse(config.LifeTime);

        var accessToken = JwtHelper.Generate(user, config.AccessSecret, config.Issuer, config.Audience, DateTime.UtcNow.AddHours(lifeTime));
        var refreshToken = JwtHelper.Generate(user, config.RefreshSecret, config.Issuer, config.Audience, DateTime.UtcNow.AddDays(lifeTime));

        await dbContext.UsersTokens.AddAsync(
            new UserToken
            {
                UserId = user.Id,
                RefreshToken = refreshToken
            });
        await dbContext.SaveChangesAsync();

        return new AuthSuccess
        {
            Email = user.Email,
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };
    }

    public async Task<OneOf<AuthSuccess, Error>> Register(AuthModel authModel)
    {
        var exists = await dbContext.Users.AnyAsync(e => e.Email == authModel.Email);

        if (exists)
        {
            return Errors.UserWithEmailAlreadyExists;
        }

        var hashPassword = HashHelper.GetHash(authModel.Password, config.HashSecret);

        var user = new User
        {
            Email = authModel.Email,
            Password = hashPassword
        };

        await dbContext.Users.AddAsync(user);
        await dbContext.SaveChangesAsync();

        return new AuthSuccess
        {
            Email = user.Email
        };
    }

    public async Task<OneOf<AuthSuccess, Error>> Refresh(string token)
    {
        if (string.IsNullOrEmpty(token))
        {
            return Errors.TokenInvalid;
        }

        var handler = new JwtSecurityTokenHandler();
        var jwtSecurityToken = handler.ReadJwtToken(token);
        var claimValue = jwtSecurityToken.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier).Value;

        if (!Guid.TryParse(claimValue, out Guid userId))
        {
            return Errors.TokenInvalid;
        }

        var dbToken = await dbContext.UsersTokens
           .FirstOrDefaultAsync(x => x.UserId == userId && x.RefreshToken == token);

        if (dbToken is null)
        {
            return Errors.TokenInvalid;
        }

        var issuer = config.Issuer;
        var audience = config.Audience;
        var accessTokenKey = config.AccessSecret;
        var refreshTokenKey = config.RefreshSecret;
        var lifeTime = int.Parse(config.LifeTime);

        var valid = JwtHelper.ValidateToken(dbToken.RefreshToken, refreshTokenKey, issuer, audience);

        if (!valid)
        {
            dbContext.UsersTokens.Remove(dbToken);
            await dbContext.SaveChangesAsync();

            return Errors.TokenInvalid;
        }

        var user = await dbContext.Users
            .Include(e => e.Role)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == dbToken.UserId);

        if (user is null)
        {
            return Errors.TokenInvalid;
        }

        var accessToken = JwtHelper.Generate(user, accessTokenKey, issuer, audience, DateTime.UtcNow.AddHours(lifeTime));
        var refreshToken = JwtHelper.Generate(user, refreshTokenKey, issuer, audience, DateTime.UtcNow.AddDays(lifeTime));

        dbToken.RefreshToken = refreshToken;
        await dbContext.SaveChangesAsync();

        return new AuthSuccess
        {
            Email = user.Email,
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };
    }

    public async Task Logout(Guid userId)
    {
        var userToken = await dbContext.UsersTokens
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.UserId == userId);

        if (userToken is null)
        {
            return;
        }

        dbContext.UsersTokens.Remove(userToken);
        await dbContext.SaveChangesAsync();
    }
}