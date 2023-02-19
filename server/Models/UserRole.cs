using System.ComponentModel.DataAnnotations.Schema;
using Bulletin.Server.Models.Abstractions;

namespace Bulletin.Server.Models;

[Table(nameof(UserRole))]
public sealed class UserRole : EntityBase
{
    [ForeignKey(nameof(User))]
    [ID(nameof(User))]
    public string UserID { get; init; }
    public User? User { get; private init; }

    [ForeignKey(nameof(Role))]
    [ID(nameof(Role))]
    public string RoleID { get; init; }
    public Role? Role { get; private init; }

#pragma warning disable CS8618
    internal UserRole() { }
#pragma warning disable CS8618

    public UserRole(string userID, string roleID) : base()
    {
        UserID = userID;
        RoleID = roleID;
    }
}