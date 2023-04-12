using EFBlogFluente.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFBlogFluente.Data.Mappings
{
    public class PostMap : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            // Tabela
            builder.ToTable("Post");
            // Chave primária
            builder.HasKey(x => x.Id);
            // Identity
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            // Propriedades
            builder.Property(x => x.LastUpdateDate)
                .IsRequired()
                .HasColumnName("LastUpdateDate")
                .HasColumnType("SMALLDATETIME")
                // o SQL irá gerenciar
                .HasDefaultValueSql("GETDATE()");
            // o EF irá gerenciar
            //.HasDefaultValue(DateTime.Now.ToUniversalTime());                           

            // Índices
            builder.HasIndex(x => x.Slug, "IX_Post_Slug")
                .IsUnique();

            // Relacionamentos 1:N
            builder.HasOne(x => x.Author)
                .WithMany(x => x.Posts)
                .HasConstraintName("FK_Post_Author")
                .OnDelete(DeleteBehavior.Cascade); //Qnd excluir um Post excluirá tbm o Author

            builder.HasOne(x => x.Category)
                .WithMany(x => x.Posts)
                .HasConstraintName("FK_Post_Category")
                .OnDelete(DeleteBehavior.Cascade); //Qnd excluir um Post excluirá tbm a Category

            // Relacionamento N:N
            builder.HasMany(x => x.Tags)
                .WithMany(x => x.Posts)
                .UsingEntity<Dictionary<string, object>>(
                "PostTag",
            post => post.HasOne<Tag>()
                .WithMany()
                .HasForeignKey("PostId")
                .HasConstraintName("FK_PostTag_PostId")
                .OnDelete(DeleteBehavior.Cascade), //Qnd excluir uma Tag excluirá tbm o Post

            tag => tag.HasOne<Post>()
                .WithMany()
                .HasForeignKey("TagId")
                .HasConstraintName("FK_PostTag_TagId")
                .OnDelete(DeleteBehavior.Cascade)); //Qnd excluir um Post excluirá tbm a Tag
        }
    }
}
