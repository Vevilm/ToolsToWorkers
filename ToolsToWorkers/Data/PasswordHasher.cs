using BCrypt; 
namespace ToolsDistribution.Data
{
    public static class PasswordHasher
    {
        public static string Generate(string password)
        {
            return BCrypt.Net.BCrypt.EnhancedHashPassword(password);
        }
        public static bool Verify(string password, string hash)
        {
            return BCrypt.Net.BCrypt.EnhancedVerify(password, hash);
        }
    }
}
