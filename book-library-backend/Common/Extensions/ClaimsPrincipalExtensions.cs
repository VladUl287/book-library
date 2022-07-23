using System.ComponentModel;
using System.Security.Claims;

namespace Domain.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static T GetLoggedInUserId<T>(this ClaimsPrincipal principal) where T : struct
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
        else if (typeof(T) == typeof(Guid))
        {
            return (T)TypeDescriptor.GetConverter(typeof(T)).ConvertFromInvariantString(loggedInUserId.Value);
        }
        else
        {
            throw new Exception("Использован неверный тип данных");
        }
    }
}

