using MapsterMapper;
using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;
using TatBlog.core.Collections;
using TatBlog.core.DTO;
using TatBlog.services.Blogs;
using TatBlog.services.Blogs.Authors;
using TatBlog.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using TatBlog.core.Entities;
using TatBlog.services.Media;
using System.ComponentModel.DataAnnotations;
using System.Threading.Channels;
using TatBlog.WebApi.Extensions;
using Mapster;
using TatBlog.WebApi.Filters;


namespace TatBlog.WebApi.Endpoints
{
    public static class AuthorEndpoints
    {
        public static WebApplication MapAuthorEndpoints(
            this WebApplication app)
        {
            var routeGroupBuider = app.MapGroup("/api/authors");
            routeGroupBuider.MapGet("/", GetAuthors)
                .WithName("GetAuthors")
                .Produces<PaginationResult<AuthorItem>>();

            routeGroupBuider.MapGet(
                "/{slug:regex(^[a-z0-9 -]+$)}/post",
                GetPostsByAuthorSlug)
               .WithName("GetPostsByAuthorSlug")
               .Produces<PaginationResult<PostDto>>();
             
            routeGroupBuider.MapGet("/{id:int}", GetAuthorDetails)
             .WithName("GetAuthorDetails")
             .Produces(204)
             .Produces(400)
             .Produces(409);
            routeGroupBuider.MapPost("/", AddAuthor)
            .WithName("AddNewAuthor")
             //.AddEndpointFilter<ValidationFilter<AuthorEditModel>>()
             .Produces(201)
             .Produces(400)
             .Produces(409);
            routeGroupBuider.MapPost("/{id:int}", SetAuthorPicture)
             .WithName("SetAuthorPicture")
             .Accepts<IFormFile>("multipart/form-data")
             .Produces<string>()
             .Produces(409);
            routeGroupBuider.MapPut("/{id:int}", UpdateAuthor)
              .WithName("UpdateAuthor")
              //.AddEndpointFilter<ValidationFilter<AuthorEditModel>>()
              .Produces(204)
              .Produces(400)
              .Produces(409);
            routeGroupBuider.MapDelete("/{id:int}", DeleteAuthor)
               .WithName("DeleteAuthor")
               .Produces(204)
               .Produces(404);
            return app;
        }
        private static async Task<IResult> GetAuthors(
            [AsParameters] AuthorFilterModel model,
            IAuthorRepository authorReposity)
        {
            var authorsList = await authorReposity.GetPagedAuthorsAsync(model, model.Name);
            var paginationResult = new PaginationResult<AuthorItem>(authorsList);
            return Results.Ok(paginationResult);
        }


        private static async Task<IResult> GetAuthorDetails(
            int id,
            IAuthorRepository authorRepository,
            IMapper mapper)
        {
            var author = await authorRepository.GetCachedAuthorByIdAsync(id);
            return author == null
                ? Results.NotFound($"Không tìm thấy tác giả có mã số {id}")
                : Results.Ok(mapper.Map<AuthorItem>(author));
        }


        private static async Task<IResult> GetPostsByAuthorId(
            int id,
            [AsParameters] PagingModel pagingModel,
            IBlogRepository blogRepository)
        {
            var postQuery = new PostQuery()
            {
                AuthorId = id,
                PublishedOnly = true
            };
            var postsList = await blogRepository.GetPagedPostsAsync(postQuery, pagingModel,posts => posts.ProjectToType<PostDto>());
            var paginationResult = new PaginationResult<PostDto>(postsList);
            return Results.Ok(paginationResult);
        }


        private static async Task<IResult> GetPostsByAuthorSlug(
            [FromRoute] string slug,
            [AsParameters] PagingModel pagingModel,
            IBlogRepository blogRepository)
        {
            var postQuery = new PostQuery()
            {
                AuthorSlug = slug,
                PublishedOnly = true
            };
            var postsList = await blogRepository.GetPagedPostsAsync(postQuery, pagingModel, posts => posts.ProjectToType<PostDto>());
            var paginationResult = new PaginationResult<PostDto>(postsList);

            return Results.Ok(paginationResult);
        }


        private static async Task<IResult> AddAuthor(
            AuthorEditModel model,
            IValidator<AuthorEditModel> validator,
            IAuthorRepository authorRepository,
            IMapper mapper)
        {
            var validationResult = await validator.ValidateAsync(model);
            if (!validationResult.IsValid)
            {
                return Results.BadRequest(
                validationResult.Errors.ToResponse());

            }
            if (await authorRepository
                .IsAuthorSlugExistedAsync(0, model.UrlSlug))
            {
                return Results.Conflict(
              $"slug '{model.UrlSlug}' đã được sử dụng");
            }
            var author = mapper.Map<Author>(model);
            await authorRepository.AddOrUpdateAsync(author);
            return Results.CreatedAtRoute(
            "GetAuthorById", new { author.Id },
            mapper.Map<AuthorItem>(author));
        }
        private static async Task<IResult> SetAuthorPicture(
            int id,IFormFile imageFile,
            IAuthorRepository authorRepository,
            IMediaManager mediaManager)
        {
            var imageUrl = await mediaManager.SaveFileAsync(
                imageFile.OpenReadStream(),
                imageFile.FileName, imageFile.ContentType);
            if (string.IsNullOrWhiteSpace(imageUrl))
            {
                return Results.BadRequest("Không lưu được tập tin");
            } 
            await authorRepository.SetImageUrlAsync(id, imageUrl);
            return Results.Ok(imageUrl);
        }
        private static async Task<IResult> UpdateAuthor(
            int id, AuthorEditModel model,
            IValidator<AuthorEditModel> validator,
            IAuthorRepository authorRepository,
            IMapper mapper)
        {
            var validationResult = await validator
                .ValidateAsync(model);
            if (!validationResult.IsValid)
            {
                return Results.BadRequest(
                    validationResult.Errors.ToResponse());
            }
            if (await authorRepository
                .IsAuthorSlugExistedAsync(id, model.UrlSlug))
            {
                return Results.Conflict(
                    $"Slug '{model.UrlSlug}' đã được sử dụng");
            }
            var author = mapper.Map<Author>(model);
            author.Id = id;
            return await authorRepository.AddOrUpdateAsync(author)
                ? Results.NoContent()
                : Results.NotFound();
        }
        private static async Task<IResult> DeleteAuthor(
            int id, IAuthorRepository authorRepository)
        {
            return await authorRepository.DeleteAuthorAsync(id)
                ? Results.NoContent()
                : Results.NotFound($"Could not find author with id = {id}");
        }
    }
}

