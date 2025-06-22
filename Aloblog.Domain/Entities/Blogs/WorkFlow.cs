using Aloblog.Domain.Common;

namespace Aloblog.Domain.Entities.Blogs;

public class WorkFlow : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
    public string Alt { get; set; } = string.Empty;
    public int Priority { get; set; }
    public ICollection<WorkFlowItem> Items { get; set; } = [];
}