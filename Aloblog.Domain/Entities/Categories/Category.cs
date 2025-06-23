using System.ComponentModel.DataAnnotations.Schema;
using Aloblog.Domain.Common;

namespace Aloblog.Domain.Entities.Categories;

public class Category : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public string Alt { get; set; } = string.Empty;
    public int? ParentId { get; set; }
    [ForeignKey("ParentId")] public Category? Parent { get; set; }
    public int Priority { get; set; }
    public ICollection<Category> Children { get; set; } = [];
}