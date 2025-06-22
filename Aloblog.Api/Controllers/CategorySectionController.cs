using Aloblog.Application.Common.ApiResult;
using Aloblog.Application.Dtos.CategorySection;
using Aloblog.Application.Interfaces;
using Aloblog.Domain.Entities.MainPages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Aloblog.Api.Controllers;

public class CategorySectionController(IUnitOfWork _unitOfWork) : BaseApiController
{
    [HttpGet("GetCategorySections")]
    public async Task<ActionResult<ApiResult<List<CategorySection>>>> GetCategorySections()
    {
        var result = await _unitOfWork.GenericRepository<CategorySection>().TableNoTracking
            .OrderBy(c => c.Priority).ToListAsync();

        return Ok(new ApiResult<List<CategorySection>>(result, "بخش‌ها با موفقیت دریافت شدند", ApiResultStatusCode.Success));
    }

    [HttpGet("GetCategorySectionById/{id}")]
    public async Task<ActionResult<ApiResult<CategorySection>>> GetCategorySectionById(int id)
    {
        var result = await _unitOfWork.GenericRepository<CategorySection>().TableNoTracking
            .FirstOrDefaultAsync(x => x.Id == id);

        if (result == null)
            return NotFound(new ApiResult<CategorySection>(null, "بخش یافت نشد", ApiResultStatusCode.NotFound));

        return Ok(new ApiResult<CategorySection>(result, "بخش با موفقیت دریافت شد", ApiResultStatusCode.Success));
    }

    [HttpPost("CreateCategorySection")]
    public async Task<ActionResult<ApiResult<CategorySection>>> CreateCategorySection([FromBody] InsertCategorySectionDto dto)
    {
        var entity = new CategorySection
        {
            Title = dto.Title,
            Priority = dto.Priority
        };

        await _unitOfWork.GenericRepository<CategorySection>().AddAsync(entity, CancellationToken.None);

        return Ok(new ApiResult<CategorySection>(entity, "بخش با موفقیت ایجاد شد", ApiResultStatusCode.Success));
    }

    [HttpPut("UpdateCategorySection/{id}")]
    public async Task<ActionResult<ApiResult<CategorySection>>> UpdateCategorySection(int id, [FromBody] UpdateCategorySectionDto dto)
    {
        var existing = await _unitOfWork.GenericRepository<CategorySection>().Table.FirstOrDefaultAsync(x => x.Id == id);
        if (existing == null)
            return NotFound(new ApiResult<CategorySection>(null, "بخش یافت نشد", ApiResultStatusCode.NotFound));

        existing.Title = dto.Title;
        existing.Priority = dto.Priority;

        await _unitOfWork.GenericRepository<CategorySection>().UpdateAsync(existing, CancellationToken.None);

        return Ok(new ApiResult<CategorySection>(existing, "بخش با موفقیت بروزرسانی شد", ApiResultStatusCode.Success));
    }

    [HttpDelete("DeleteCategorySection/{id}")]
    public async Task<ActionResult<ApiResult<bool>>> DeleteCategorySection(int id)
    {
        var entity = await _unitOfWork.GenericRepository<CategorySection>().Table.FirstOrDefaultAsync(x => x.Id == id);
        if (entity == null)
            return NotFound(new ApiResult<bool>(false, "بخش یافت نشد", ApiResultStatusCode.NotFound));

        await _unitOfWork.GenericRepository<CategorySection>().DeleteAsync(entity, CancellationToken.None);

        return Ok(new ApiResult<bool>(true, "بخش با موفقیت حذف شد", ApiResultStatusCode.Success));
    }
}
