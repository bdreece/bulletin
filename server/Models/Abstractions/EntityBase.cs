using Microsoft.EntityFrameworkCore;

namespace Bulletin.Server.Models.Abstractions;

[Node]
[PrimaryKey(nameof(ID))]
public abstract class EntityBase
{
    [ID]
    public string ID { get; init; } = Guid.NewGuid().ToString();

    public DateTime DateCreated { get; init; } = DateTime.UtcNow;
    public DateTime DateLastUpdated { get; private set; } = DateTime.UtcNow;

    public void Update() => DateLastUpdated = DateTime.UtcNow;
}