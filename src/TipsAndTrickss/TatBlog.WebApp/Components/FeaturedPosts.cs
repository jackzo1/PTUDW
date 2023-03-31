using Microsoft.AspNetCore.Mvc;
using TatBlog.services.Blogs;

namespace TatBlog.WebApp.Components
{
    public class FeaturedPost : ViewComponent
    {
        private readonly IBlogRepository _blogRepository;

        public FeaturedPost(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var posts = await _blogRepository.GetFeaturedPostToTakeNumber(2);
            return View(posts);
        }
    }
}
