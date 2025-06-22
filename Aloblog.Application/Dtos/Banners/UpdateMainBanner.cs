using Microsoft.AspNetCore.Http;

namespace Aloblog.Application.Dtos.Banners;

public class UpdateMainBanner
{
    public IFormFile ImageUrl { get; set; } 
    public string? Alt { get; set; }
}