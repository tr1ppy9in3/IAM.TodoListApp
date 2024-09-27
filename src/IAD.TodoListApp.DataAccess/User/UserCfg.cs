using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

using IAD.TodoListApp.Core.Abstractions;
using IAD.TodoListApp.Core.Authentication;
using IAD.TodoListApp.Core.Enums;

namespace IAD.TodoListApp.DataAccess.User;

/// <summary>
/// Конфигурация для абстрактной модели User.
/// </summary>
internal class UserCfg : IEntityTypeConfiguration<UserBase>
{
    public void Configure(EntityTypeBuilder<UserBase> builder)
    {
        builder.HasKey(u => u.Id);

        builder.HasDiscriminator(u => u.Role)
              .HasValue<RegularUser>(UserRole.RegularUser)
              .HasValue<Admin>(UserRole.Admin);

    }
}
