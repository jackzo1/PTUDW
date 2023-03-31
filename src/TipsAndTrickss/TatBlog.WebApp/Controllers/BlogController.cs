using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TatBlog.core.DTO;
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


		// Action này xử lý HTTP request đến trang chủ của
		// ứng dụng web hoặc tìm kiếm bài viết theo từ khóa
		public async Task<IActionResult> Index(
			[FromQuery(Name = "k")] string keyword = null,
			[FromQuery(Name = "p")] int pageNumber = 1,
			[FromQuery(Name = "ps")] int pageSize = 2)
		{
			//Tạo oject chứa điều kiện truy vấn
			var postQuery = new PostQuery()
			{
				PublishedOnly = true,
				Keyword = keyword
			};

			var postList = await _blogRepository
				.GetPagedPostsAsync(postQuery, pageNumber, pageSize);

			ViewBag.PostQuery = postQuery;

			return View(postList);
		}
		//public async Task<IActionResult> Category([FromRoute(Name = "slug")] string slugCate = null)
		//{
		//	var postQuery = new PostQuery()
		//	{
		//		CategorySlug = slugCate,

		//	};
		//	ViewBag.PostQuery = postQuery;
		//	var postsList = await _blogRepository.GetPagedPostsAsync(postQuery);

		//	return View("Index", postsList);
		//}
		//public async Task<IActionResult> Post([
		//	FromRoute(Name = "year")] int year = 2022,
		//	[FromRoute(Name = "month")] int month = 9,
		//	[FromRoute(Name = "day")] int day = 5,
		//	[FromRoute(Name = "slug")] string slug = null)
		//{
		//	var postQuery = new PostQuery()
		//	{
		//		Month = month,
		//		Year = year,
		//		Day = day,
		//		UrlSlug = slug,

		//	};
		//	ViewBag.PostQuery = postQuery;
		//	//var posts = await _blogRepository.GetPostAsync(postQuery);
		//	//try
		//	//{
		//	//    if (posts != null && !posts.Published)
		//	//    {
		//	//        await _blogRepository.IncreaseViewCountAsync(posts.Id);
		//	//    }
		//	//}
		//	//catch (NullReferenceException)
		//	//{

		//	//    return View("Error");
		//	//}
		//	return View("DetailPost"/*, posts*/);
		//}
		//public async Task<IActionResult> Archives([FromRoute(Name = "year")] int year = 2021,[FromRoute(Name = "month")] int month = 9)
		////{
		////    var postQuery = new PostQuery()
		////    {
		////        Year = year,
		////        Month = month
		////    };
		////    var posts = _blogRepository.GetPagedPostsAsync(postQuery);
		////    return View();
		////}

		//public IActionResult Index()
		//{
		//	ViewBag.CurrentTime = DateTime.Now.ToString("HH:mm:ss");
		//	return View();
		//}
		public IActionResult About()
            => View("About");

        public IActionResult Contact()
            => View();

        public IActionResult Rss()
            => Content("Nội dung sẽ được cập nhật");

        public IActionResult Error()
        {
            return View();
        }

    }
}
