using Microsoft.AspNetCore.Mvc;

namespace Aloblog.Api.Controllers;

public class BrandController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}