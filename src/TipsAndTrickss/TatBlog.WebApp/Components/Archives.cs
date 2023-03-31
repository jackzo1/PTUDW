using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Buffers;
using TatBlog.services.Blogs;

namespace TatBlog.WebApp.Components
{
    public class Archives : ViewComponent
    {
        private readonly IBlogRepository _blogRepository;
        public Archives(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var month = await _blogRepository.GetAllMonthOfPosts();
            return View(month);
        }
    }
}
