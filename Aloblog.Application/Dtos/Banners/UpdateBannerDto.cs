using Microsoft.AspNetCore.Http;

namespace Aloblog.Application.Dtos.Banners;

public class UpdateBannerDto
{
    public int Id { get; set; }
    public IFormFile ImageUrl { get; set; }
    public string Alt { get; set; } = string.Empty;
    public int Priority { get; set; }
}