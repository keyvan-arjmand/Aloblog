﻿using System.ComponentModel.DataAnnotations;

namespace Aloblog.Application.Common.ApiResult;

public enum ApiResultStatusCode
{
    [Display(Name = "عملیات با موفقیت انجام شد")]
    Success = 0,

    [Display(Name = "خطایی در سرور رخ داده است")]
    ServerError = 1,

    [Display(Name = "پارامتر های ارسالی معتبر نیستند")]
    BadRequest = 2,

    [Display(Name = "یافت نشد")] NotFound = 3,

    [Display(Name = "لیست خالی است")] ListEmpty = 4,

    [Display(Name = "خطایی در پردازش رخ داد")]
    LogicError = 5,

    [Display(Name = "خطای احراز هویت")] UnAuthorized = 6,

    [Display(Name = "خطای دسترسی")] Forbidden = 7,
    [Display(Name = "رکورد تکراری")] DuplicateRecord = 8
}