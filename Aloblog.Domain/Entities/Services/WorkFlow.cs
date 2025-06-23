using Aloblog.Domain.Common;

namespace Aloblog.Domain.Entities.Services;

public class WorkFlow : BaseEntity
{
    public string Description { get; set; } = string.Empty;
    public ICollection<WorkFlowItem> Items { get; set; } = [];
}