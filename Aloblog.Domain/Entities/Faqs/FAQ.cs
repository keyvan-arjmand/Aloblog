using Aloblog.Domain.Common;

namespace Aloblog.Domain.Entities.Faqs;

public class Faq:BaseEntity
{
    public string? MediaUrl { get; set; }
    public string? Alt { get; set; }
    public string? Poster { get; set; }
    public ICollection<FaqItem> Items { get; set; } = [];
}