﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TatBlog.services.Timing
{
	public class LocalTimeProvider : ITimeProvider
	{
		public DateTime Now => DateTime.Now;

		public DateTime Today => DateTime.Now.Date;
	}
}
