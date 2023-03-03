using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

using Bulletin.Server.Models.Abstractions;
using Bulletin.Server.Services;

namespace Bulletin.Server.Models;

[Table(nameof(Document))]
public class Document : NamedEntityBase
{
    internal const string DEFAULT_NAME = "New Document";

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
    internal Document() : base(new(DEFAULT_NAME)) { }
#pragma warning restore CS8618

    public Document(CreateDocumentInput input) : base(input)
    {
        var (userID, directoryID, filename, _, _) = input;
        Filename = filename;
        DirectoryID = directoryID;
        UserID = userID;
    }

    public static Task<Document?> GetAsync(string id, DataContext db, CancellationToken ct = default) =>
        db.Documents.FirstOrDefaultAsync(d => d.ID == id, ct);

    public void Update(UpdateDocumentInput input)
    {
        var (_, directoryID, _, _) = input;
        DirectoryID = directoryID ?? DirectoryID;
        base.Update(input);
    }
}

public record CreateDocumentInput(
    [property: ID(nameof(User))] string UserID,
    [property: ID(nameof(Directory))] string DirectoryID,
    string Filename,
    string? Name = default,
    string? Description = default
) : CreateNamedEntityInputBase(Name ?? Document.DEFAULT_NAME, Description);

public record UpdateDocumentInput(
    [property: ID] string ID,
    [property: ID(nameof(Directory))] string? DirectoryID = default,
    string? Name = default,
    string? Description = default
) : UpdateNamedEntityInputBase(Name, Description);