using Aloblog.Application.Common.ApiResult;
using Aloblog.Application.Dtos.Services;
using Aloblog.Application.Interfaces;
using Aloblog.Domain.Entities.MainPages;
using Aloblog.Domain.Entities.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Aloblog.Api.Controllers;

public class ServiceController(IUnitOfWork _unitOfWork, IFileService _fileService) : BaseApiController
{
    [HttpGet("GetServices")]
    public async Task<ActionResult<ApiResult<List<Service>>>> GetServices()
    {
        var result = await _unitOfWork.GenericRepository<Service>().TableNoTracking.ToListAsync();
        return Ok(new ApiResult<List<Service>>(result, "سرویس‌ها با موفقیت دریافت شدند", ApiResultStatusCode.Success));
    }

    [HttpGet("GetServiceById/{id}")]
    public async Task<ActionResult<ApiResult<Service>>> GetServiceById(int id)
    {
        var result = await _unitOfWork.GenericRepository<Service>().TableNoTracking.FirstOrDefaultAsync(x => x.Id == id);
        if (result == null)
            return NotFound(new ApiResult<Service>(null, "سرویس یافت نشد", ApiResultStatusCode.NotFound));

        return Ok(new ApiResult<Service>(result, "سرویس با موفقیت دریافت شد", ApiResultStatusCode.Success));
    }

    [HttpPost("CreateService")]
    public async Task<ActionResult<ApiResult<Service>>> CreateService([FromForm] InsertServiceDto dto)
    {
        if (dto.ImageUrl == null || dto.ImageUrl.Length == 0)
            return BadRequest(new ApiResult<Service>(null, "تصویر الزامی است", ApiResultStatusCode.BadRequest));

        var imagePath = _fileService.UploadFile(dto.ImageUrl, "images");

        var entity = new Service
        {
            Title = dto.Title,
            Alt = dto.Alt,
            Description = dto.Description,
            ImageUrl = imagePath
        };

        await _unitOfWork.GenericRepository<Service>().AddAsync(entity, CancellationToken.None);

        return Ok(new ApiResult<Service>(entity, "سرویس با موفقیت ایجاد شد", ApiResultStatusCode.Success));
    }

    [HttpPut("UpdateService/{id}")]
    public async Task<ActionResult<ApiResult<Service>>> UpdateService(int id, [FromForm] UpdateServiceDto dto)
    {
        var existing = await _unitOfWork.GenericRepository<Service>().Table.FirstOrDefaultAsync(x => x.Id == id);
        if (existing == null)
            return NotFound(new ApiResult<Service>(null, "سرویس یافت نشد", ApiResultStatusCode.NotFound));

        var imagePath = _fileService.UploadFile(dto.ImageUrl, "services");

        existing.Title = dto.Title;
        existing.Alt = dto.Alt;
        existing.Description = dto.Description;
        existing.ImageUrl = !string.IsNullOrEmpty(imagePath) ? imagePath : existing.ImageUrl;

        await _unitOfWork.GenericRepository<Service>().UpdateAsync(existing, CancellationToken.None);

        return Ok(new ApiResult<Service>(existing, "سرویس با موفقیت بروزرسانی شد", ApiResultStatusCode.Success));
    }

    [HttpDelete("DeleteService/{id}")]
    public async Task<ActionResult<ApiResult<bool>>> DeleteService(int id)
    {
        var entity = await _unitOfWork.GenericRepository<Service>().Table.FirstOrDefaultAsync(x => x.Id == id);
        if (entity == null)
            return NotFound(new ApiResult<bool>(false, "سرویس یافت نشد", ApiResultStatusCode.NotFound));

        await _unitOfWork.GenericRepository<Service>().DeleteAsync(entity, CancellationToken.None);

        return Ok(new ApiResult<bool>(true, "سرویس با موفقیت حذف شد", ApiResultStatusCode.Success));
    }
}
