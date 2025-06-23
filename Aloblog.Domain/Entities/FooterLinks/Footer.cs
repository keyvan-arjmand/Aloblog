using Aloblog.Domain.Common;

namespace Aloblog.Domain.Entities.FooterLinks;

public class Footer : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
}