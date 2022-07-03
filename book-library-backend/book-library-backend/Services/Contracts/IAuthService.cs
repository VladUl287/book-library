using OneOf;
using Common.Dtos;
using Common.Errors;

namespace BookLibraryApi.Services.Contracts
{
    public interface IAuthService
    {
        Task<OneOf<AuthSuccess, Error>> Login(AuthModel authModel);

        Task<OneOf<AuthSuccess, Error>> Register(AuthModel authModel);

        Task<OneOf<AuthSuccess, Error>> Refresh(Guid userId, string token);

        Task Logout(Guid userId);
    }
}