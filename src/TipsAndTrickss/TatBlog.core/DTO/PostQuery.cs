﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TatBlog.core.DTO
{
    public class PostQuery
    {
        public int? AuthorId { get; set; }
        public string TitleSlug { get; set; }
        public int? CategoryId { get; set; }
        public string TagSlug { get; set; }
        public string AuthorSlug { get; set; }
        public string CategorySlug { get; set; }
        public int? Month { get; set; }
        public int? Year { get; set; }
        public int? Day { get; set; }
        public string UrlSlug { get; set; }
        public string Tag { get; set; }
        public bool PublishedOnly { get; set; }
        public bool NotPublished { get; set; }
        public string Keyword { get; set; }
    }
}
