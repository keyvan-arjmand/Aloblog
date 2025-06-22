using Aloblog.Domain.Common;

namespace Aloblog.Domain.Entities.MainPages;

public class DesignTree : BaseEntity
{
    public string ImageUrl { get; set; } = string.Empty;
    public string Label { get; set; } = string.Empty;
    public string? Alt { get; set; }
    public string? Image { get; set; }
    public ICollection<DesignItem> Items { get; set; } = [];
}