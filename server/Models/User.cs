using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;

using Bulletin.Server.Models.Abstractions;
using Bulletin.Server.Services;

namespace Bulletin.Server.Models;

[Table(nameof(User))]
[Index(nameof(Email), IsUnique = true)]
public class User : EntityBase
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }

    private string _hash = string.Empty;

    [GraphQLIgnore]
    public string Hash
    {
        get => _hash;
        set
        {
            _hash = value;
            SecurityToken = CreateSecurityToken();
        }
    }

    [GraphQLIgnore]
    public string SecurityToken { get; private set; } = string.Empty;

    public ICollection<UserRole> Roles { get; init; } =
        new List<UserRole>();

    public ICollection<Directory> Directories { get; init; } =
        new List<Directory>();

    public ICollection<Document> Documents { get; init; } =
        new List<Document>();

#pragma warning disable CS8618
    internal User() { }
#pragma warning restore CS8618

    public User(string firstName, string lastName, string email)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
    }

    public static Task<User?> GetAsync(string id, DataContext db, CancellationToken ct = default) =>
        db.Users.FirstOrDefaultAsync(u => u.ID == id, ct);

    public void Update(UpdateUserInput input)
    {
        var (_, firstName, lastName, email) = input;
        FirstName = firstName ?? FirstName;
        LastName = lastName ?? LastName;
        Email = email ?? Email;

        base.Update();
    }

    private static string CreateSecurityToken()
    {
        using var rng = RandomNumberGenerator.Create();
        var bytes = new byte[64];
        rng.GetBytes(bytes);
        return Convert.ToBase64String(bytes);
    }
}

public record UpdateUserInput(
    [property: ID] string ID,
    string? FirstName = default,
    string? LastName = default,
    string? Email = default
);