using Aloblog.Domain.Common;

namespace Aloblog.Domain.Entities.MainPages;

public class DesignItem : BaseEntity
{
    public int DesignTreeId { get; set; }
    public string Item { get; set; } = string.Empty;
}