using Microsoft.AspNetCore.Mvc;
using TatBlog.services.Blogs;

namespace TatBlog.WebApp.Components
{
    public class RandomPosts : ViewComponent
    {
        private readonly IBlogRepository _blogRepository;
        public RandomPosts(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var posts = await _blogRepository.GetPostRandomsAsync(4);
            return View(posts);
        }
    }
}
