using Aloblog.Domain.Common;
using Aloblog.Domain.Common.Enums;

namespace Aloblog.Domain.Entities.Orders;

public class Order : BaseEntity
{
    public string FullName { get; set; } = string.Empty;
    public string Tel { get; set; } = string.Empty;
    public OrderType OrderType { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
}