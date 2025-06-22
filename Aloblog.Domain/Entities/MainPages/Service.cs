using Aloblog.Domain.Common;

namespace Aloblog.Domain.Entities.MainPages;

public class Service : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public string Alt { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}