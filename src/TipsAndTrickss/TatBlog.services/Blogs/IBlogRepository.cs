using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatBlog.core.Contracts;
using TatBlog.core.DTO;
using TatBlog.core.Entities;

namespace TatBlog.services.Blogs
{
    public interface IBlogRepository
    {

        Task<IPagedList<Post>> GetPostAsync(
            PostQuery condition,
            int pageNumber = 1,
            int pageSize = 10,
            CancellationToken cancellationToken = default);


        public Task<IList<Post>> GetPopularArticleAsync(
            int numPosts,
            CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }


        public Task<bool> IPostSlugExistedAsync(
            int postId,
            string slug,
            CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }


        public Task IncreaseViewCountAsync(
            int postId,
            CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }


        public Task<IList<CategoryItem>> GetCategoriesAsync(
            bool showOnMenu = false,
            CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }


        public Task<IPagedList<TagItem>> GetPagedTagsAsync(
            IPagingParams pagingParams,
            CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
        //Tìm Tag có tên định danh là Slug
        Task<Tag> GetTagBySlugAsync(string slug, CancellationToken cancellationToken = default);


        //xóa
        Task DeleteTagByIdAsync(int? id, CancellationToken cancellationToken = default);
    }

}
