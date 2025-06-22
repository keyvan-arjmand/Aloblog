using Aloblog.Domain.Common;

namespace Aloblog.Domain.Entities.MainPages;

public class CategorySection:BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public int Priority { get; set; }
}