using Aloblog.Domain.Common;

namespace Aloblog.Domain.Entities.MainPages;

public class BrandSlider : BaseEntity
{
    public string ImageUrl { get; set; } = string.Empty;
    public string? Alt { get; set; }
    public int Priority { get; set; }
}