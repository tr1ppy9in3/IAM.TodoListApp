using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

using IAD.TodoListApp.Core.Authentication;

namespace IAD.TodoListApp.DataAccess.Cfg;

internal class RegularUserCfg : IEntityTypeConfiguration<RegularUser>
{
    public void Configure(EntityTypeBuilder<RegularUser> builder)
    {

        builder.Property(ru => ru.Name)
               .IsRequired()
               .HasMaxLength(32);

        builder.Property(ru => ru.Surname)
               .IsRequired()
               .HasMaxLength(32);

        builder.Property(ru => ru.ProfilePic).HasColumnType("bytea");
    }
}
