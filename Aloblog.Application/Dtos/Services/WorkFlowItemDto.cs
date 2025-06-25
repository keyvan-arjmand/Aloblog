using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Aloblog.Application.Dtos.Services
{
    public class WorkFlowItemDto
    {
        public string Title { get; set; }
        public IFormFile Image { get; set; }
        public string Alt { get; set; }
        public int Priority { get; set; }
    }

}
