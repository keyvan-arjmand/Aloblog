using Aloblog.Domain.Common;

namespace Aloblog.Domain.Entities.SeoPages;

public class PageDetail:BaseEntity
{
    public string Patch { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}