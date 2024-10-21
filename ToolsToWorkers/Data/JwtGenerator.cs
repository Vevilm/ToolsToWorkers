using ToolsDistribution.Data;
using ToolsToWorkers.Models;

namespace ToolsToWorkers.Data
{
    public class JwtGenerator
    {
        ApplicationDBContext context;

        public JwtGenerator(ApplicationDBContext context)
        {
            this.context = context;
        }

        public User Authorize(LoginData loginData)
        {
            User? user = context.Users.FirstOrDefault(a => a.Login == loginData.Login && PasswordHasher.Verify(loginData.Password, a.HashedPassword));
            return user;
        }
    }
}
