using System.Text;
using System.Security.Cryptography;

namespace IAD.TodoListApp.Core.Services;

/// <summary>
/// Сервис криптографии.
/// </summary>
public static class CryptographyService
{
    /// <summary>
    /// Получить хеш пароля.
    /// </summary>
    /// <param name="password"> Пароль. </param>
    /// <param name="salt"> Соль для хеширования пароля. </param>
    /// <returns> Хеш пароля. </returns>
    public static string HashPassword(string password, string salt)
    {
        byte[] hash;

        var saltBytes = Encoding.UTF8.GetBytes(salt);
        byte[] byteString = Encoding.UTF8.GetBytes(password + saltBytes);
        hash = SHA256.HashData(byteString);

        return BitConverter.ToString(hash).Replace("-", "");
    }
}

