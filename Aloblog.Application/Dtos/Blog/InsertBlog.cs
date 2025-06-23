using Microsoft.AspNetCore.Http;

namespace Aloblog.Application.Dtos.Blog;

public class InsertBlog
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public IFormFile ImageUrl { get; set; } 
    public string Alt { get; set; } = string.Empty;
    
    public string DetailTitle { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string HtmlDesc { get; set; } = string.Empty;
    public string Date { get; set; } = string.Empty;
}