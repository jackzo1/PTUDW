using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TatBlog.services.Timing
{
	public interface ITimeProvider
	{
		DateTime Now { get; }

		DateTime Today { get; }
	}
}
