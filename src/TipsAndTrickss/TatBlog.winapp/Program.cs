using TatBlog.core.Entities;
using TatBlog.data.Contexts;
using TatBlog.data.Seeders;
using TatBlog.services.Blogs;
using TatBlog.Services.Blogs;
using TatBlog.winapp;


internal class Program
{
    static async Task Main(string[] args)
    {

        var context = new BlogDbContext();
        IBlogRepository blogRepository = new BlogRepository(context);
        var pagingParams = new PagingParams
        {
            PageSize = 5,
            PageNumber = 1,
            SortColumn = "Id",
            SortOrder = "DESC"

        };
        var tagList = await blogRepository.GetPagedTagsAsync(pagingParams);
        var categories = await blogRepository.GetCategoriesAsync();

        Console.WriteLine("{0, -5}{1, -50}{2, 10}", "ID", "Name", "Count");

        foreach (var item in categories)
        {
            Console.WriteLine("{0, -5}{1, -50}{2, 10}", item.Id, item.Name, item.PostCount);
        }


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

        
   
         var postss = await blogRepository.GetPopularArticleAsync(3);

         foreach (var post in postss)
        {
            Console.WriteLine($"ID        : {post.Id}");
            Console.WriteLine($"Title     : {post.Title}");
            Console.WriteLine($"View      : {post.ViewCount}");
            Console.WriteLine("Date       : {0:MM/dd/yyyy}", post.PostedDate);
            Console.WriteLine($"Author    : {post.Author.FullName}");
            Console.WriteLine($"Category  : {post.Category.Name}");
            Console.WriteLine("".PadRight(80, '-'));
        }

        Console.WriteLine("\nTìm một thẻ (Tag) theo tên định danh (slug)");
        var tagBySlug = await blogRepository.GetTagBySlugAsync("google");
        Console.WriteLine("{0, -20}{1, -50}{2, 10}", "Name", "Description", "Slug");
        Console.WriteLine("{0, -20}{1, -50}{2, 10}", tagBySlug.Name, tagBySlug.Description, tagBySlug.UrlSlug);
        ////xóa
      





    }
}
