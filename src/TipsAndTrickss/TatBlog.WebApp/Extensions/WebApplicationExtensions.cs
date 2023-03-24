using Microsoft.EntityFrameworkCore;
using TatBlog.data.Contexts;
using TatBlog.data.Seeders;
using TatBlog.services.Blogs;
using TatBlog.services.Media;
using TatBlog.Services.Blogs;

namespace TatBlog.WebApp.Extensions
{
    public static class WebApplicationExtensions
    {
       
        public static WebApplicationBuilder ConfigureMvc(this WebApplicationBuilder builder)
        {
            builder.Services.AddControllersWithViews();
            builder.Services.AddResponseCompression();
            return builder;
        }
      
        public static WebApplicationBuilder ConfigureServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<BlogDbContext>(options => 
            options.UseSqlServer(builder.Configuration
            .GetConnectionString("DefaultConnection")));

            //builder.Services.AddScoped<IMediaManager, LocalFileSystemMediaManager>();
            builder.Services.AddScoped<IBlogRepository, BlogRepository>();
            builder.Services.AddScoped<IDataSeeder, DataSeeder>();

            return builder;
        }
       
        public static WebApplication UseRequestPieline(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Blog/Error");
               
                app.UseHsts();
            }
          
            app.UseHttpsRedirection();
            
            app.UseStaticFiles();
            
            app.UseRouting();
            return app;
        }
        
        public static IApplicationBuilder UseDataSeeder(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            try
            {
                scope.ServiceProvider
                    .GetRequiredService<IDataSeeder>().Initialize();
            }
            catch (Exception ex)
            {

                scope.ServiceProvider.GetRequiredService<ILogger<Program>>().LogError(ex, "could not insert data into database");
            }

            return app;
        }
    }
}
