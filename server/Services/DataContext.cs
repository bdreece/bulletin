using Microsoft.EntityFrameworkCore;

using Bulletin.Server.Models;

namespace Bulletin.Server.Services;

public class DataContext : DbContext
{
    public DataContext() { }
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Role>().HasData(new[]
        {
            new Role(WellKnownRoles.Admin),
            new Role(WellKnownRoles.User)
        });
    }

    public virtual DbSet<User> Users { get; protected init; } = default!;
    public virtual DbSet<UserRole> UserRoles { get; protected init; } = default!;
    public virtual DbSet<Role> Roles { get; protected init; } = default!;

    public virtual DbSet<Directory> Directories { get; protected init; } = default!;
    public virtual DbSet<Document> Documents { get; protected init; } = default!;
}