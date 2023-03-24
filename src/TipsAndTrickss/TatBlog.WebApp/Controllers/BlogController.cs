﻿using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Mvc;
using TatBlog.services.Blogs;
using TatBlog.core.DTO;

namespace TatBlog.WebApp.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogRepository _blogRepository;

        public BlogController(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }


        // Action này xử lý HTTP request đến trang chủ của
        // ứng dụng web hoặc tìm kiếm bài viết theo từ khóa
        public async Task<IActionResult> Index(
            [FromQuery(Name = "k")] string keyword = null,
            [FromQuery(Name = "p")] int pageNumber = 1,
            [FromQuery(Name = "ps")] int pageSize = 2)
        {
            //Tạo oject chứa điều kiện truy vấn
            var postQuery = new PostQuery()
            {
                //Chỉ lấy những bài viết Published
                PublishedOnly = true,
                KeyWord = keyword
            };

            // Truy vấn các bài viết theo điều kiện đã tạo
            var postList = await _blogRepository.GetPagedPostsAsync(postQuery, pageNumber, pageSize);

            // Lưu lại đk truy vấn để hiển thị trong View
            ViewBag.PostQuery = postQuery;

            return View(postList);
        }

        public IActionResult About()
            => View();

        public IActionResult Contact()
            => View();

        public IActionResult Rss()
            => Content("Nội dung sẽ được cập nhật");

        public IActionResult Error()
        {
            return View();
        }

    }
}
