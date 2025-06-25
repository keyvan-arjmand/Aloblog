using Aloblog.Application.Common.ApiResult;
using Aloblog.Application.Dtos.Services;
using Aloblog.Application.Interfaces;
using Aloblog.Domain.Entities.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Aloblog.Api.Controllers;

public class WorkFlowController(IUnitOfWork _unitOfWork) : BaseApiController
{
    [HttpGet("GetWorkFlows")]
    public async Task<ActionResult<ApiResult<List<WorkFlow>>>> GetWorkFlows()
    {
        var result = await _unitOfWork.GenericRepository<WorkFlow>()
            .TableNoTracking
            .Include(x => x.Items.OrderBy(i => i.Priority))
            .ToListAsync();

        return Ok(new ApiResult<List<WorkFlow>>(result, "گردش‌کارها با موفقیت دریافت شدند", ApiResultStatusCode.Success));
    }

    [HttpGet("GetWorkFlowById/{id}")]
    public async Task<ActionResult<ApiResult<WorkFlow>>> GetWorkFlowById(int id)
    {
        var result = await _unitOfWork.GenericRepository<WorkFlow>()
            .TableNoTracking
            .Include(x => x.Items.OrderBy(i => i.Priority))
            .FirstOrDefaultAsync(x => x.Id == id);

        if (result == null)
            return NotFound(new ApiResult<WorkFlow>(null, "گردش‌کار یافت نشد", ApiResultStatusCode.NotFound));

        return Ok(new ApiResult<WorkFlow>(result, "گردش‌کار با موفقیت دریافت شد", ApiResultStatusCode.Success));
    }

    [HttpPost("CreateWorkFlow")]
    public async Task<ActionResult<ApiResult<WorkFlow>>> CreateWorkFlow([FromBody] InsertWorkFlowDto dto)
    {
        var entity = new WorkFlow
        {
            Description = dto.Description,
            Items = dto.Items.Select(i => new WorkFlowItem
            {
                Title = i.Title,
                Image = i.Image,
                Alt = i.Alt,
                Priority = i.Priority
            }).ToList()
        };

        await _unitOfWork.GenericRepository<WorkFlow>().AddAsync(entity, CancellationToken.None);

        return Ok(new ApiResult<WorkFlow>(entity, "گردش‌کار با موفقیت ایجاد شد", ApiResultStatusCode.Success));
    }

    [HttpPut("UpdateWorkFlow/{id}")]
    public async Task<ActionResult<ApiResult<WorkFlow>>> UpdateWorkFlow(int id, [FromBody] UpdateWorkFlowDto dto)
    {
        var existing = await _unitOfWork.GenericRepository<WorkFlow>()
            .Table.Include(x => x.Items).FirstOrDefaultAsync(x => x.Id == id);

        if (existing == null)
            return NotFound(new ApiResult<WorkFlow>(null, "گردش‌کار یافت نشد", ApiResultStatusCode.NotFound));

        existing.Description = dto.Description;

        // پاک‌سازی آیتم‌های قبلی و افزودن جدید
        existing.Items.Clear();
        foreach (var item in dto.Items)
        {
            existing.Items.Add(new WorkFlowItem
            {
                Title = item.Title,
                Image = item.Image,
                Alt = item.Alt,
                Priority = item.Priority,
                WorkFlowId = existing.Id
            });
        }

        await _unitOfWork.GenericRepository<WorkFlow>().UpdateAsync(existing, CancellationToken.None);

        return Ok(new ApiResult<WorkFlow>(existing, "گردش‌کار با موفقیت بروزرسانی شد", ApiResultStatusCode.Success));
    }

    [HttpDelete("DeleteWorkFlow/{id}")]
    public async Task<ActionResult<ApiResult<bool>>> DeleteWorkFlow(int id)
    {
        var entity = await _unitOfWork.GenericRepository<WorkFlow>().Table.FirstOrDefaultAsync(x => x.Id == id);
        if (entity == null)
            return NotFound(new ApiResult<bool>(false, "گردش‌کار یافت نشد", ApiResultStatusCode.NotFound));

        await _unitOfWork.GenericRepository<WorkFlow>().DeleteAsync(entity, CancellationToken.None);

        return Ok(new ApiResult<bool>(true, "گردش‌کار با موفقیت حذف شد", ApiResultStatusCode.Success));
    }
}
