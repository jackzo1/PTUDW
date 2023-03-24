using Microsoft.AspNetCore.Mvc;

namespace TatBlog.WebApp.Areas.Admin.Controllers
{
	public class DashbroardController : Controller
	{
		public IActionResult Index() 
		{ 
			return View();
		}
	}
}
