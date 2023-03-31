using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatBlog.core.Contracts;

namespace TatBlog.core.Collections
{
	public class PaginationResult<T>
	{
		public IEnumerable<T> Items { get; set; }

		public PagingMetadata Metadata { get; set; }

		public PaginationResult(IPagedList<T> pagedList)
		{
			Items = pagedList;
			Metadata = new PagingMetadata(pagedList);
		}
	}
}
