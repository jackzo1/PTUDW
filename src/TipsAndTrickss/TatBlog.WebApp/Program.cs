using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using TatBlog.data.Contexts;
using TatBlog.data.Seeders;
using TatBlog.services.Blogs;
using TatBlog.WebApp.Extensions;
using TatBlog.WebApp.Mapsters;

var builder = WebApplication.CreateBuilder(args);
{
    builder
        .ConfigureMvc()
        .ConfigureServices()
        .ConfigureMapster();
        
}

var app = builder.Build();
app.UseRequestPieline();
app.UseBlogRoutes();
app.UseDataSeeder();
app.Run();