using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aloblog.Domain.Common.Enums;


namespace Aloblog.Application.Dtos.Orders
{
    public class InsertOrderDto
    {
        public string FullName { get; set; }
        public string Tel { get; set; }
        public OrderType OrderType { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
    }
}
