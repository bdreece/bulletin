using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Serilog;

using Bulletin.Server.Models;
using Bulletin.Server.Models.Options;

namespace Bulletin.Server.Services;

public interface ITokenService : IDisposable, IAsyncDisposable
{
    (string, DateTime) BuildAccessToken(User user);
    (string, DateTime) BuildRefreshToken(User user);
    ClaimsPrincipal DecodeToken(string token, TokenType tokenType);
}

public sealed class TokenService : ITokenService
{
    private readonly ILogger _logger = Log.Logger.ForContext<TokenService>();
    private readonly JwtSecurityTokenHandler _tokenHandler = new();
    private readonly TokenOptions _options;
    private readonly DataContext _db;

    public TokenService(IDbContextFactory<DataContext> dbFactory, IOptions<TokenOptions> options)
    {
        _db = dbFactory.CreateDbContext();
        _options = options.Value;
    }

    public void Dispose() =>
        _db.Dispose();

    public ValueTask DisposeAsync() =>
        _db.DisposeAsync();

    public (string, DateTime) BuildAccessToken(User user)
    {
        var expiration = DateTime.UtcNow.AddHours(4);
        var token = BuildToken(user, expiration);
        return (token, expiration);
    }

    public (string, DateTime) BuildRefreshToken(User user)
    {
        var expiration = DateTime.UtcNow.AddDays(30);
        var token = BuildToken(user, expiration);
        return (token, expiration);
    }

    public ClaimsPrincipal DecodeToken(string token, TokenType tokenType)
    {
        var principal = _tokenHandler.ValidateToken(token, new()
        {
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true,
            ValidAudience = _options.Audience,
            ValidIssuer = _options.Issuer,
            IssuerSigningKey = _options.SecurityKey,
        }, out var _);

        return principal;
    }

    private string BuildToken(User user, DateTime expiration)
    {
        var claims = user.Roles
            .Select(r => new Claim(ClaimTypes.Role, r.Role!.Name))
            .Concat(new Claim[]
            {
                new(ClaimTypes.Sid, user.ID),
                new(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                new(ClaimTypes.Email, user.Email),
                new(ClaimTypes.Hash, user.SecurityToken),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            });

        return _tokenHandler.CreateEncodedJwt(new()
        {
            Subject = new ClaimsIdentity(claims),
            Audience = _options.Audience,
            Expires = expiration,
            Issuer = _options.Issuer,
            SigningCredentials = new(_options.SecurityKey, SecurityAlgorithms.HmacSha256Signature),
        });
    }
}