using System.Security.Claims;
using HotChocolate.Authorization;
using Microsoft.EntityFrameworkCore;

using Bulletin.Server.Models;
using Bulletin.Server.Services;

namespace Bulletin.Server.Resolvers;

public partial class Query
{
    [Authorize(Roles = new[] { WellKnownRoles.Admin })]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<User> GetUsers(DataContext db)
    {
        _logger.Information("Querying users...");
        return db.Users;
    }

    [Authorize(Roles = new[] { WellKnownRoles.Admin })]
    [UseFirstOrDefault]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<User> GetUser(DataContext db)
    {
        _logger.Information("Querying user...");
        return db.Users;
    }
}

public partial class Mutation
{
    [Authorize(Roles = new[] { WellKnownRoles.User })]
    [Error(typeof(UnauthorizedException))]
    [Error(typeof(EntityNotFoundException))]
    public async Task<User> UpdateUserAsync(
        UpdateUserInput input,
        DataContext db,
        ClaimsPrincipal principal,
        CancellationToken ct = default
    )
    {
        _logger.Information("Authorizing user for resource...");
        if (input.ID != principal.FindFirstValue(ClaimTypes.Sid))
            throw new UnauthorizedException();

        _logger.Information("Querying user to update...");
        var user = await db.Users.FirstOrDefaultAsync(u => u.ID == input.ID, ct);
        if (user is null)
            throw new EntityNotFoundException(typeof(User));

        _logger.Information("Updating user...");
        user.Update(input);

        _logger.Information("Committing user updates...");
        var entry = db.Users.Update(user);
        await db.SaveChangesAsync(ct);

        _logger.Information("User updated!");
        await entry.ReloadAsync(ct);
        return entry.Entity;
    }
}