using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Builder_WASM.Server
{
    public class AuthOptions
    {
        public const string ISSUER = "MyAuth"; // издатель токена
        public const string AUDIENCE = "MyAuth"; // потребитель токена
        const string KEY = "mysupersecret_secretkey!123";   // ключ для шифрации
        public const int LIFETIME = 1; // время жизни токена - 1 день
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
        }
    }
}
