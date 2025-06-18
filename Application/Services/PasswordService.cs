using Domain.Interfaces.Auth;
using System.Security.Cryptography;

namespace Application.Services;

public class PasswordService : IPasswordService
{
    private const int SaltSize = 32; // 256 bits
    private const int HashSize = 32; // 256 bits
    private const int Iterations = 100000; // PBKDF2 iterations

    public (string hash, string salt) HashPassword(string password)
    {
        // Generate a random salt
        var salt = new byte[SaltSize];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(salt);
        }

        // Hash the password with the salt using PBKDF2
        using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256))
        {
            var hash = pbkdf2.GetBytes(HashSize);

            return (Convert.ToBase64String(hash), Convert.ToBase64String(salt));
        }
    }

    public bool VerifyPassword(string password, string hash, string salt)
    {
        try
        {
            var saltBytes = Convert.FromBase64String(salt);
            var hashBytes = Convert.FromBase64String(hash);

            // Hash the provided password with the stored salt
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, saltBytes, Iterations, HashAlgorithmName.SHA256))
            {
                var computedHash = pbkdf2.GetBytes(HashSize);

                // Compare the computed hash with the stored hash
                return CryptographicOperations.FixedTimeEquals(hashBytes, computedHash);
            }
        }
        catch
        {
            return false;
        }
    }
}