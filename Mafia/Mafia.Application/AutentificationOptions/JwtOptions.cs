using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Mafia.Application.AutentificationOptions;

public static class JwtOptions
{
    public const string ISSUER = "MyIssuer";
    public const string AUDIENCE = "MyAudience";
    const string KEY = "qzwxecrvtbynumi,o.p/asdfgafdgadfgdfgadfbadfgafdvbsfgbaf";
    public const int LIFETIME = 1;

    public static SymmetricSecurityKey GetKey()
    {
        return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
    }
}