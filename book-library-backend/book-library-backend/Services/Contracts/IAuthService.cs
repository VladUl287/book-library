using OneOf;
using Domain.Dtos;
using Domain.Errors;

namespace BookLibraryApi.Services.Contracts;

public interface IAuthService
{
    Task<OneOf<AuthSuccess, Error>> Login(AuthModel authModel);

    Task<OneOf<AuthSuccess, Error>> Register(AuthModel authModel);

    Task<OneOf<AuthSuccess, Error>> Refresh(string token);

    Task Logout(Guid userId);
}