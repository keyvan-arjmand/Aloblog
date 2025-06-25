using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aloblog.Application.Dtos.Services
{
    public class UpdateWorkFlowDto
    {
        public string Description { get; set; }
        public List<WorkFlowItemDto> Items { get; set; }
    }

}
