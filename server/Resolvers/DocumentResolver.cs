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
    public IQueryable<Document> GetDocuments(DataContext db)
    {
        _logger.Information("Querying documents...");
        return db.Documents;
    }

    [Authorize(Roles = new[] { WellKnownRoles.User })]
    [UseFirstOrDefault]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Document> GetDocument(DataContext db)
    {
        _logger.Information("Querying document...");
        return db.Documents;
    }
}

public partial class Mutation
{
    [Authorize(Roles = new[] { WellKnownRoles.User })]
    [Error(typeof(UnauthorizedException))]
    public async Task<Document> CreateDocumentAsync(
        CreateDocumentInput input,
        DataContext db,
        ClaimsPrincipal principal,
        CancellationToken ct = default
    )
    {
        _logger.Information("Authorizing user for document...");
        var userID = principal.FindFirstValue(ClaimTypes.Sid);
        if (input.UserID != userID)
            throw new UnauthorizedException();

        _logger.Information("Creating document...");
        var document = new Document(input);

        _logger.Information("Committing document to database...");
        var entry = await db.Documents.AddAsync(document, ct);
        await db.SaveChangesAsync(ct);

        _logger.Information("Document created!");
        return entry.Entity;
    }

    [Authorize(Roles = new[] { WellKnownRoles.User })]
    [Error(typeof(EntityNotFoundException))]
    [Error(typeof(UnauthorizedException))]
    public async Task<Document> UpdateDocumentAsync(
        UpdateDocumentInput input,
        DataContext db,
        ClaimsPrincipal principal,
        CancellationToken ct = default
    )
    {
        _logger.Information("Querying document {ID} to update...", input.ID);
        var document = await db.Documents.FirstOrDefaultAsync(d => d.ID == input.ID, ct);
        if (document is null)
            throw new EntityNotFoundException(typeof(Document));

        _logger.Information("Authorizing user for document...");
        var userID = principal.FindFirstValue(ClaimTypes.Sid);
        if (document.UserID != userID)
            throw new UnauthorizedException();

        _logger.Information("Updating document...");
        document.Update(input);

        _logger.Information("Committing document to database...");
        var entry = db.Documents.Update(document);
        await db.SaveChangesAsync(ct);

        _logger.Information("Document updated!");
        await entry.ReloadAsync(ct);
        return entry.Entity;
    }
}