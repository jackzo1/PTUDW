using Microsoft.AspNetCore.Mvc;
using TatBlog.services.Blogs.Authors;

namespace TatBlog.WebApp.Components
{
    public class BestAuthors : ViewComponent
    {
        private readonly IAuthorRepository _authorRepository;
        public BestAuthors(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var author = await _authorRepository.GetFourPopulationAuthor();
            return View(author);
        }
    }
}
