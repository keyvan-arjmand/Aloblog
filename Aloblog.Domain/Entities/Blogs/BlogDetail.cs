using Aloblog.Domain.Common;

namespace Aloblog.Domain.Entities.Blogs;

public class BlogDetail : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string HtmlDesc { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public int BlogId { get; set; }
}