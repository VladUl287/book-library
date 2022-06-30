using System.Security.Claims;

namespace Common.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static T GetLoggedInUserId<T>(this ClaimsPrincipal principal)
        {
            if (principal is null)
            {
                throw new ArgumentNullException(nameof(principal));
            }

            var loggedInUserId = principal.FindFirst(ClaimTypes.NameIdentifier);

            if (typeof(T) == typeof(string) && loggedInUserId is not null)
            {
                return (T)Convert.ChangeType(loggedInUserId, typeof(T));
            }
            else if (typeof(T) == typeof(int) || typeof(T) == typeof(long))
            {
                return loggedInUserId is not null ? (T)Convert.ChangeType(loggedInUserId, typeof(T)) : (T)Convert.ChangeType(0, typeof(T));
            }
            else
            {
                throw new Exception("Использован неверный тип данных");
            }
        }
    }
}
