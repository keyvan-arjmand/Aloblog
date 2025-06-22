using Aloblog.Domain.Common;

namespace Aloblog.Domain.Entities.Blogs;

public class WorkFlowItem : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public int WorkFlowId { get; set; }
    
}