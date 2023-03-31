using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace TatBlog.WebApp.Areas.Admin.Models
{
	public class PostEditModel
	{
		public int Id { get; set; }
		[DisplayName("Tieu de")]
		[Required(ErrorMessage = "Tiêu đề không được đề trống")]
		[MaxLength(500, ErrorMessage = "Tiêu đề tối đa 500 ký tự")]
		public string? Title { get; set; }
		[DisplayName("Giới thiệu")]
		[Required(ErrorMessage = "Giới thiệu không được đề trống")]
		[MaxLength(2000, ErrorMessage = "Giới thiệu tối đa 2000 ký tự")]
		public string? ShortDescription { get; set; }
		[DisplayName("Nội dung")]
		[Required(ErrorMessage = "Nội dung không được đề trống")]
		[MaxLength(5000, ErrorMessage = "Nội dung tối đa 5000 ký tự")]
		public string? Description { get; set; }
		[DisplayName("MetaData")]
		[Required(ErrorMessage = "Metadata không được đề trống")]
		[MaxLength(1000, ErrorMessage = "Metadata tối đa 1000 ký tự")]
		public string? Meta { get; set; }
		[DisplayName("Slug")]
		[Remote("VerifyPostSlug", "Posts", "Admin", HttpMethod = "POST", AdditionalFields = "Id")]
		[Required(ErrorMessage = "Slug không được đề trống")]
		[MaxLength(200, ErrorMessage = "Slug tối đa 200 ký tự")]
		public string? UrlSlug { get; set; }
		[DisplayName("Chọn hình ảnh")]
		public IFormFile? ImageFile { get; set; }
		[DisplayName("Hình ảnh hiện tại")]
		public string? ImageUrl { get; set; }
		[DisplayName("Xuất bản ngay")]
		public bool Published { get; set; }
		[DisplayName("Chủ đề")]
		[Required(ErrorMessage = "Bạn chưa chọn chủ đề")]
		public int CategoryId { get; set; }
		[DisplayName("Tác giả")]
		[Required(ErrorMessage = "Bạn chưa chọn tác giả")]
		public int AuthorId { get; set; }
		[DisplayName("Từ khóa(mỗi từ 1 dòng)")]
		[Required(ErrorMessage = "Bạn chưa nhập tên thẻ")]
		public string? SelectedTags { get; set; }

		public IEnumerable<SelectListItem>? AuthorList { get; set; }
		public IEnumerable<SelectListItem>? CategoryList { get; set; }
		public List<string> GetSelectedTags()
		{
			return (SelectedTags ?? "").Split(new[] { ',', ';', '\r', '\n' },
				StringSplitOptions.RemoveEmptyEntries).ToList();
		}
	}
}
