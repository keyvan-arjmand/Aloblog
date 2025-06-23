using System.ComponentModel.DataAnnotations.Schema;
using Aloblog.Domain.Common;

namespace Aloblog.Domain.Entities.Blogs;

public class Blog : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public string Alt { get; set; } = string.Empty;
    public int? BlogDetailId { get; set; }
    [ForeignKey("BlogDetailId")] public BlogDetail? BlogDetail { get; set; }
    
}