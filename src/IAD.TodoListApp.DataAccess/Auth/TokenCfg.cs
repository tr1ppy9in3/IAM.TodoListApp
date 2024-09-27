using IAD.TodoListApp.Core;
using IAD.TodoListApp.Core.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IAD.TodoListApp.DataAccess.Auth
{
    public class TokenCfg : IEntityTypeConfiguration<Token>
    {
        public void Configure(EntityTypeBuilder<Token> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Value)
                .IsRequired()
                .HasMaxLength(512);

            builder.HasOne<UserBase>()
                .WithMany()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
