using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatBlog.core.Contracts;
using TatBlog.core.DTO;
using TatBlog.core.Entities;

namespace TatBlog.services.Blogs.Authors
{
    public interface IAuthorRepository
    {
        Task<Author> GetAuthorBySlugAsync(
            string slug,
            CancellationToken cancellationToken = default);

        Task<Author> GetCachedAuthorBySlugAsync(
            string slug, CancellationToken cancellationToken = default);

        Task<Author> GetAuthorByIdAsync(int authorId);

        Task<Author> GetCachedAuthorByIdAsync(int authorId);

        Task<IList<AuthorItem>> GetAuthorsAsync(
            CancellationToken cancellationToken = default);

        Task<IPagedList<AuthorItem>> GetPagedAuthorsAsync(
            IPagingParams pagingParams,
            string name = null,
            CancellationToken cancellationToken = default);

        Task<IPagedList<T>> GetPagedAuthorsAsync<T>(
            Func<IQueryable<Author>, IQueryable<T>> mapper,
            IPagingParams pagingParams,
            string name = null,
            CancellationToken cancellationToken = default);

        Task<bool> AddOrUpdateAsync(
            Author author,
            CancellationToken cancellationToken = default);

        Task<bool> DeleteAuthorAsync(
            int authorId,
            CancellationToken cancellationToken = default);

        Task<bool> IsAuthorSlugExistedAsync(
            int authorId, string slug,
            CancellationToken cancellationToken = default);

        Task<bool> SetImageUrlAsync(
            int authorId, string imageUrl,
            CancellationToken cancellationToken = default);
    }
}
