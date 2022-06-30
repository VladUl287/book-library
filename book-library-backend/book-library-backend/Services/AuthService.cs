using OneOf;
using DataAccess;
using Common.Dtos;
using Common.Errors;
using Common.Options;
using DataAccess.Models;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using book_library_backend.Helpers;
using book_library_backend.Services.Contracts;

namespace book_library_backend.Services;

public class AuthService : IAuthService
{
    private readonly DatabaseContext dbContext;
    private readonly AuthOptions authOptions;
    private readonly PassOptions passwordOptions;

    public AuthService(DatabaseContext dbContext, IOptions<PassOptions> passwordOptions, IOptions<AuthOptions> authOptions)
    {
        this.dbContext = dbContext;
        this.authOptions = authOptions.Value;
        this.passwordOptions = passwordOptions.Value;
    }

    public async Task<OneOf<AuthSuccess, Error>> Login(AuthModel authModel)
    {
        var user = await dbContext.Users
            .FirstOrDefaultAsync(x => x.Email == authModel.Email);

        if (user is null)
        {
            return Errors.LoginFaild;
        }

        var hashSecret = passwordOptions.HashSecret;
        var hashPassword = HashService.Hash(authModel.Password, hashSecret);

        if (user.Password != hashPassword)
        {
            return Errors.LoginFaild;
        }

        var issuer = authOptions.Issuer;
        var audience = authOptions.Audience;
        var accessTokenKey = authOptions.AccessSecret;
        var refreshTokenKey = authOptions.RefreshSecret;
        var lifeTime = authOptions.LifeTime;

        var accessToken = JwtService.Generate(user, accessTokenKey, issuer, audience, DateTime.UtcNow.AddMinutes(lifeTime));
        var refreshToken = JwtService.Generate(user, refreshTokenKey, issuer, audience, DateTime.UtcNow.AddDays(lifeTime));

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
            return Errors.LoginFaild;
        }

        var hashSecret = passwordOptions.HashSecret;
        var hashPassword = HashService.Hash(authModel.Password, hashSecret);

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

        var issuer = authOptions.Issuer;
        var audience = authOptions.Audience;
        var accessTokenKey = authOptions.AccessSecret;
        var refreshTokenKey = authOptions.RefreshSecret;
        var lifeTime = authOptions.LifeTime;

        var valid = JwtService.ValidateToken(dbToken.RefreshToken, refreshTokenKey, issuer, audience);

        if (!valid)
        {
            dbContext.UsersTokens.Remove(dbToken);
            await dbContext.SaveChangesAsync();

            return Errors.LoginFaild;
        }

        var user = await dbContext.Users.FindAsync(dbToken.UserId);

        var accessToken = JwtService.Generate(user, accessTokenKey, issuer, audience, DateTime.UtcNow.AddMinutes(lifeTime));
        var refreshToken = JwtService.Generate(user, refreshTokenKey, issuer, audience, DateTime.UtcNow.AddDays(lifeTime));

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