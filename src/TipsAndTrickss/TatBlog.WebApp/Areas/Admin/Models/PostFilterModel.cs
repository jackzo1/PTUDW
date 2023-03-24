﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.Globalization;

namespace TatBlog.WebApp.Areas.Admin.Models
{
	public class PostFilterModel 
	{
		//private object _blogRepository;

		[DisplayName("Từ Khóa")]
		public string KeyWord { get; set; }

		[DisplayName("Tác giả")]
		public int? AuthorId { get; set; }

		[DisplayName("Chủ đề")]
		public int? CategoryId { get; set; }

		[DisplayName("Năm")]
		public int? Year { get; set; }

		[DisplayName("Tháng")]
		public int? Month { get; set; }

		public IEnumerable<SelectListItem> AuthorList { get; set; } = default;
		public IEnumerable<SelectListItem> CategoryList { get; set; } = default;
		public IEnumerable<SelectListItem> MonthList { get; set; } = default;
		public PostFilterModel() 
		{
			MonthList = Enumerable.Range(1,12)
				.Select(m => new SelectListItem()
				{
					Value = m.ToString(),
					Text = CultureInfo.CurrentCulture
					.DateTimeFormat.GetMonthName(m)
				})
				.ToList();
		}
		
	}
}
