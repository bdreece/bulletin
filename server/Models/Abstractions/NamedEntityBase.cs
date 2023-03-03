namespace Bulletin.Server.Models.Abstractions;

[InterfaceType("NamedEntity")]
public abstract class NamedEntityBase : EntityBase
{
    public string Name { get; protected set; }
    public string? Description { get; protected set; } = default;

    protected NamedEntityBase(CreateNamedEntityInputBase input)
    {
        var (name, description) = input;
        Name = name;
        Description = description;
    }

    protected void Update(UpdateNamedEntityInputBase input)
    {
        var (name, description) = input;
        Name = name ?? Name;
        Description = description ?? Description;
        base.Update();
    }
}

public record CreateNamedEntityInputBase(
    string Name,
    string? Description = default
);

public record UpdateNamedEntityInputBase(
    string? Name = default,
    string? Description = default
);