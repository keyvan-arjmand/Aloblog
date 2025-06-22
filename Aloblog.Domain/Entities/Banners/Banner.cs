using Aloblog.Domain.Common;

namespace Aloblog.Domain.Entities.Banners;

public class Banner : BaseEntity
{
    public string ImageUrl { get; set; } = string.Empty;
    public string Alt { get; set; } = string.Empty;
    public int Priority { get; set; }
}