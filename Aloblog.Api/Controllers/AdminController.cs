using Aloblog.Application.Common.ApiResult;
using Aloblog.Application.Dtos.Users;
using Aloblog.Domain.Entities.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Aloblog.Api.Controllers;

public class AdminController(UserManager<User> userManager) : BaseApiController
{
    [Authorize(Roles = "Admin")]
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResult<UserDto>>> GetAdminById(int id)
    {
        var user = await userManager.Users.FirstOrDefaultAsync(u => u.Id == id);

        if (user == null)
            return NotFound(new ApiResult("کاربر یافت نشد", ApiResultStatusCode.NotFound, false));

        var roles = await userManager.GetRolesAsync(user);

        var result = new UserDto
        {
            Id = user.Id,
            UserName = user.UserName,
            Email = user.Email,
            Roles = roles.ToList()
        };

        return Ok(new ApiResult<UserDto>(result, "کاربر با موفقیت یافت شد", ApiResultStatusCode.Success));
    }

    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<ActionResult<ApiResult<UserDto>>> GetAdminByPhoneNumber(string number)
    {
        var user = await userManager.Users.FirstOrDefaultAsync(x => x.PhoneNumber == number);

        if (user == null)
            return NotFound(new ApiResult("کاربر یافت نشد", ApiResultStatusCode.NotFound, false));

        var roles = await userManager.GetRolesAsync(user);

        var result = new UserDto
        {
            Id = user.Id,
            UserName = user.UserName,
            Email = user.Email,
            Roles = roles.ToList()
        };

        return Ok(new ApiResult<UserDto>(result, "کاربر با موفقیت یافت شد", ApiResultStatusCode.Success));
    }
}