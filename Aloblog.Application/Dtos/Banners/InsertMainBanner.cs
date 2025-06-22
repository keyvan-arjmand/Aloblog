using Microsoft.AspNetCore.Http;

namespace Aloblog.Application.Dtos.Banners;

public class InsertMainBanner
{
    public IFormFile ImageUrl { get; set; } 
    public string? Alt { get; set; }
}