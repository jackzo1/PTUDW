using TatBlog.data.Contexts;
using TatBlog.data.Seeders;
using TatBlog.services.Blogs;
using TatBlog.winapp;

internal class Program
{
    static async Task Main(string[] args)
    {

        var context = new BlogDbContext();


        var seeder = new DataSeeder(context);


        seeder.Initialize();


        var authors = context.Authors.ToList();


        Console.WriteLine("{0, -4}{1, -30}{2, -30}{3, 12}", "ID", "ID", "Full Name", " Email", "Joined Date");

        foreach (var author in authors)
        {
            Console.WriteLine("{0, -4}{1, -30}{2, -30}{3, 12:MM/dd/yyyy}", author.Id, author.FullName, author.Email, author.JoinedDate);
        }


        var posts = context.Posts
                           .Where(p => p.Published)
                           .OrderBy(p => p.Title)
                           .Select(p => new
                           {
                               Id = p.Id,
                               Title = p.Title,
                               ViewCount = p.ViewCount,
                               PostedDate = p.PostedDate,
                               Author = p.Author.FullName,
                               Category = p.Category.Name
                           })
                           .ToList();



   

    }
}
