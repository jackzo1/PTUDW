using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TatBlog.services.Blogs
{
	public interface IMailservice
	{
		Task SendEmailMessage(string email, CancellationToken cancellationToken);
	}
}
