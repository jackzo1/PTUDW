using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatBlog.core.Entities;
using TatBlog.core.DTO;
using TatBlog.core.Contracts;

namespace TatBlog.services.Blogs
{
    public interface IBlogRepository
    {
        Task<Post> GetPostAsync(
            int year,
            int month,
            string slug,
            CancellationToken cancellationToken = default);
        Task<IPagedList<Post>> GetPagedPostsAsync(
           PostQuery condition,
           int pageNumber = 1,
           int pageSize = 10,
           CancellationToken cancellationToken = default);
        public Task<IList<Post>> GetPopularArticleAsync(
            int numPosts,
            CancellationToken cancellationToken = default);
        public Task<bool> IPostSlugExistedAsync(
            int postId,
            string slug,
            CancellationToken cancellationToken = default);
        public Task IncreaseViewCountAsync(
            int postId,
            CancellationToken cancellationToken = default);
        //public Task<IList<Author>> GetAuthorsAsync(
        //    CancellationToken cancellationToken = default);
        public Task<Category> GetCategoryByIdAsync(
            int id,
            CancellationToken cancellationToken = default);
        public Task<IList<CategoryItem>> GetCategoriesAsync(
         bool showOnMenu = false,
         CancellationToken cancellationToken = default);
        public Task<IPagedList<TagItem>> GetPagedTagsAsync(
            IPagingParams pagingParams,
            CancellationToken cancellationToken = default);
        public Task<Post> GetPostByIdAsync(
           int id,
           bool includeDetails = false,
           CancellationToken cancellationToken = default);
        public Task<IList<Post>> GetRandomPostsAsync(
            int randomOfPosts,
            CancellationToken cancellationToken = default);
        //public Task<IList<MonthlyPostCountItem>> CountMonthlyPostsAsync(
        //int numMonths,
        //CancellationToken cancellationToken = default);
        public Task<bool> DeleteTagByNameAsync(
            int id,
            CancellationToken cancellationToken = default);
        public Task<Tag> GetTagBySlugAsync(
            string slug,
            CancellationToken cancellationToken = default);
        public Task<Tag> DeleteTagByIdAsync(
            int? id,
            CancellationToken cancellationToken = default);
        public Task CreateOrUpdatePostAsync(
            object post,
            List<string> list);
        public Task<bool> IsCategorySlugExistedAsync(
            string slug,
            CancellationToken cancellationToken = default);
        public Task<bool> IsPostSlugExistedAsync(
            int id,
            string slug,
            CancellationToken cancellationToken = default);
        public Task GetPostAsync(PostQuery postQuery);
        Task<IList<Post>> GetFeaturedPostToTakeNumber(
            int number, CancellationToken cancellationToken = default);
        public Task<IDictionary<int, CountYear>>
            GetAllMonthOfPosts(CancellationToken cancellationToken = default);
        public Task<IList<Tag>>
            GetAllTagAsync(CancellationToken CancellationToken = default);
        public Task<IList<Post>> GetPostRandomsAsync(
            int numPosts,
            CancellationToken cancellationToken = default);
    }
}
