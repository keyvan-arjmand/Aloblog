using Microsoft.AspNetCore.Http;

namespace Aloblog.Application.Dtos.Banners;

public class InsertBannerSlider
{
    public IFormFile ImageUrl { get; set; }
    public string? Alt { get; set; }
    public int Priority { get; set; }
}