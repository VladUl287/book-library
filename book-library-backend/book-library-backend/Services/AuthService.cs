using OneOf;
using DataAccess;
using Common.Dtos;
using Common.Errors;
using DataAccess.Models;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using BookLibraryApi.Helpers;
using BookLibraryApi.Services.Contracts;
using BookLibraryApi.Configuration;

namespace BookLibraryApi.Services;

public class AuthService : IAuthService
{
    private readonly DatabaseContext dbContext;
    private readonly Config config;

    public AuthService(DatabaseContext dbContext, IOptions<Config> config)
    {
        this.dbContext = dbContext;
        this.config = config.Value;
    }

    public async Task<OneOf<AuthSuccess, Error>> Login(AuthModel authModel)
    {
        var user = await dbContext.Users
            .Include(e => e.Role)
            .FirstOrDefaultAsync(x => x.Email == authModel.Email);

        if (user is null)
        {
            return Errors.LoginFaild;
        }

        var hashSecret = config.HashSecret;
        var hashPassword = HashHelper.Hash(authModel.Password, hashSecret);

        if (user.Password != hashPassword)
        {
            return Errors.LoginFaild;
        }

        var issuer = config.Issuer;
        var audience = config.Audience;
        var accessTokenKey = config.AccessSecret;
        var refreshTokenKey = config.RefreshSecret;
        var lifeTime = int.Parse(config.LifeTime);

        var accessToken = JwtHelper.Generate(user, accessTokenKey, issuer, audience, DateTime.UtcNow.AddMinutes(lifeTime));
        var refreshToken = JwtHelper.Generate(user, refreshTokenKey, issuer, audience, DateTime.UtcNow.AddDays(lifeTime));

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
        var role = await dbContext.Roles.FirstOrDefaultAsync();

        if (exists)
        {
            return Errors.LoginFaild;
        }

        var hashSecret = config.HashSecret;
        var hashPassword = HashHelper.Hash(authModel.Password, hashSecret);

        var user = new User
        {
            Email = authModel.Email,
            Password = hashPassword,
            RoleId = role.Id
        };

        await dbContext.Users.AddAsync(user);
        await dbContext.SaveChangesAsync();

        return new AuthSuccess
        {
            Email = user.Email
        };
    }

    public async Task<OneOf<AuthSuccess, Error>> Refresh(Guid userId, string token)
    {
        if (string.IsNullOrEmpty(token))
        {
            return Errors.LoginFaild;
        }

        var dbToken = await dbContext.UsersTokens
           .FirstOrDefaultAsync(x => x.UserId == userId && x.RefreshToken == token);

        if (dbToken is null)
        {
            return Errors.LoginFaild;
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

            return Errors.LoginFaild;
        }

        var user = await dbContext.Users
            .Include(e => e.Role)
            .FirstOrDefaultAsync(x => x.Id == dbToken.UserId);

        var accessToken = JwtHelper.Generate(user, accessTokenKey, issuer, audience, DateTime.UtcNow.AddMinutes(lifeTime));
        var refreshToken = JwtHelper.Generate(user, refreshTokenKey, issuer, audience, DateTime.UtcNow.AddDays(lifeTime));

        dbToken.RefreshToken = refreshToken;
        dbContext.UsersTokens.Update(dbToken);
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
            .FirstOrDefaultAsync(e => e.UserId == userId);

        if (userToken is null)
        {
            return;
        }

        dbContext.UsersTokens.Remove(userToken);
        await dbContext.SaveChangesAsync();
    }
}