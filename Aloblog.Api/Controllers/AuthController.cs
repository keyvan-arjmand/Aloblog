using Aloblog.Application.Common.ApiResult;
using Aloblog.Application.Common.Utilities;
using Aloblog.Application.Dtos.Login;
using Aloblog.Domain.Entities.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Aloblog.Api.Controllers;

public class AuthController(
    UserManager<User> userManager,
    SignInManager<User> signInManager,
    JwtService jwtService,
    RoleManager<Role> roleManager)
    : BaseApiController
{
    private readonly SignInManager<User> _signInManager = signInManager;

    [HttpPost("register")]
    public async Task<ActionResult<ApiResult>> Register([FromBody] RegisterDto dto)
    {
        var user = await userManager.Users.FirstOrDefaultAsync(u => u.PhoneNumber == dto.PhoneNumber);
        if (user != null)
            return BadRequest(new ApiResult("کاربری با این شماره قبلاً ثبت ‌نام کرده است",
                ApiResultStatusCode.DuplicateRecord, false));

        user = new User
        {
            UserName = dto.PhoneNumber,
            PhoneNumber = dto.PhoneNumber,
            PhoneNumberConfirmed = false
        };

        var createResult = await userManager.CreateAsync(user);
        if (!createResult.Succeeded)
        {
            var errorMessages = string.Join(" | ", createResult.Errors.Select(e => e.Description));
            return BadRequest(new ApiResult(errorMessages, ApiResultStatusCode.BadRequest, false));
        }

        // ساخت نقش customer اگر وجود نداشت
        const string roleName = "customer";
        if (!await roleManager.RoleExistsAsync(roleName))
        {
            await roleManager.CreateAsync(new Role
            {
                Name = roleName,
            });
        }

        await userManager.AddToRoleAsync(user, roleName);

        return Ok(new ApiResult("ثبت‌نام با موفقیت انجام شد", ApiResultStatusCode.Success));
    }


    [HttpPost("login")]
    public async Task<ActionResult<ApiResult>> LoginWithPhone([FromBody] LoginDto dto)
    {
        var user = await userManager.Users.FirstOrDefaultAsync(u => u.PhoneNumber == dto.Username);
        if (user == null)
        {
            user = new User
            {
                UserName = dto.Username,
                PhoneNumber = dto.Username,
                PhoneNumberConfirmed = false
            };
            var createResult = await userManager.CreateAsync(user);
            if (!createResult.Succeeded)
            {
                var errorMessages = string.Join(" | ", createResult.Errors.Select(e => e.Description));
                return BadRequest(new ApiResult(errorMessages, ApiResultStatusCode.BadRequest, false));
            }
        }

        // تولید OTP
        var otp = new Random().Next(1000, 9999).ToString();

        // ذخیره در SecurityStamp
        user.SecurityStamp = otp;
        await userManager.UpdateAsync(user);

        // پاسخ (در واقع باید از طریق پیامک ارسال شود)
        return Ok(new ApiResult<object>(new
        {
            message = "کد تایید ارسال شد",
            otp, // فقط برای تست
            userId = user.Id
        }, "موفق", ApiResultStatusCode.Success));
    }


    [HttpPost("VerifyOtp")]
    public async Task<ActionResult<ApiResult<string>>> VerifyOtp([FromBody] VerifyOtpDto dto)
    {
        var user = await userManager.Users.FirstOrDefaultAsync(u => u.PhoneNumber == dto.PhoneNumber);
        
        if (user == null)
            return BadRequest(new ApiResult("کاربری با این شماره یافت نشد", ApiResultStatusCode.NotFound, false));
        
        if (user.SecurityStamp != dto.Otp)
            return Unauthorized(new ApiResult("کد تأیید اشتباه است", ApiResultStatusCode.UnAuthorized, false));
        
        const string roleName = "customer";
        if (!await roleManager.RoleExistsAsync(roleName))
        {
            await roleManager.CreateAsync(new Role
            {
                Name = roleName,
            });
        }

        if (!await userManager.IsInRoleAsync(user, roleName))
        {
            await userManager.AddToRoleAsync(user, roleName);
        }

        var roles = await userManager.GetRolesAsync(user);
        var token = jwtService.GenerateToken(user, roles);
        
        user.SecurityStamp = Guid.NewGuid().ToString();
        user.PhoneNumberConfirmed = true;
        await userManager.UpdateAsync(user);

        return Ok(new ApiResult<string>(token, "ورود موفقیت‌آمیز بود", ApiResultStatusCode.Success));
    }
}