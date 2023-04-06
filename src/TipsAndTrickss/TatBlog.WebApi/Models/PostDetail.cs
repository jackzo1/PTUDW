﻿using System.Collections.Generic;
using System;

namespace TatBlog.WebApi.Models
{
	public class PostDetail
	{
		public int Id { get; set; }
		public string? Title { get; set; }
		public string? ShortDescription { get; set; }
		public string? ImageUrl { get; set; }
		public string? UrlSlug { get; set; }
		public int ViewCount { get; set; }
		public DateTime PostedDate { get; set; }
		public DateTime? ModifiedDate { get; set; }
		public CategoryDto Category { get; set; }
		public AuthorDto Author { get; set; }
		public IList<TagDto> Tags { get; set; }
	}
}
