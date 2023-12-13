using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace cluster_back.Services
{
    public class AuthOptions
    {
        public const string ISSUER = "MyAuthServer";
        public const string AUDIENCE = "MyAuthClient";
        const string KEY = "SUPERSECRETJWTTOKENKEY!!!12346!@#$%^";
        public const int LIFETIME = 43200;
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
