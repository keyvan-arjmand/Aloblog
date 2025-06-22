using Aloblog.Domain.Common;

namespace Aloblog.Domain.Entities.Blogs;

public class BlogDetail : BaseEntity
{
    public int BlogId { get; set; }
    public string Slug { get; set; } = string.Empty;
    public string HtmlDesc { get; set; } = string.Empty;
    public string Date { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
}