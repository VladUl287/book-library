using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace BookLibraryApi.Helpers;

public static class HashHelper
{
    private static readonly ArgumentException PasswordKeyArgumentException = new($"{nameof(HashHelper)} - {nameof(GetHash)} не переданы пароль или ключ");

    public static string GetHash(string password, string key)
    {
        if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(key))
        {
            throw PasswordKeyArgumentException;
        }

        return Convert.ToBase64String(KeyDerivation.Pbkdf2(
             password: password,
             salt: Encoding.UTF8.GetBytes(key),
             prf: KeyDerivationPrf.HMACSHA256,
             iterationCount: 100000,
             numBytesRequested: 256 / 8));
    }
}