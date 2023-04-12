using EFBlogFluente.Data.Mappings;
using EFBlogFluente.Models;
using Microsoft.EntityFrameworkCore;

namespace EFBlogFluente.Data
{
    public class BlogDataContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<User> Users { get; set; }            
        public DbSet<Tag> Tags { get; set; }
        public DbSet<PostWithTagsCount> PostWithTagsCounts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
           => options.UseSqlServer("Server=localhost,1433;Database=BlogFluentMap;User ID=sa;Password=12345678;Trusted_Connection=False;TrustServerCertificate=True");
            // options.LogTo(Console.WriteLine);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CategoryMap());
            modelBuilder.ApplyConfiguration(new PostMap());
            modelBuilder.ApplyConfiguration(new UserMap());           
            modelBuilder.ApplyConfiguration(new TagMap());

            // mapeia uma entidade que não existe no banco
            modelBuilder.Entity<PostWithTagsCount>(
               x =>
                {
                    x.HasNoKey();
                    x.ToView("vwPostComTag"); // mapeia a View
                    x.Property(x => x.PostName).HasColumnName("PostName");
                    x.Property(x => x.TagsCount).HasColumnName("Quantidade_de_Tags");

                    // Realiza uma "query direta"

                    //x.ToSqlQuery(@"
                                    //SELECT TOP 100
			                            //[Post].[Title] AS [PostName],		
			                                //COUNT([Tag].[Id]) AS [TagsCount]
		                                //FROM 
			                                //[Post]
			                            //INNER JOIN [Tag]
				                            //ON [Tag].[Id] = [Tag].[Id]	
			                            // GROUP BY 
				                            //[Post].[Title]");
                    
                });
        }        
    }
}
