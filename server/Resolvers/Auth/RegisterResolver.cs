using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using Bulletin.Server.Models;
using Bulletin.Server.Services;

namespace Bulletin.Server.Resolvers;

public record RegisterInput(
    string FirstName,
    string LastName,
    string Email,
    string Password
);

public partial class Mutation
{
    public async Task<User> RegisterAsync(
        RegisterInput input,
        DataContext db,
        IPasswordHasher<User> hashService,
        CancellationToken ct = default
    )
    {
        var (firstName, lastName, email, password) = input;

        _logger.Information("Creating user {Email}...", email);
        var user = new User(firstName, lastName, email);
        var hash = hashService.HashPassword(user, password);
        user.Hash = hash;

        _logger.Information("Querying user roles...");
        var role = await db.Roles.FirstAsync(r => r.Name == WellKnownRoles.User, ct);
        user.Roles.Add(new UserRole
        {
            RoleID = role.ID
        });

        _logger.Information("Committing user to database...");
        var entry = await db.Users.AddAsync(user, ct);
        await db.SaveChangesAsync(ct);

        _logger.Information("User registered!");
        return entry.Entity;
    }
}