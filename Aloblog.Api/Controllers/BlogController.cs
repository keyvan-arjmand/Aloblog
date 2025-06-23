using Aloblog.Application.Common.ApiResult;
using Aloblog.Application.Dtos.Blog;
using Aloblog.Application.Interfaces;
using Aloblog.Domain.Entities.Blogs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Aloblog.Api.Controllers;

public class BlogController(IUnitOfWork _unitOfWork, IFileService _fileService) : BaseApiController
{
    [HttpGet("GetBlogs")]
    public async Task<ActionResult<ApiResult<PagedResult<Blog>>>> GetBlogs(int page = 0, int pageSize = 15,
        string? search = null)
    {
        if (page < 0) page = 0;
        if (pageSize <= 0) pageSize = 15;

        var query = _unitOfWork.GenericRepository<Blog>()
            .TableNoTracking;

        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(b => b.Title.Contains(search) || b.Description.Contains(search));
        }

        query = query.Include(x => x.BlogDetail);
        var totalCount = await query.CountAsync();

        var result = await query
            .OrderByDescending(b => b.Id)
            .Skip(page * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var pagedResult = new PagedResult<Blog>
        {
            Items = result,
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount
        };

        return Ok(new ApiResult<PagedResult<Blog>>(pagedResult, "بلاگ‌ها با موفقیت دریافت شدند",
            ApiResultStatusCode.Success));
    }

    [HttpGet("GetBlogById/{id}")]
    public async Task<ActionResult<ApiResult<Blog>>> GetBlogById(int id)
    {
        var blog = await _unitOfWork.GenericRepository<Blog>()
            .TableNoTracking
            .Include(x => x.BlogDetail)
            .FirstOrDefaultAsync(b => b.Id == id);

        if (blog == null)
        {
            return NotFound(new ApiResult("بلاگ مورد نظر یافت نشد", ApiResultStatusCode.NotFound));
        }

        return Ok(new ApiResult<Blog>(blog, "بلاگ با موفقیت دریافت شد", ApiResultStatusCode.Success));
    }

    [HttpPost("CreateBlog")]
    public async Task<ActionResult> CreateBlog([FromBody] InsertBlog dto)
    {
        var blogImg = _fileService.UploadFile(dto.ImageUrl, "Blogs");
        var blogEntity = new Blog
        {
            Alt = dto.Alt,
            Title = dto.Title,
            Description = dto.Description,
            ImageUrl = blogImg,
            Date = DateTime.Now,
        };
        await _unitOfWork.GenericRepository<Blog>().AddAsync(blogEntity, CancellationToken.None);
        var detailEntity = new BlogDetail
        {
            Date = DateTime.Now,
            BlogId = blogEntity.Id,
            HtmlDesc = dto.HtmlDesc,
            Slug = dto.Slug,
            Title = dto.DetailTitle,
        };
        await _unitOfWork.GenericRepository<BlogDetail>().AddAsync(detailEntity, CancellationToken.None);
        blogEntity.BlogDetailId = detailEntity.Id;
        await _unitOfWork.GenericRepository<Blog>().UpdateAsync(blogEntity, CancellationToken.None);
        return Ok();
    }
}