using EFBlogFluente.Data;
using EFBlogFluente.Models;
using Microsoft.EntityFrameworkCore;

namespace EFBlogFluente
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using var context = new BlogDataContext();
            {
                //var posts = GetPosts(context, 0, 25);
                //var posts2 = GetPosts(context, 25, 25);
                //var posts3 = GetPosts(context, 50, 25);
                //var posts4 = GetPosts(context, 75, 25);

                var postsWithTagsCount = context.PostWithTagsCounts.AsNoTracking().ToList();
                foreach (var item in postsWithTagsCount)
                {
                    Console.WriteLine($"Título do Post - { item.PostName}");
                    Console.WriteLine($"Quantidade de Tags - {item.TagsCount}");
                    Console.WriteLine();
                }
                Console.ReadKey();

                //context.Users.Add(new User
                //{
                //    Bio = "testando o map3",
                //    Email = "teste@teste.com3",
                //    Image = "https://balta.io3",
                //    Name = "Teste Map2",
                //    PasswordHash = "12342",
                //    Slug = "teste-testando3",
                //    GitHub = "rafamalta3"
                //});
                //context.SaveChanges();

                //var user = context.Users.FirstOrDefault(x => x.Bio == "testando o map3");
                //var category = context.Categories.FirstOrDefault(x => x.Name == "Teste Map2");
                //var post = new Post
                //{
                //    Author = user,
                //    Body = "Meu artigo",
                //    Category = category,
                //    CreateDate = DateTime.Now,
                //    // LastUpdateDate =
                //    Slug = "meu-artigo2",
                //    Summary = "Neste artigo vamos conferir...",
                //    Title = "Meu artigo"
                //};

                //context.Posts.Add(post);
                //context.SaveChanges();

                //var tag = context.Tags.FirstOrDefault(x => x.Id == 2);
                //var tag = new Tag
                //{ 
                //    Name = "testando a tag",
                //    Slug = "meu-artigo2"                    
                //};

                //context.Tags.Remove(tag);
                //context.SaveChanges();
            }
        }

        // query otimizida com paginação de dados
        //public static List<Post> GetPosts(BlogDataContext context, int skip = 0, int take = 25)
        //{
        //    var posts = context.Posts
        //        .AsNoTracking()
        //        .Skip(skip)
        //        .Take(take)
        //        .ToList();
        //    return posts;
        //}
    }
}

