using Aloblog.Application.Common.ApiResult;
using Aloblog.Application.Dtos.Services;
using Aloblog.Application.Interfaces;
using Aloblog.Domain.Entities.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Aloblog.Api.Controllers;

public class WorkFlowController(IUnitOfWork _unitOfWork, IFileService fileService) : BaseApiController
{
    [HttpGet("GetWorkFlows")]
    public async Task<ActionResult<ApiResult<List<WorkFlow>>>> GetWorkFlows()
    {
        var result = await _unitOfWork.GenericRepository<WorkFlow>()
            .TableNoTracking
            .Include(x => x.Items.OrderBy(i => i.Priority))
            .ToListAsync();

        return Ok(
            new ApiResult<List<WorkFlow>>(result, "گردش‌کارها با موفقیت دریافت شدند", ApiResultStatusCode.Success));
    }

    [HttpGet("GetWorkFlowById/{id}")]
    public async Task<ActionResult<ApiResult<WorkFlow>>> GetWorkFlowById(int id)
    {
        var result = await _unitOfWork.GenericRepository<WorkFlow>()
            .TableNoTracking
            .Include(x => x.Items.OrderBy(i => i.Priority))
            .FirstOrDefaultAsync(x => x.Id == id);

        if (result == null)
            return NotFound(new ApiResult("گردش‌ کار یافت نشد", ApiResultStatusCode.NotFound));

        return Ok(new ApiResult<WorkFlow>(result, "گردش ‌کار با موفقیت دریافت شد", ApiResultStatusCode.Success));
    }

    [HttpPost("CreateWorkFlow")]
    public async Task<ActionResult<ApiResult<WorkFlow>>> CreateWorkFlow([FromForm] InsertWorkFlowDto dto)
    {
        var entity = new WorkFlow
        {
            Description = dto.Description
        };
        await _unitOfWork.GenericRepository<WorkFlow>().AddAsync(entity, CancellationToken.None);

        foreach (var item in dto.Items)
        {
            var img = fileService.UploadFile(item.Image, "WorkFlows");
            await _unitOfWork.GenericRepository<WorkFlowItem>().AddAsync(new WorkFlowItem
            {
                WorkFlowId = entity.Id,
                Image = img,
                Alt = item.Alt,
                Priority = item.Priority,
                Title = item.Title,
            }, CancellationToken.None);
        }

        return Ok(new ApiResult<WorkFlow>(entity, "گردش‌کار با موفقیت ایجاد شد", ApiResultStatusCode.Success));
    }

    [HttpPut("UpdateWorkFlow/{id}")]
    public async Task<ActionResult> UpdateWorkFlow(int id, [FromForm] UpdateWorkFlowDto dto)
    {
        var existing = await _unitOfWork.GenericRepository<WorkFlow>()
            .Table.Include(x => x.Items).FirstOrDefaultAsync(x => x.Id == id);

        if (existing == null)
            return NotFound(new ApiResult("گردش ‌کار یافت نشد", ApiResultStatusCode.NotFound));

        existing.Description = dto.Description;
        await _unitOfWork.GenericRepository<WorkFlow>().UpdateAsync(existing, CancellationToken.None);
        // پاک‌سازی آیتم‌های قبلی و افزودن جدید
        foreach (var item in existing.Items)
        {
            await _unitOfWork.GenericRepository<WorkFlowItem>().DeleteAsync(item, CancellationToken.None);
        }

        foreach (var item in dto.Items)
        {
            var img = fileService.UploadFile(item.Image, "WorkFlows");
            await _unitOfWork.GenericRepository<WorkFlowItem>().AddAsync(new WorkFlowItem
            {
                WorkFlowId = existing.Id,
                Image = img,
                Alt = item.Alt,
                Priority = item.Priority,
                Title = item.Title,
            }, CancellationToken.None);
        }
        return Ok(new ApiResult("گردش ‌کار با موفقیت بروزرسانی شد", ApiResultStatusCode.Success));
    }

    [HttpDelete("DeleteWorkFlow/{id}")]
    public async Task<ActionResult<ApiResult<bool>>> DeleteWorkFlow(int id)
    {
        var entity = await _unitOfWork.GenericRepository<WorkFlow>().Table.FirstOrDefaultAsync(x => x.Id == id);
        if (entity == null)
            return NotFound(new ApiResult<bool>(false, "گردش‌ کار یافت نشد", ApiResultStatusCode.NotFound));

        await _unitOfWork.GenericRepository<WorkFlow>().DeleteAsync(entity, CancellationToken.None);

        return Ok(new ApiResult<bool>(true, "گردش ‌کار با موفقیت حذف شد", ApiResultStatusCode.Success));
    }
}