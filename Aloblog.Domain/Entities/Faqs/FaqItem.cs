using Aloblog.Domain.Common;

namespace Aloblog.Domain.Entities.Faqs;

public class FaqItem : BaseEntity
{
    public int FaqId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}