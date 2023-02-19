using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

using Bulletin.Server.Models.Abstractions;
using Bulletin.Server.Services;

namespace Bulletin.Server.Models;

[Table(nameof(Document))]
public class Document : EntityBase
{
    private const string DEFAULT_NAME = "New Document";

    public string Name { get; private set; } = DEFAULT_NAME;
    public string Filename { get; private init; }

    [ForeignKey(nameof(Directory))]
    [ID(nameof(Directory))]
    public string DirectoryID { get; private set; }
    public Directory? Directory { get; private init; }

    [ForeignKey(nameof(User))]
    [ID(nameof(User))]
    public string UserID { get; private init; }
    public User? User { get; private init; }

#pragma warning disable CS8618
    internal Document() { }
#pragma warning restore CS8618

    public Document(CreateDocumentInput input) : base()
    {
        var (userID, directoryID, filename, name) = input;
        Filename = filename;
        DirectoryID = directoryID;
        UserID = userID;
        Name = name ?? Name;
    }

    public static Task<Document?> GetAsync(string id, DataContext db, CancellationToken ct = default) =>
        db.Documents.FirstOrDefaultAsync(d => d.ID == id, ct);

    public void Update(UpdateDocumentInput input)
    {
        var (_, directoryID, name) = input;
        Name = name ?? Name;
        DirectoryID = directoryID ?? DirectoryID;

        base.Update();
    }
}

public record CreateDocumentInput(
    [property: ID(nameof(User))] string UserID,
    [property: ID(nameof(Directory))] string DirectoryID,
    string Filename,
    string? Name = default
);

public record UpdateDocumentInput(
    [property: ID] string ID,
    [property: ID(nameof(Directory))] string? DirectoryID = default,
    string? Name = default
);