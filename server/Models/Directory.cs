global using Directory = Bulletin.Server.Models.Directory;

using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

using Bulletin.Server.Models.Abstractions;
using Bulletin.Server.Services;

namespace Bulletin.Server.Models;

[Table(nameof(Directory))]
public sealed class Directory : EntityBase
{
    private const string DEFAULT_NAME = "New Directory";

    public string Name { get; private set; } = DEFAULT_NAME;

    [ForeignKey(nameof(ParentDirectory))]
    [ID(nameof(Directory))]
    public string? ParentDirectoryID { get; private set; }
    public Directory? ParentDirectory { get; private init; }

    [ForeignKey(nameof(User))]
    [ID(nameof(User))]
    public string UserID { get; private init; }
    public User? User { get; private init; }

    public ICollection<Directory> SubDirectories { get; init; } =
        new List<Directory>();

    public ICollection<Document> Documents { get; init; } =
        new List<Document>();

#pragma warning disable CS8618
    internal Directory() { }
#pragma warning restore CS8618

    public Directory(CreateDirectoryInput input) : base()
    {
        var (userID, parentDirectoryID, name) = input;
        UserID = userID;
        Name = name ?? Name;
        ParentDirectoryID = parentDirectoryID;
    }

    public static Task<Directory?> GetAsync(string id, DataContext db, CancellationToken ct = default) =>
        db.Directories.FirstOrDefaultAsync(d => d.ID == id, ct);

    public void Update(UpdateDirectoryInput input)
    {
        var (_, parentDirectoryID, name) = input;
        Name = name ?? Name;
        ParentDirectoryID = parentDirectoryID ?? ParentDirectoryID;

        base.Update();
    }
}

public record CreateDirectoryInput(
    [property: ID(nameof(User))] string UserID,
    [property: ID(nameof(Directory))] string? ParentDirectoryID = default,
    string? Name = default
);

public record UpdateDirectoryInput(
    [property: ID] string ID,
    [property: ID(nameof(Directory))] string? ParentDirectoryID = default,
    string? Name = default
);