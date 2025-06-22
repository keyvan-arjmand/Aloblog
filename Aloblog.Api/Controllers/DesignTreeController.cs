using Aloblog.Application.Common.ApiResult;
using Aloblog.Application.Dtos.DesignTree;
using Aloblog.Application.Interfaces;
using Aloblog.Domain.Entities.MainPages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Aloblog.Api.Controllers;

public class DesignTreeController(IUnitOfWork _unitOfWork, IFileService _fileService) : BaseApiController
{
    [HttpGet("GetDesignTrees")]
    public async Task<ActionResult<ApiResult<List<DesignTree>>>> GetDesignTrees()
    {
        var result = await _unitOfWork.GenericRepository<DesignTree>()
            .TableNoTracking.Include(x => x.Items).ToListAsync();

        return Ok(new ApiResult<List<DesignTree>>(result, "درخت طراحی‌ها با موفقیت دریافت شد", ApiResultStatusCode.Success));
    }

    [HttpGet("GetDesignTreeById/{id}")]
    public async Task<ActionResult<ApiResult<DesignTree>>> GetDesignTreeById(int id)
    {
        var result = await _unitOfWork.GenericRepository<DesignTree>()
            .TableNoTracking.Include(x => x.Items).FirstOrDefaultAsync(x => x.Id == id);

        if (result == null)
            return NotFound(new ApiResult<DesignTree>(null, "درخت طراحی یافت نشد", ApiResultStatusCode.NotFound));

        return Ok(new ApiResult<DesignTree>(result, "درخت طراحی با موفقیت دریافت شد", ApiResultStatusCode.Success));
    }

    [HttpPost("CreateDesignTree")]
    public async Task<ActionResult<ApiResult<DesignTree>>> CreateDesignTree([FromForm] InsertDesignTreeDto dto)
    {
        if (dto.ImageUrl == null || dto.ImageUrl.Length == 0)
            return BadRequest(new ApiResult<DesignTree>(null, "تصویر الزامی است", ApiResultStatusCode.BadRequest));

        var imagePath = _fileService.UploadFile(dto.ImageUrl, "design");

        var tree = new DesignTree
        {
            Label = dto.Label,
            Alt = dto.Alt,
            ImageUrl = imagePath,
            Items = dto.Items.Select(i => new DesignItem { Item = i }).ToList()
        };

        await _unitOfWork.GenericRepository<DesignTree>().AddAsync(tree, CancellationToken.None);

        return Ok(new ApiResult<DesignTree>(tree, "درخت طراحی با موفقیت ایجاد شد", ApiResultStatusCode.Success));
    }

    [HttpPut("UpdateDesignTree/{id}")]
    public async Task<ActionResult<ApiResult<DesignTree>>> UpdateDesignTree(int id, [FromForm] UpdateDesignTreeDto dto)
    {
        var existing = await _unitOfWork.GenericRepository<DesignTree>()
            .Table.Include(x => x.Items).FirstOrDefaultAsync(x => x.Id == id);

        if (existing == null)
            return NotFound(new ApiResult<DesignTree>(null, "درخت طراحی یافت نشد", ApiResultStatusCode.NotFound));

        var imagePath = dto.ImageUrl != null ? _fileService.UploadFile(dto.ImageUrl, "design") : existing.ImageUrl;

        existing.Label = dto.Label;
        existing.Alt = dto.Alt;
        existing.ImageUrl = imagePath;

        // حذف آیتم‌های قبلی و جایگزینی با جدیدها
        existing.Items.Clear();
        foreach (var i in dto.Items)
        {
            existing.Items.Add(new DesignItem { Item = i });
        }

        await _unitOfWork.GenericRepository<DesignTree>().UpdateAsync(existing, CancellationToken.None);

        return Ok(new ApiResult<DesignTree>(existing, "درخت طراحی با موفقیت بروزرسانی شد", ApiResultStatusCode.Success));
    }

    [HttpDelete("DeleteDesignTree/{id}")]
    public async Task<ActionResult<ApiResult<bool>>> DeleteDesignTree(int id)
    {
        var entity = await _unitOfWork.GenericRepository<DesignTree>()
            .Table.Include(x => x.Items).FirstOrDefaultAsync(x => x.Id == id);

        if (entity == null)
            return NotFound(new ApiResult<bool>(false, "درخت طراحی یافت نشد", ApiResultStatusCode.NotFound));

        await _unitOfWork.GenericRepository<DesignTree>().DeleteAsync(entity, CancellationToken.None);

        return Ok(new ApiResult<bool>(true, "درخت طراحی با موفقیت حذف شد", ApiResultStatusCode.Success));
    }
}
