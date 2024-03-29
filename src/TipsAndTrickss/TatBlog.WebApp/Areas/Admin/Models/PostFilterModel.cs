﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;

namespace TatBlog.WebApp.Areas.Admin.Models
{
    public class PostFilterModel
    {
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
        public bool Published { get; set; }
        public IEnumerable<SelectListItem> AuthorList { get; set; }
        public IEnumerable<SelectListItem> PublishedList { get; set; }
        public IEnumerable<SelectListItem> CategoryList { get; set; }

        public IEnumerable<SelectListItem> MonthList { get; set; }
        public string Keyword { get; internal set; }

        public PostFilterModel()
        {
            MonthList = Enumerable.Range(1, 12).Select(m => new SelectListItem()
            {
                Value = m.ToString(),
                Text = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(m)
            }).ToList();

        }



    }
}
