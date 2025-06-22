using Aloblog.Application.Common.ApiResult;
using Aloblog.Application.Dtos.Banners;
using Aloblog.Application.Interfaces;
using Aloblog.Domain.Entities.Banners;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Aloblog.Api.Controllers;

public class MainBannerController(IUnitOfWork _unitOfWork, IFileService _fileService) : BaseApiController
{
    [HttpGet("GetMainBanners")]
    public async Task<ActionResult<ApiResult<List<MainBanner>>>> GetMainBanners()
    {
        var result = await _unitOfWork.GenericRepository<MainBanner>().TableNoTracking.ToListAsync();
        return Ok(new ApiResult<List<MainBanner>>(result, "بنرها با موفقیت دریافت شدند",
            ApiResultStatusCode.Success));
    }

    [HttpGet("GetMainBannerById/{id}")]
    public async Task<ActionResult<ApiResult<MainBanner>>> GetMainBannerById(int id)
    {
        var result = await _unitOfWork.GenericRepository<MainBanner>().TableNoTracking
            .FirstOrDefaultAsync(x => x.Id == id);

        if (result == null)
            return NotFound(new ApiResult<MainBanner>(null, "بنر یافت نشد", ApiResultStatusCode.NotFound));

        return Ok(new ApiResult<MainBanner>(result, "بنر با موفقیت دریافت شد", ApiResultStatusCode.Success));
    }

    [HttpPost("CreateMainBanner")]
    public async Task<ActionResult<ApiResult<MainBanner>>> CreateMainBanner(
        [FromForm] InsertMainBanner MainBanner)
    {
        if (MainBanner.ImageUrl == null || MainBanner.ImageUrl.Length == 0)
        {
            return BadRequest(new ApiResult<MainBanner>(null, "تصویر الزامی است", ApiResultStatusCode.BadRequest));
        }

        var imagePath = _fileService.UploadFile(MainBanner.ImageUrl, "MainBanners");

        var entity = new MainBanner
        {
            ImageUrl = imagePath,
            Alt = MainBanner.Alt,
        };

        await _unitOfWork.GenericRepository<MainBanner>().AddAsync(entity, CancellationToken.None);

        return Ok(new ApiResult<MainBanner>(entity, "بنر با موفقیت ایجاد شد", ApiResultStatusCode.Success));
    }

    [HttpPut("UpdateMainBanner/{id}")]
    public async Task<ActionResult<ApiResult<MainBanner>>> UpdateMainBanner(int id,
        [FromForm] UpdateMainBanner MainBanner)
    {
        var imagePath = _fileService.UploadFile(MainBanner.ImageUrl, "MainBanners");

        var existing = await _unitOfWork.GenericRepository<MainBanner>().Table.FirstOrDefaultAsync(x => x.Id == id);
        if (existing == null)
            return NotFound(new ApiResult<MainBanner>(null, "بنر یافت نشد", ApiResultStatusCode.NotFound));

        existing.ImageUrl = !string.IsNullOrEmpty(imagePath) ? imagePath : existing.ImageUrl;
        existing.Alt = MainBanner.Alt;

        await _unitOfWork.GenericRepository<MainBanner>().UpdateAsync(existing, CancellationToken.None);

        return Ok(new ApiResult<MainBanner>(existing, "بنر با موفقیت بروزرسانی شد", ApiResultStatusCode.Success));
    }

    [HttpDelete("DeleteMainBanner/{id}")]
    public async Task<ActionResult<ApiResult<bool>>> DeleteMainBanner(int id)
    {
        var MainBanner =
            await _unitOfWork.GenericRepository<MainBanner>().Table.FirstOrDefaultAsync(x => x.Id == id);
        if (MainBanner == null)
            return NotFound(new ApiResult<bool>(false, "بنر یافت نشد", ApiResultStatusCode.NotFound));

        await _unitOfWork.GenericRepository<MainBanner>().DeleteAsync(MainBanner, CancellationToken.None);

        return Ok(new ApiResult<bool>(true, "بنر با موفقیت حذف شد", ApiResultStatusCode.Success));
    }
}