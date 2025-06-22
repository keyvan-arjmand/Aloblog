using Aloblog.Domain.Common;

namespace Aloblog.Domain.Entities.Banners;

public class MainBanner : BaseEntity
{
    public string ImageUrl { get; set; } = string.Empty;
    public string? Alt { get; set; }
}