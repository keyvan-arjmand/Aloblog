using Aloblog.Application.Common.ApiResult;
using Aloblog.Application.Dtos.Banners;
using Aloblog.Application.Dtos.Users;
using Aloblog.Application.Interfaces;
using Aloblog.Domain.Entities.Banners;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Aloblog.Api.Controllers;

public class BannerController(IUnitOfWork _unitOfWork, IFileService _fileService) : BaseApiController
{
    [HttpGet("GetBanners")]
    public async Task<ActionResult<ApiResult<List<Banner>>>> GetBanners()
    {
        var result = await _unitOfWork.GenericRepository<Banner>().TableNoTracking.ToListAsync();
        return Ok(new ApiResult<List<Banner>>(result, "بنرها با موفقیت دریافت شدند", ApiResultStatusCode.Success));
    }

    [HttpGet("GetBannerById/{id}")]
    public async Task<ActionResult<ApiResult<Banner>>> GetBannerById(int id)
    {
        var result = await _unitOfWork.GenericRepository<Banner>().TableNoTracking.FirstOrDefaultAsync(x => x.Id == id);

        if (result == null)
            return NotFound(new ApiResult<Banner>(null, "بنر یافت نشد", ApiResultStatusCode.NotFound));

        return Ok(new ApiResult<Banner>(result, "بنر با موفقیت دریافت شد", ApiResultStatusCode.Success));
    }

    [HttpPost("CreateBanner")]
    public async Task<ActionResult<ApiResult<Banner>>> CreateBanner([FromForm] InsertBannerDto banner)
    {
        if (banner.ImageUrl == null || banner.ImageUrl.Length == 0)
        {
            return BadRequest(new ApiResult<Banner>(null, "تصویر الزامی است", ApiResultStatusCode.BadRequest));
        }

        var imagePath = _fileService.UploadFile(banner.ImageUrl, "banners");

        var entity = new Banner
        {
            ImageUrl = imagePath,
            Alt = banner.Alt,
            Priority = banner.Priority,
        };

        await _unitOfWork.GenericRepository<Banner>().AddAsync(entity, CancellationToken.None);

        return Ok(new ApiResult<Banner>(entity, "بنر با موفقیت ایجاد شد", ApiResultStatusCode.Success));
    }

    [HttpPut("UpdateBanner/{id}")]
    public async Task<ActionResult<ApiResult<Banner>>> UpdateBanner(int id, [FromForm] UpdateBannerDto banner)
    {
        var imagePath = _fileService.UploadFile(banner.ImageUrl, "banners");

        var existing = await _unitOfWork.GenericRepository<Banner>().Table.FirstOrDefaultAsync(x => x.Id == id);
        if (existing == null)
            return NotFound(new ApiResult<Banner>(null, "بنر یافت نشد", ApiResultStatusCode.NotFound));

        existing.ImageUrl = !string.IsNullOrEmpty(imagePath) ? imagePath : existing.ImageUrl;
        existing.Alt = banner.Alt;
        existing.Priority = banner.Priority;

        await _unitOfWork.GenericRepository<Banner>().UpdateAsync(existing, CancellationToken.None);

        return Ok(new ApiResult<Banner>(existing, "بنر با موفقیت بروزرسانی شد", ApiResultStatusCode.Success));
    }

    [HttpDelete("DeleteBanner/{id}")]
    public async Task<ActionResult<ApiResult<bool>>> DeleteBanner(int id)
    {
        var banner = await _unitOfWork.GenericRepository<Banner>().Table.FirstOrDefaultAsync(x => x.Id == id);
        if (banner == null)
            return NotFound(new ApiResult<bool>(false, "بنر یافت نشد", ApiResultStatusCode.NotFound));

        await _unitOfWork.GenericRepository<Banner>().DeleteAsync(banner, CancellationToken.None);

        return Ok(new ApiResult<bool>(true, "بنر با موفقیت حذف شد", ApiResultStatusCode.Success));
    }
}