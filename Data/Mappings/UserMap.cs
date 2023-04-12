using EFBlogFluente.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFBlogFluente.Data.Mappings
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            // Tabela
            builder.ToTable("User");
            // Chave primária
            builder.HasKey(x => x.Id);
            // Identity
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            // Propriedades
            builder.Property(x => x.Name)
                .IsRequired()
                .HasColumnName("Name")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(80);            

            builder.Property(x => x.Bio);
            builder.Property(x => x.Email);
            builder.Property(x => x.Image);
            builder.Property(x => x.PasswordHash);
            builder.Property(x => x.GitHub);

            builder.Property(x => x.Slug)
                .IsRequired()
                .HasColumnName("Slug")
                .HasColumnType("VARCHAR")
                .HasMaxLength(80);

            // Relacionamento N:N
            builder.HasMany(x => x.Roles)
                .WithMany(x => x.Users)
                .UsingEntity<Dictionary<string, object>>(
                "UserRole",
            role => role.HasOne<Role>()
                .WithMany()
                .HasForeignKey("RoleId")
                .HasConstraintName("FK_UserRole_RoleId")
                .OnDelete(DeleteBehavior.Cascade), //Qnd excluir um Role excluirá tbm o User

            user => user.HasOne<User>()
                .WithMany()
                .HasForeignKey("UserId")
                .HasConstraintName("FK_UserRole_UserId")
                .OnDelete(DeleteBehavior.Cascade)); //Qnd excluir um User excluirá tbm o Role

            // Índices
            builder.HasIndex(x => x.Slug, "IX_User_Slug")
                .IsUnique();
        }
    }
}
