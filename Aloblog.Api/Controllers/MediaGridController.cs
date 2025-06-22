using Aloblog.Application.Common.ApiResult;
using Aloblog.Application.Dtos.MediaGrid;
using Aloblog.Application.Interfaces;
using Aloblog.Domain.Entities.MainPages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Aloblog.Api.Controllers;

public class MediaGridController(IUnitOfWork _unitOfWork, IFileService _fileService) : BaseApiController
{
    [HttpGet("GetMediaGrids")]
    public async Task<ActionResult<ApiResult<List<MediaGrid>>>> GetMediaGrids()
    {
        var result = await _unitOfWork.GenericRepository<MediaGrid>().TableNoTracking
            .OrderBy(x => x.Priority).ToListAsync();

        return Ok(new ApiResult<List<MediaGrid>>(result, "مدیاها با موفقیت دریافت شدند", ApiResultStatusCode.Success));
    }

    [HttpGet("GetMediaGridById/{id}")]
    public async Task<ActionResult<ApiResult<MediaGrid>>> GetMediaGridById(int id)
    {
        var result = await _unitOfWork.GenericRepository<MediaGrid>().TableNoTracking
            .FirstOrDefaultAsync(x => x.Id == id);

        if (result == null)
            return NotFound(new ApiResult<MediaGrid>(null, "مدیا یافت نشد", ApiResultStatusCode.NotFound));

        return Ok(new ApiResult<MediaGrid>(result, "مدیا با موفقیت دریافت شد", ApiResultStatusCode.Success));
    }

    [HttpPost("CreateMediaGrid")]
    public async Task<ActionResult<ApiResult<MediaGrid>>> CreateMediaGrid([FromForm] InsertMediaGridDto dto)
    {
        if (dto.MediaUrl == null || dto.MediaUrl.Length == 0)
            return BadRequest(new ApiResult<MediaGrid>(null, "فایل مدیا الزامی است", ApiResultStatusCode.BadRequest));

        var mediaPath = _fileService.UploadFile(dto.MediaUrl, "media");
        var posterPath = dto.Poster != null ? _fileService.UploadFile(dto.Poster, "media") : null;

        var entity = new MediaGrid
        {
            MediaUrl = mediaPath,
            Poster = posterPath,
            Alt = dto.Alt,
            Priority = dto.Priority
        };

        await _unitOfWork.GenericRepository<MediaGrid>().AddAsync(entity, CancellationToken.None);

        return Ok(new ApiResult<MediaGrid>(entity, "مدیا با موفقیت ایجاد شد", ApiResultStatusCode.Success));
    }

    [HttpPut("UpdateMediaGrid/{id}")]
    public async Task<ActionResult<ApiResult<MediaGrid>>> UpdateMediaGrid(int id, [FromForm] UpdateMediaGridDto dto)
    {
        var existing = await _unitOfWork.GenericRepository<MediaGrid>().Table.FirstOrDefaultAsync(x => x.Id == id);
        if (existing == null)
            return NotFound(new ApiResult<MediaGrid>(null, "مدیا یافت نشد", ApiResultStatusCode.NotFound));

        var mediaPath = dto.MediaUrl != null ? _fileService.UploadFile(dto.MediaUrl, "media") : existing.MediaUrl;
        var posterPath = dto.Poster != null ? _fileService.UploadFile(dto.Poster, "media") : existing.Poster;

        existing.MediaUrl = mediaPath;
        existing.Poster = posterPath;
        existing.Alt = dto.Alt;
        existing.Priority = dto.Priority;

        await _unitOfWork.GenericRepository<MediaGrid>().UpdateAsync(existing, CancellationToken.None);

        return Ok(new ApiResult<MediaGrid>(existing, "مدیا با موفقیت بروزرسانی شد", ApiResultStatusCode.Success));
    }

    [HttpDelete("DeleteMediaGrid/{id}")]
    public async Task<ActionResult<ApiResult<bool>>> DeleteMediaGrid(int id)
    {
        var entity = await _unitOfWork.GenericRepository<MediaGrid>().Table.FirstOrDefaultAsync(x => x.Id == id);
        if (entity == null)
            return NotFound(new ApiResult<bool>(false, "مدیا یافت نشد", ApiResultStatusCode.NotFound));

        await _unitOfWork.GenericRepository<MediaGrid>().DeleteAsync(entity, CancellationToken.None);

        return Ok(new ApiResult<bool>(true, "مدیا با موفقیت حذف شد", ApiResultStatusCode.Success));
    }
}
