using IAD.TodoListApp.Core;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace IAD.TodoListApp.DataAccess.TaskCategory;

/// <summary>
/// Конфигурация для модели TaskCategory.
/// </summary>
internal class TaskCategoryConfiguration : IEntityTypeConfiguration<Core.TaskCategory>
{
    public void Configure(EntityTypeBuilder<Core.TaskCategory> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(c => c.Description)
            .IsRequired()
            .HasMaxLength(500);
    }
}
