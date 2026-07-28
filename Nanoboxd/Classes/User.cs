using System.Security.Cryptography;

namespace Classes;

public class User
{
    private const int SaltSize = 16;
    private const int HashSize = 32;
    private const int Iterations = 100_000;

    public string Username { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string PasswordSalt { get; set; } = string.Empty;
    public List<RatedMovie> Collection { get; set; } = [];

    public void SetPassword(string password)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);
        byte[] hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, HashAlgorithmName.SHA256, HashSize);

        PasswordSalt = Convert.ToBase64String(salt);
        PasswordHash = Convert.ToBase64String(hash);
    }

    public bool VerifyPassword(string password)
    {
        byte[] salt = Convert.FromBase64String(PasswordSalt);
        byte[] expected = Convert.FromBase64String(PasswordHash);
        byte[] actual = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, HashAlgorithmName.SHA256, HashSize);

        return CryptographicOperations.FixedTimeEquals(actual, expected);
    }
}
