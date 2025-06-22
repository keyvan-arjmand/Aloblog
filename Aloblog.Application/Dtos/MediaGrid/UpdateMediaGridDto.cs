using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aloblog.Application.Dtos.MediaGrid
{
    public class UpdateMediaGridDto
    {
        public IFormFile? MediaUrl { get; set; }
        public IFormFile? Poster { get; set; }
        public string? Alt { get; set; }
        public int Priority { get; set; }
    }
}
