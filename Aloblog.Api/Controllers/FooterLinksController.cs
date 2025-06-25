using Aloblog.Application.Common.ApiResult;
using Aloblog.Application.Dtos.FooterLinks;
using Aloblog.Application.Interfaces;
using Aloblog.Domain.Entities.FooterLinks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Aloblog.Api.Controllers;

public class FooterLinksController(IUnitOfWork _unitOfWork) : BaseApiController
{
    [HttpGet("GetFooters")]
    public async Task<ActionResult<ApiResult<List<Footer>>>> GetFooters()
    {
        var result = await _unitOfWork.GenericRepository<Footer>()
            .TableNoTracking.OrderByDescending(x => x.Id).ToListAsync();

        return Ok(new ApiResult<List<Footer>>(result, "لینک‌های فوتر با موفقیت دریافت شدند", ApiResultStatusCode.Success));
    }

    [HttpGet("GetFooterById/{id}")]
    public async Task<ActionResult<ApiResult<Footer>>> GetFooterById(int id)
    {
        var result = await _unitOfWork.GenericRepository<Footer>()
            .TableNoTracking.FirstOrDefaultAsync(x => x.Id == id);

        if (result == null)
            return NotFound(new ApiResult<Footer>(null, "لینک فوتر یافت نشد", ApiResultStatusCode.NotFound));

        return Ok(new ApiResult<Footer>(result, "لینک فوتر با موفقیت دریافت شد", ApiResultStatusCode.Success));
    }

    [HttpPost("CreateFooter")]
    public async Task<ActionResult<ApiResult<Footer>>> CreateFooter([FromBody] InsertFooterDto dto)
    {
        var entity = new Footer
        {
            Title = dto.Title,
            Url = dto.Url
        };

        await _unitOfWork.GenericRepository<Footer>().AddAsync(entity, CancellationToken.None);

        return Ok(new ApiResult<Footer>(entity, "لینک فوتر با موفقیت ایجاد شد", ApiResultStatusCode.Success));
    }

    [HttpPut("UpdateFooter/{id}")]
    public async Task<ActionResult<ApiResult<Footer>>> UpdateFooter(int id, [FromBody] UpdateFooterDto dto)
    {
        var existing = await _unitOfWork.GenericRepository<Footer>().Table.FirstOrDefaultAsync(x => x.Id == id);
        if (existing == null)
            return NotFound(new ApiResult<Footer>(null, "لینک فوتر یافت نشد", ApiResultStatusCode.NotFound));

        existing.Title = dto.Title;
        existing.Url = dto.Url;

        await _unitOfWork.GenericRepository<Footer>().UpdateAsync(existing, CancellationToken.None);

        return Ok(new ApiResult<Footer>(existing, "لینک فوتر با موفقیت بروزرسانی شد", ApiResultStatusCode.Success));
    }

    [HttpDelete("DeleteFooter/{id}")]
    public async Task<ActionResult<ApiResult<bool>>> DeleteFooter(int id)
    {
        var entity = await _unitOfWork.GenericRepository<Footer>().Table.FirstOrDefaultAsync(x => x.Id == id);
        if (entity == null)
            return NotFound(new ApiResult<bool>(false, "لینک فوتر یافت نشد", ApiResultStatusCode.NotFound));

        await _unitOfWork.GenericRepository<Footer>().DeleteAsync(entity, CancellationToken.None);

        return Ok(new ApiResult<bool>(true, "لینک فوتر با موفقیت حذف شد", ApiResultStatusCode.Success));
    }
}
