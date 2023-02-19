using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

using Bulletin.Server.Models;
using Bulletin.Server.Models.Options;
using Bulletin.Server.Services;

namespace Bulletin.Server.Resolvers;

public record RefreshResult(AccessTokenResult? Result, string? Error = default);

public partial class Query
{
    public async Task<RefreshResult> RefreshAsync(
        DataContext db,
        ITokenService tokenService,
        [Service] IOptions<AuthOptions> authOptions,
        [Service] IHttpContextAccessor httpContextAccessor,
        CancellationToken ct = default
    )
    {
        var cookies = httpContextAccessor.HttpContext?.Request.Cookies;
        if (cookies?.TryGetValue(authOptions.Value.RefreshTokenName, out var refreshToken) ?? false)
        {
            var principal = tokenService.DecodeToken(refreshToken, TokenType.REFRESH);
            var userID = principal.FindFirst(ClaimTypes.Sid)?.Value ?? string.Empty;
            var user = await db.Users
                .Include(u => u.Roles)
                .ThenInclude(r => r.Role)
                .FirstOrDefaultAsync(u => u.ID == userID, ct);

            if (user is null)
                throw new EntityNotFoundException(typeof(User));

            var (accessToken, expiration) = tokenService.BuildAccessToken(user);
            var result = new AccessTokenResult(accessToken, expiration);
            return new(result);
        }
        else throw new MissingTokenException(TokenType.REFRESH);
    }
}