using System.Security.Claims;
using HotChocolate.Authorization;

using Bulletin.Server.Models;
using Bulletin.Server.Services;

namespace Bulletin.Server.Resolvers;

public record SelfResult(
    [property: ID(nameof(User))] string? ID,
    string? Name,
    string? Email,
    IEnumerable<string> Roles
);

public partial class Query
{
    [Authorize]
    public SelfResult GetSelf(ClaimsPrincipal principal)
    {
        var id = principal.FindFirstValue(ClaimTypes.Sid);
        var name = principal.FindFirstValue(ClaimTypes.Name);
        var email = principal.FindFirstValue(ClaimTypes.Email);
        var roles = principal.FindAll(ClaimTypes.Role).Select(c => c.Value);

        return new(id, name, email, roles);
    }
}