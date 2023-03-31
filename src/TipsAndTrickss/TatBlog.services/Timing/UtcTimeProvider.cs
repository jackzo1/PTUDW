using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TatBlog.services.Timing
{
	public class UtcTimeProvider : ITimeProvider
	{
		public DateTime Now => DateTime.UtcNow;

		public DateTime Today => DateTime.UtcNow.Date;
	}
}
