using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

using Bulletin.Server.Models;
using Bulletin.Server.Models.Options;
using Bulletin.Server.Services;

namespace Bulletin.Server.Resolvers;

public record LoginInput(
    string Email,
    string Password
);

public record AccessTokenResult(
    string AccessToken,
    DateTime Expiration
);

public partial class Mutation
{
    [Error(typeof(BadCredentialsException))]
    [UseMutationConvention(PayloadFieldName = "result")]
    public async Task<AccessTokenResult> LoginAsync(
        LoginInput input,
        DataContext db,
        ITokenService tokenService,
        IPasswordHasher<User> hashService,
        [Service] IOptions<AuthOptions> authOptions,
        [Service] IHttpContextAccessor httpContextAccessor,
        CancellationToken ct = default
    )
    {
        var (email, password) = input;
        var user = await db.Users
            .Include(u => u.Roles)
            .ThenInclude(r => r.Role)
            .FirstOrDefaultAsync(u => u.Email == email, ct);

        if (user is null)
            throw new BadCredentialsException();

        var result = hashService.VerifyHashedPassword(user, user.Hash, password);
        if (result != PasswordVerificationResult.Success)
            throw new BadCredentialsException();

        var (accessToken, expiration) = tokenService.BuildAccessToken(user);
        var (refreshToken, _) = tokenService.BuildRefreshToken(user);
        httpContextAccessor.HttpContext?.Response.Cookies.Append(
            authOptions.Value.RefreshTokenName,
            refreshToken,
            new()
            {
                SameSite = SameSiteMode.None,
                Path = "/",
                Secure = true,
                HttpOnly = true,
            });

        return new(accessToken, expiration);
    }
}