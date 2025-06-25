using Aloblog.Application.Common.ApiResult;
using Aloblog.Application.Dtos.Orders;
using Aloblog.Application.Interfaces;
using Aloblog.Domain.Entities.Orders;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Aloblog.Api.Controllers;


public class OrderController(IUnitOfWork _unitOfWork) : BaseApiController
{
    [HttpGet("GetOrders")]
    public async Task<ActionResult<ApiResult<List<Order>>>> GetOrders()
    {
        var result = await _unitOfWork.GenericRepository<Order>()
            .TableNoTracking.OrderByDescending(x => x.Id).ToListAsync();

        return Ok(new ApiResult<List<Order>>(result, "سفارش‌ها با موفقیت دریافت شدند", ApiResultStatusCode.Success));
    }

    [HttpGet("GetOrderById/{id}")]
    public async Task<ActionResult<ApiResult<Order>>> GetOrderById(int id)
    {
        var result = await _unitOfWork.GenericRepository<Order>()
            .TableNoTracking.FirstOrDefaultAsync(x => x.Id == id);

        if (result == null)
            return NotFound(new ApiResult<Order>(null, "سفارش یافت نشد", ApiResultStatusCode.NotFound));

        return Ok(new ApiResult<Order>(result, "سفارش با موفقیت دریافت شد", ApiResultStatusCode.Success));
    }

    [HttpPost("CreateOrder")]
    public async Task<ActionResult<ApiResult<Order>>> CreateOrder([FromBody] InsertOrderDto dto)
    {
        var entity = new Order
        {
            FullName = dto.FullName,
            Tel = dto.Tel,
            Address = dto.Address,
            Description = dto.Description,
            OrderType = dto.OrderType
        };

        await _unitOfWork.GenericRepository<Order>().AddAsync(entity, CancellationToken.None);

        return Ok(new ApiResult<Order>(entity, "سفارش با موفقیت ثبت شد", ApiResultStatusCode.Success));
    }

    [HttpPut("UpdateOrder/{id}")]
    public async Task<ActionResult<ApiResult<Order>>> UpdateOrder(int id, [FromBody] UpdateOrderDto dto)
    {
        var existing = await _unitOfWork.GenericRepository<Order>().Table.FirstOrDefaultAsync(x => x.Id == id);
        if (existing == null)
            return NotFound(new ApiResult<Order>(null, "سفارش یافت نشد", ApiResultStatusCode.NotFound));

        existing.FullName = dto.FullName;
        existing.Tel = dto.Tel;
        existing.Address = dto.Address;
        existing.Description = dto.Description;
        existing.OrderType = dto.OrderType;

        await _unitOfWork.GenericRepository<Order>().UpdateAsync(existing, CancellationToken.None);

        return Ok(new ApiResult<Order>(existing, "سفارش با موفقیت بروزرسانی شد", ApiResultStatusCode.Success));
    }

    [HttpDelete("DeleteOrder/{id}")]
    public async Task<ActionResult<ApiResult<bool>>> DeleteOrder(int id)
    {
        var entity = await _unitOfWork.GenericRepository<Order>().Table.FirstOrDefaultAsync(x => x.Id == id);
        if (entity == null)
            return NotFound(new ApiResult<bool>(false, "سفارش یافت نشد", ApiResultStatusCode.NotFound));

        await _unitOfWork.GenericRepository<Order>().DeleteAsync(entity, CancellationToken.None);

        return Ok(new ApiResult<bool>(true, "سفارش با موفقیت حذف شد", ApiResultStatusCode.Success));
    }
}
