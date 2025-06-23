using Aloblog.Application.Common.ApiResult;
using Aloblog.Application.Dtos.CategorySection;
using Aloblog.Application.Interfaces;
using Aloblog.Domain.Entities.Categories;
using Aloblog.Domain.Entities.MainPages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Aloblog.Api.Controllers;

public class CategoryController(IUnitOfWork _unitOfWork, IFileService _fileService) : BaseApiController
{
    [HttpGet("GetCategorySections")]
    public async Task<ActionResult<ApiResult<List<Category>>>> GetCategorySections()
    {
        var result = await _unitOfWork.GenericRepository<Category>().TableNoTracking
            .Include(x => x.Children)
            .OrderBy(c => c.Priority).ToListAsync();

        return Ok(new ApiResult<List<Category>>(result, "دسته بندی ها با موفقیت دریافت شدند",
            ApiResultStatusCode.Success));
    }

    [HttpGet("GetCategorySectionById/{id}")]
    public async Task<ActionResult<ApiResult<Category>>> GetCategorySectionById(int id)
    {
        var result = await _unitOfWork.GenericRepository<Category>().TableNoTracking
            .Include(x => x.Children)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (result == null)
            return NotFound(new ApiResult("دسته بندی یافت نشد", ApiResultStatusCode.NotFound));

        return Ok(new ApiResult<Category>(result, "دسته بندی با موفقیت دریافت شد", ApiResultStatusCode.Success));
    }

    [HttpPost("CreateCategorySection")]
    public async Task<ActionResult<ApiResult<Category>>> CreateCategorySection([FromBody] InsertCategorySectionDto dto)
    {
        var imagePath = _fileService.UploadFile(dto.Image, "Categories");

        var entity = new Category
        {
            Title = dto.Title,
            Priority = dto.Priority,
            Alt = dto.Alt,
            ParentId = dto.ParentId,
            ImageUrl = imagePath
        };

        await _unitOfWork.GenericRepository<Category>().AddAsync(entity, CancellationToken.None);

        return Ok(new ApiResult<Category>(entity, "دسته بندی با موفقیت ایجاد شد", ApiResultStatusCode.Success));
    }

    [HttpPut("UpdateCategorySection/{id}")]
    public async Task<ActionResult<ApiResult<Category>>> UpdateCategorySection(int id,
        [FromBody] UpdateCategorySectionDto dto)
    {
        var existing = await _unitOfWork.GenericRepository<Category>().Table.FirstOrDefaultAsync(x => x.Id == id);
        if (existing == null)
            return NotFound(new ApiResult("دسته بندی یافت نشد", ApiResultStatusCode.NotFound));
        var imagePath = _fileService.UploadFile(dto.Image, "Categories");

        existing.Title = dto.Title;
        existing.Priority = dto.Priority;
        existing.Alt = dto.Alt;
        existing.ParentId = dto.ParentId;
        existing.ImageUrl = !string.IsNullOrEmpty(imagePath) ? imagePath : existing.ImageUrl;

        await _unitOfWork.GenericRepository<Category>().UpdateAsync(existing, CancellationToken.None);

        return Ok(new ApiResult<Category>(existing, "دسته بندی با موفقیت بروزرسانی شد", ApiResultStatusCode.Success));
    }

    [HttpDelete("DeleteCategorySection/{id}")]
    public async Task<ActionResult<ApiResult<bool>>> DeleteCategorySection(int id)
    {
        var entity = await _unitOfWork.GenericRepository<Category>().Table.FirstOrDefaultAsync(x => x.Id == id);
        if (entity == null)
            return NotFound(new ApiResult<bool>(false, "دسته بندی یافت نشد", ApiResultStatusCode.NotFound));

        await _unitOfWork.GenericRepository<Category>().DeleteAsync(entity, CancellationToken.None);

        return Ok(new ApiResult<bool>(true, "دسته بندی با موفقیت حذف شد", ApiResultStatusCode.Success));
    }
}