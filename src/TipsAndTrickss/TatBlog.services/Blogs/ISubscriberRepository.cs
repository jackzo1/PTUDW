using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatBlog.core.Contracts;
using TatBlog.core.Entities;

namespace TatBlog.services.Blogs
{
    public interface ISubscriberRepository
    {
        Task<bool> SubscribeAsync(string email, CancellationToken cancellationToken = default);
        Task<bool> UnsubscribeAsync(string email, string reason, bool voluntary = false, CancellationToken cancellationToken = default);
        Task<bool> BlockSubscriberAsync(int id, string reason, string notes, CancellationToken cancellationToken = default);
        Task<bool> DeleteSubscriberAsync(int id, CancellationToken cancellationToken = default);
        Task<Subscriber> GetSubscriberByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<Subscriber> GetSubscriberByEmailAsync(string email, CancellationToken cancellationToken = default);
        Task<IPagedList<Subscriber>> SearchSubscribersAsync(IPagingParams pagingParams, string keyword, bool unsubscribed, bool involuntary, CancellationToken cancellationToken = default);
    }
}
