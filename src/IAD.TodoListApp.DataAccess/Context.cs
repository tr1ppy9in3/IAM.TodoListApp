using Microsoft.EntityFrameworkCore;

using IAD.TodoListApp.Core.Abstractions;
using IAD.TodoListApp.Core.Authentication;
using IAD.TodoListApp.DataAccess.Cfg;


namespace IAD.TodoListApp.DataAccess;

/// <summary>
/// Контекст БД.
/// </summary>
public class Context : DbContext
{
    public DbSet<UserBase> Users { get; set; }
    public DbSet<Admin> PetSitters { get; set; }
    public DbSet<RegularUser> PetOwners { get; set; }

    public Context(DbContextOptions<Context> option) : base(option)
    {
        //Database.EnsureDeleted();
        Database.EnsureCreated();

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserCfg).Assembly);
    }
}
