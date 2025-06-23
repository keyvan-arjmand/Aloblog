using Aloblog.Domain.Common;

namespace Aloblog.Domain.Entities.Services;

public class WorkFlowItem : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
    public string Alt { get; set; } = string.Empty;
    public int Priority { get; set; }
    public int WorkFlowId { get; set; }
}