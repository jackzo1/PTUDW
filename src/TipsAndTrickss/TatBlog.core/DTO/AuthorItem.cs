using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatBlog.core.Entities;

namespace TatBlog.core.DTO
{
    public class AuthorItem
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string ShortDescription { get; set; }
        public string Notes { get; set; }
        public string Email { get; set; }
        public string UrlSlug { get; set; }
        public string ImageUrl { get; set; }
        public int PostCount { get; set; }
        public bool Published { get; set; }
        public DateTime JoinedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int CategoryId { get; set; }
        public int AuthorId { get; set; }
        public Category Category { get; set; }
        public Author Author { get; set; }
        public IList<Tag> Tags { get; set; }
        public string CategoryName { get; set; }
    }
}
