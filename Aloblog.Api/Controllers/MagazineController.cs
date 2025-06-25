using Aloblog.Application.Common.ApiResult;
using Aloblog.Application.Dtos.Blog;
using Aloblog.Application.Interfaces;
using Aloblog.Domain.Entities.Blogs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Aloblog.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MagazineController(IUnitOfWork _unitOfWork, IFileService _fileService) : ControllerBase
{
    [HttpGet("GetMagazines")]
    public async Task<ActionResult<ApiResult<List<Magazine>>>> GetMagazines()
    {
        var result = await _unitOfWork.GenericRepository<Magazine>()
            .TableNoTracking.OrderByDescending(x => x.Date).ToListAsync();

        return Ok(new ApiResult<List<Magazine>>(result, "مجله‌ها با موفقیت دریافت شدند", ApiResultStatusCode.Success));
    }

    [HttpGet("GetMagazineById/{id}")]
    public async Task<ActionResult<ApiResult<Magazine>>> GetMagazineById(int id)
    {
        var result = await _unitOfWork.GenericRepository<Magazine>()
            .TableNoTracking.FirstOrDefaultAsync(x => x.Id == id);

        if (result == null)
            return NotFound(new ApiResult<Magazine>(null, "مجله یافت نشد", ApiResultStatusCode.NotFound));

        return Ok(new ApiResult<Magazine>(result, "مجله با موفقیت دریافت شد", ApiResultStatusCode.Success));
    }

    [HttpPost("CreateMagazine")]
    public async Task<ActionResult<ApiResult<Magazine>>> CreateMagazine([FromForm] InsertMagazineDto dto)
    {
        if (dto.ImageUrl == null || dto.ImageUrl.Length == 0)
            return BadRequest(new ApiResult<Magazine>(null, "تصویر الزامی است", ApiResultStatusCode.BadRequest));

        var imagePath = _fileService.UploadFile(dto.ImageUrl, "magazines");

        var entity = new Magazine
        {
            Title = dto.Title,
            Description = dto.Description,
            Alt = dto.Alt,
            ImageUrl = imagePath,
            Date = DateTime.Now // همونی که گفتی
        };

        await _unitOfWork.GenericRepository<Magazine>().AddAsync(entity, CancellationToken.None);

        return Ok(new ApiResult<Magazine>(entity, "مجله با موفقیت ایجاد شد", ApiResultStatusCode.Success));
    }

    [HttpPut("UpdateMagazine/{id}")]
    public async Task<ActionResult<ApiResult<Magazine>>> UpdateMagazine(int id, [FromForm] UpdateMagazineDto dto)
    {
        var existing = await _unitOfWork.GenericRepository<Magazine>().Table.FirstOrDefaultAsync(x => x.Id == id);
        if (existing == null)
            return NotFound(new ApiResult<Magazine>(null, "مجله یافت نشد", ApiResultStatusCode.NotFound));

        var imagePath = dto.ImageUrl != null ? _fileService.UploadFile(dto.ImageUrl, "magazines") : existing.ImageUrl;

        existing.Title = dto.Title;
        existing.Description = dto.Description;
        existing.Alt = dto.Alt;
        existing.ImageUrl = imagePath;

        await _unitOfWork.GenericRepository<Magazine>().UpdateAsync(existing, CancellationToken.None);

        return Ok(new ApiResult<Magazine>(existing, "مجله با موفقیت بروزرسانی شد", ApiResultStatusCode.Success));
    }

    [HttpDelete("DeleteMagazine/{id}")]
    public async Task<ActionResult<ApiResult<bool>>> DeleteMagazine(int id)
    {
        var entity = await _unitOfWork.GenericRepository<Magazine>().Table.FirstOrDefaultAsync(x => x.Id == id);
        if (entity == null)
            return NotFound(new ApiResult<bool>(false, "مجله یافت نشد", ApiResultStatusCode.NotFound));

        await _unitOfWork.GenericRepository<Magazine>().DeleteAsync(entity, CancellationToken.None);

        return Ok(new ApiResult<bool>(true, "مجله با موفقیت حذف شد", ApiResultStatusCode.Success));
    }
}
