using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;
using TatBlog.core.Entities;
using TatBlog.data.Contexts;
using TatBlog.services.Blogs;
using TatBlog.services.Blogs.Authors;
using TatBlog.services.Media;
using TatBlog.services.Timing;

namespace TatBlog.WebApi.Extensions
{
	public static class WebApplicationExtensions
	{
		public static WebApplicationBuilder ConfigureServices(
			this WebApplicationBuilder builder)
		{
			builder.Services.AddMemoryCache();
			builder.Services.AddDbContext<BlogDbContext>(
				options => options.UseSqlServer(
					builder.Configuration.GetConnectionString(
						"DefaultConnection")));
			builder.Services.AddScoped<ITimeProvider, LocalTimeProvider>();
			builder.Services.AddScoped<IMediaManager, LocalFileSystemMediaManager>();
			builder.Services.AddScoped<IBlogRepository, BlogRepository>();
			builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
			return builder;
		}
		public static WebApplicationBuilder ConfigureCors(
			this WebApplicationBuilder builder)
		{
			builder.Services.AddCors(options =>
			{
				options.AddPolicy("TatBlogApp", policyBuilder =>
				policyBuilder
				.AllowAnyHeader()
				.AllowAnyMethod()
				.AllowAnyOrigin());
			});
			return builder;
		}


		// cấu hình việc sử dụng NLog
		public static WebApplicationBuilder ConfigureNLog(this WebApplicationBuilder builder)
		{
			builder.Logging.ClearProviders();
			builder.Host.UseNLog();
			return builder;
		}
		public static WebApplicationBuilder ConfigureSwaggerOpenApi(
			this WebApplicationBuilder builder)
		{
			builder.Services.AddEndpointsApiExplorer(); 
			builder.Services.AddSwaggerGen();
			return builder;
		}
		public static WebApplication SetupRequestPipeline(
			this WebApplication app)
		{
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}
			app.UseStaticFiles();
			app.UseHttpsRedirection();
			app.UseCors("TatBlogApp");
			return app;
		}
	}
}
