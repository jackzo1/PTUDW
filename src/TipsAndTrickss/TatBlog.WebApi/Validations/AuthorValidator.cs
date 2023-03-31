using System;
using TatBlog.WebApi.Models;

namespace TatBlog.WebApi.Validations
{
    public class AuthorValidator : AuthorValidator<AuthorEditModel>
    {
        public AuthorValidator() 
        {
            //RuleFor(a => a.FullName)
            //    .NotEmpty()
            //    .WithMessage("Tên tác giả ko được để trốn")
            //    .MaximumLength(100)
            //    .WithMessage("Tên tác giả tối đa 100 ký tự");
            //RuleFor(a => a.UrlSlug)
            //    .NotEmpty()
            //    .WithMessage("UrlSlug ko được để trốn")
            //    .MaximumLength(100)
            //    .WithMessage("UrlSlug tối đa 100 ký tự");
            //RuleFor(a => a.JoinedDate)
            //    .GreaterThan(DateTime.MinValue)
            //    .WithMessage("Ngày tham gia không hợp lệ");
            //RuleFor(a => a.Email)
            //    .NotEmpty()
            //    .WithMessage("Email ko được để trốn")
            //    .MaximumLength(100)
            //    .WithMessage("Email tối đa 100 ký tự");
            //RuleFor(a => a.Notes)
            //    .MaximumLength(500)
            //    .WithMessage("Tên tác giả tối đa 500 ký tự");
        }
    }
}
