using System.Security.Claims;
using HotChocolate.Authorization;
using Microsoft.EntityFrameworkCore;

using Bulletin.Server.Models;
using Bulletin.Server.Services;

namespace Bulletin.Server.Resolvers;

public partial class Query
{
    [Authorize(Roles = new[] { WellKnownRoles.User })]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Directory> GetDirectories(DataContext db)
    {
        _logger.Information("Querying directories...");
        return db.Directories;
    }

    [Authorize(Roles = new[] { WellKnownRoles.User })]
    [UseFirstOrDefault]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Directory> GetDirectory(DataContext db)
    {
        _logger.Information("Querying directory...");
        return db.Directories;
    }
}

public partial class Mutation
{
    [Authorize(Roles = new[] { WellKnownRoles.User })]
    [Error(typeof(UnauthorizedException))]
    public async Task<Directory> CreateDirectoryAsync(
        CreateDirectoryInput input,
        DataContext db,
        ClaimsPrincipal principal,
        CancellationToken ct = default
    )
    {
        var userID = principal.FindFirstValue(ClaimTypes.Sid);
        if (input.UserID != userID)
            throw new UnauthorizedException();

        _logger.Information("Creating directory...");
        var directory = new Directory(input);

        _logger.Information("Committing directory to database...");
        var entry = await db.Directories.AddAsync(directory, ct);
        await db.SaveChangesAsync(ct);

        _logger.Information("Directory created!");
        return entry.Entity;
    }

    [Authorize(Roles = new[] { WellKnownRoles.User })]
    [Error(typeof(EntityNotFoundException))]
    [Error(typeof(UnauthorizedException))]
    public async Task<Directory> UpdateDirectoryAsync(
        UpdateDirectoryInput input,
        DataContext db,
        ClaimsPrincipal principal,
        CancellationToken ct = default
    )
    {
        _logger.Information("Querying directory {ID} to update...", input.ID);
        var directory = await db.Directories.FirstOrDefaultAsync(d => d.ID == input.ID, ct);
        if (directory is null)
            throw new EntityNotFoundException(typeof(Directory));

        _logger.Information("Authorizing user for given directory...");
        var userID = principal.FindFirstValue(ClaimTypes.Sid);
        if (directory.UserID != userID)
            throw new UnauthorizedException();

        _logger.Information("Updating directory...");
        directory.Update(input);

        _logger.Information("Committing directory to database...");
        var entry = db.Directories.Update(directory);
        await db.SaveChangesAsync(ct);

        _logger.Information("Directory updated!");
        await entry.ReloadAsync(ct);
        return entry.Entity;
    }
}