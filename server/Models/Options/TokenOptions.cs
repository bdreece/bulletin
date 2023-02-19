global using TokenOptions = Bulletin.Server.Models.Options.TokenOptions;

using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Bulletin.Server.Models.Options;

public class TokenOptions
{
    public const string Token = "Token";
    private const string DEFAULT_NAME = "Bulletin";

    public string Issuer { get; set; } = DEFAULT_NAME;
    public string Audience { get; set; } = DEFAULT_NAME;
    private string _secretKey = string.Empty;
    public string SecretKey
    {
        get => _secretKey;
        set
        {
            _secretKey = value;
            SecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(value));
        }
    }

    public SymmetricSecurityKey SecurityKey { get; private set; } = default!;
}