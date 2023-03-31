using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TatBlog.core.DTO;
using TatBlog.core.Entities;
using TatBlog.services.Blogs;
using TatBlog.services.Blogs.Authors;
using TatBlog.services.Media;
using TatBlog.WebApp.Areas.Admin.Models;

namespace TatBlog.WebApp.Areas.Admin.Controllers
{

	public class PostsController : Controller
	{
		private readonly IBlogRepository _blogRepository;
		private readonly IAuthorRepository _authorRepository;
		private readonly IMediaManager _mediaManager;
		private readonly IMapper _mapper;
		public PostsController(IBlogRepository blogRepository,IAuthorRepository authorRepository, IMapper mapper, IMediaManager mediaManager)
		{
			_blogRepository = blogRepository;
			_authorRepository = authorRepository;
			_mapper = mapper;
			_mediaManager = mediaManager;
		}
		private async Task PopulatePostFilterModelAsync(PostFilterModel model)
		{
			var authors = await _authorRepository.GetAuthorsAsync();
			var categories = await _blogRepository.GetCategoriesAsync();
			model.AuthorList = authors.Select(a => new SelectListItem()
			{
				//Chính là Bug =>> ở dòng này///
				Text = a.FullName,
				////Đây/////////////////////
				Value = a.Id.ToString()
			});
			model.CategoryList = categories.Select(a => new SelectListItem()
			{
				Text = a.Name,
				Value = a.Id.ToString()
			});
		}
		public async Task<IActionResult> Index(PostFilterModel model)
		{
			var postQuery = new PostQuery()
			{
				Keyword = model.Keyword,
				CategoryId = model.CategoryId,
				AuthorId = model.AuthorId,
				Year = model.Year,
				Month = model.Month
			};
			//var postQuery = _mapper.Map<PostQuery>(model);

			ViewBag.PostsList = await _blogRepository
				.GetPagedPostsAsync(postQuery, 1, 10);
			await PopulatePostFilterModelAsync(model);
			return View(model);
		}


		[HttpGet]
		public async Task<IActionResult> Edit(int id = 0)
		{
			// ID = 0 <=> Thêm bài viết mới
			// ID > @ : Đọc dữ liệu của bài viết từ CSDL
			var post = id > 0
				? await _blogRepository.GetPostByIdAsync(id, true)
				: null;
			// Tạo view model từ dữ liệu của bài viết
			var model = post == null
					? new PostEditModel()
					: _mapper.Map<PostEditModel>(post);
			// Gán các giá trị khác cho view model
			await PopulatePostFilterModelAsync(model);
			return View(model);
		}

		private async Task PopulatePostFilterModelAsync(PostEditModel model)
		{
			var authors = await _authorRepository.GetAuthorsAsync();
			var categories = await _blogRepository.GetCategoriesAsync();
			var publishedlist = new List<bool>() { true, false };
			//model.PublishedList = publishedlist.Select(a => new SelectListItem()
			//{
			//    Text = a.ToString(),
			//    Value = a.ToString(),
			//});
			model.AuthorList = authors.Select(a => new SelectListItem()
			{
				Text = a.FullName,
				Value = a.Id.ToString(),
			});
			model.CategoryList = categories.Select(c => new SelectListItem()
			{
				Text = c.Name,
				Value = c.Id.ToString(),
			});
		}
		[HttpPost]
		public async Task<IActionResult> VerifyPostSlug(int id, string urlslug)
		{
			var slugExisted = await _blogRepository.IsPostSlugExistedAsync(id, urlslug);
			return slugExisted
			? Json($"slug '{urlslug}' Đã Được Sử Dụng")
			: Json(true);

		}
		[HttpPost]
		public async Task<IActionResult> Edit(PostEditModel model)
		{
			if (!ModelState.IsValid)
			{
				await PopulatePostFilterModelAsync(model);
				return View(model);
			}
			var post = model.Id > 0
			? await _blogRepository.GetPostByIdAsync(model.Id) : null;
			if (post == null)
			{
				post = _mapper.Map<Post>(model);
				post.Id = 0;
				post.PostedDate = DateTime.Now;
			}
			else
			{
				_mapper.Map(model, post);
				post.Category = null;
				post.ModifiedDate = DateTime.Now;
			}
			if (model.ImageFile?.Length > 0)
			{
				var newImagePath = await _mediaManager.SaveFileAsync(
					model.ImageFile.OpenReadStream(),
					model.ImageFile.FileName,
					model.ImageFile.ContentType);
				if (!string.IsNullOrWhiteSpace(newImagePath))
				{
					await _mediaManager.DeleteFileAsync(post.ImageUrl);
					post.ImageUrl = newImagePath;
				}
			}
			await _blogRepository.CreateOrUpdatePostAsync(
				post, model.GetSelectedTags());
			return RedirectToAction(nameof(Index));
		}



	}
}

