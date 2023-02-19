using System.ComponentModel.DataAnnotations.Schema;

using Bulletin.Server.Models.Abstractions;

namespace Bulletin.Server.Models;

[Table(nameof(Role))]
public sealed class Role : EntityBase
{
    public string Name { get; private init; }

    public Role(string name) : base()
    {
        Name = name;
    }
}