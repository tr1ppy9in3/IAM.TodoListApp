using IAD.TodoListApp.Core;
using IAD.TodoListApp.Core.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace IAD.TodoListApp.DataAccess.Cfg;

using TaskStatus = Core.Enums.TaskStatus;

/// <summary>
/// Конфигурация для модели TodoTask.
/// </summary>
internal class TodoTaskCfg : IEntityTypeConfiguration<TodoTask>
{
    public void Configure(EntityTypeBuilder<TodoTask> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Title)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(t => t.Description)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(t => t.DueDate)
            .IsRequired();

        builder.Property(t => t.Priority)
            .HasDefaultValue(TaskPriority.Medium);

        builder.Property(t => t.Status)
            .HasDefaultValue(TaskStatus.Pending);

        builder.HasOne(t => t.User)
            .WithMany()
            .HasForeignKey(t => t.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(t => t.Category)
            .WithMany()
            .HasForeignKey(t => t.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
