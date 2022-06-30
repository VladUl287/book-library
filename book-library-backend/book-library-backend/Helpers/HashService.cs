﻿using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace book_library_backend.Helpers;

public static class HashService
{
    public static string Hash(string password, string key)
    {
        if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(key))
        {
            throw new ArgumentNullException(nameof(password));
        }

        return Convert.ToBase64String(KeyDerivation.Pbkdf2(
             password: password,
             salt: Encoding.UTF8.GetBytes(key),
             prf: KeyDerivationPrf.HMACSHA256,
             iterationCount: 100000,
             numBytesRequested: 256 / 8));
    }
}
