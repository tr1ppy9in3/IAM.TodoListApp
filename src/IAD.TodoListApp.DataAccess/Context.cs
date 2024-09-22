using Microsoft.EntityFrameworkCore;

using IAD.TodoListApp.Core.Abstractions;
using IAD.TodoListApp.Core.Authentication;
using IAD.TodoListApp.DataAccess.Cfg;
using IAD.TodoListApp.Core;


namespace IAD.TodoListApp.DataAccess;

/// <summary>
/// Контекст БД.
/// </summary>
public class Context : DbContext
{
    // Auth
    public DbSet<UserBase> Users { get; set; }
    public DbSet<Admin> Admins{ get; set; }
    public DbSet<RegularUser> RegularUsers { get; set; }
    public DbSet<Token> Tokens { get; set; }
    
    // Buisness models
    public DbSet<TodoTask> TodoTasks { get; set; }
    public DbSet<TaskCategory> TaskCategories { get; set; }

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
