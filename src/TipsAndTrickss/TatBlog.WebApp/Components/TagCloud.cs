using Microsoft.AspNetCore.Mvc;
using TatBlog.services.Blogs;

namespace TatBlog.WebApp.Components
{
    public class TagCloud : ViewComponent
    {
        private readonly IBlogRepository _blogRepository;

        public TagCloud(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var tagList = await _blogRepository.GetAllTagAsync();
            return View(tagList);
        }
    }
}
