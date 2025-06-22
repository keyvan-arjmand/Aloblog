using Aloblog.Domain.Common;

namespace Aloblog.Domain.Entities.MainPages;

public class Faq:BaseEntity
{
    public string Title { get; set; }=string.Empty;
    public string Description { get; set; }=string.Empty;
    public string MediaUrl { get; set; }=string.Empty;
    public string? Alt { get; set; }
    public string? Poster { get; set; }
    public ICollection<FaqItem> Items { get; set; } = [];
}