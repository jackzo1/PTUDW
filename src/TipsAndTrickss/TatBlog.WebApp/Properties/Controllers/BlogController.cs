using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Mvc;
using TatBlog.services.Blogs;

namespace TatBlog.WebApp.Controllers
{
	public class BlogController : Controller
	{
		private readonly IBlogRepository _blogRepository;

		public BlogController(IBlogRepository blogRepository)
		{
			_blogRepository = blogRepository;
		}

		public async Task<IActionResult> Index(
                                   [FromQuery(Name = "k")] string keyword = null,
                                  [FromQuery(Name = "p")] int pageNumber = 1,
								  [FromQuery(Name = "ps")] int pageSize = 10)
		{
			var postQuery = new PostQuery()
			{
				PublishedOnly = true,
				Keyword = keyword
			};
			var postsList = await _blogRepository.GetPostAsync(postQuery, pageNumber, pageSize);

			ViewBag.PostQuery = postQuery;

			return View(postsList);
		}

		//public IActionResult About()
		//	==>View();
		//public IActionResult Contract()
		//	==>View();
		//public IActionResult Rss()
		//	==>Content("Noi dung se dươc cap nhat");
	}
}
