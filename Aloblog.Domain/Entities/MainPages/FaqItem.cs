using Aloblog.Domain.Common;

namespace Aloblog.Domain.Entities.MainPages;

public class FaqItem : BaseEntity
{
    public int FaqId { get; set; }
    public string Title { get; set; } = string.Empty;
}