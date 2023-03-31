using TatBlog.core.Entities;
using TatBlog.data.Contexts;
using TatBlog.data.Seeders;
using TatBlog.services.Blogs;

var context = new BlogDbContext();
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
IBlogRepository blogRepository = new BlogRepository(context);
var postss = await blogRepository.GetPopularArticleAsync(3);
Console.WriteLine("{0,-5}{1,-50}{2,10}","ID","Name" ,"Count");
foreach (var post in posts)
{
    Console.WriteLine($"ID        : {post.Id}");
    Console.WriteLine($"Title     : {post.Title}");
    Console.WriteLine($"View      : {post.ViewCount}");
    Console.WriteLine("Date       : {0:MM/dd/yyyy}", post.PostedDate);
    Console.WriteLine($"Author    : {0}", post.Author);
    Console.WriteLine($"Category  : {0}", post.Category);
    Console.WriteLine("".PadRight(80, '-'));
}
var seeder = new DataSeeder(context);
seeder.Initialize();
var authors = context.Authors.ToList();
Console.WriteLine("{0,-4}{1,-30}{2,-30}{3,12}",
    "ID","Full Name","Email","Joined Date");
foreach (var author in authors)
{
    Console.WriteLine("{0,-4}{1,-30}{2,-30}{3,12}",
        author.Id,author.FullName,author.Email,author.JoinedDate);
}
var categories = await blogRepository.GetCategoriesAsync();
Console.WriteLine("{0,-5}{1,-50}{2,10}",
    "ID", "Name", "Count");
foreach (var item in categories)
{
    Console.WriteLine("{0,-4}{1,-30}{2,-30}{3,12}",
        item.Id, item.Name, item.PostCount);
}