using Microsoft.AspNetCore.Mvc;

namespace Aloblog.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public abstract class BaseApiController : ControllerBase
{
}