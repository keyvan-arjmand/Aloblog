using Aloblog.Application.Common.ApiResult;
using Aloblog.Application.Dtos.Banners;
using Aloblog.Application.Interfaces;
using Aloblog.Domain.Entities.Banners;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Aloblog.Api.Controllers;

public class BannerSliderSliderController(IUnitOfWork _unitOfWork, IFileService _fileService) : BaseApiController
{
    [HttpGet("GetBannerSliders")]
    public async Task<ActionResult<ApiResult<List<BannerSlider>>>> GetBannerSliders()
    {
        var result = await _unitOfWork.GenericRepository<BannerSlider>().TableNoTracking.ToListAsync();
        return Ok(new ApiResult<List<BannerSlider>>(result, "بنرها با موفقیت دریافت شدند",
            ApiResultStatusCode.Success));
    }

    [HttpGet("GetBannerSliderById/{id}")]
    public async Task<ActionResult<ApiResult<BannerSlider>>> GetBannerSliderById(int id)
    {
        var result = await _unitOfWork.GenericRepository<BannerSlider>().TableNoTracking
            .FirstOrDefaultAsync(x => x.Id == id);

        if (result == null)
            return NotFound(new ApiResult<BannerSlider>(null, "بنر یافت نشد", ApiResultStatusCode.NotFound));

        return Ok(new ApiResult<BannerSlider>(result, "بنر با موفقیت دریافت شد", ApiResultStatusCode.Success));
    }

    [HttpPost("CreateBannerSlider")]
    public async Task<ActionResult<ApiResult<BannerSlider>>> CreateBannerSlider(
        [FromForm] InsertBannerSlider BannerSlider)
    {
        if (BannerSlider.ImageUrl == null || BannerSlider.ImageUrl.Length == 0)
        {
            return BadRequest(new ApiResult<BannerSlider>(null, "تصویر الزامی است", ApiResultStatusCode.BadRequest));
        }

        var imagePath = _fileService.UploadFile(BannerSlider.ImageUrl, "BannerSliders");

        var entity = new BannerSlider
        {
            ImageUrl = imagePath,
            Alt = BannerSlider.Alt,
            Priority = BannerSlider.Priority,
        };

        await _unitOfWork.GenericRepository<BannerSlider>().AddAsync(entity, CancellationToken.None);

        return Ok(new ApiResult<BannerSlider>(entity, "بنر با موفقیت ایجاد شد", ApiResultStatusCode.Success));
    }

    [HttpPut("UpdateBannerSlider/{id}")]
    public async Task<ActionResult<ApiResult<BannerSlider>>> UpdateBannerSlider(int id,
        [FromForm] UpdateBannerSlider BannerSlider)
    {
        var imagePath = _fileService.UploadFile(BannerSlider.ImageUrl, "BannerSliders");

        var existing = await _unitOfWork.GenericRepository<BannerSlider>().Table.FirstOrDefaultAsync(x => x.Id == id);
        if (existing == null)
            return NotFound(new ApiResult<BannerSlider>(null, "بنر یافت نشد", ApiResultStatusCode.NotFound));

        existing.ImageUrl = !string.IsNullOrEmpty(imagePath) ? imagePath : existing.ImageUrl;
        existing.Alt = BannerSlider.Alt;
        existing.Priority = BannerSlider.Priority;

        await _unitOfWork.GenericRepository<BannerSlider>().UpdateAsync(existing, CancellationToken.None);

        return Ok(new ApiResult<BannerSlider>(existing, "بنر با موفقیت بروزرسانی شد", ApiResultStatusCode.Success));
    }

    [HttpDelete("DeleteBannerSlider/{id}")]
    public async Task<ActionResult<ApiResult<bool>>> DeleteBannerSlider(int id)
    {
        var BannerSlider =
            await _unitOfWork.GenericRepository<BannerSlider>().Table.FirstOrDefaultAsync(x => x.Id == id);
        if (BannerSlider == null)
            return NotFound(new ApiResult<bool>(false, "بنر یافت نشد", ApiResultStatusCode.NotFound));

        await _unitOfWork.GenericRepository<BannerSlider>().DeleteAsync(BannerSlider, CancellationToken.None);

        return Ok(new ApiResult<bool>(true, "بنر با موفقیت حذف شد", ApiResultStatusCode.Success));
    }
}