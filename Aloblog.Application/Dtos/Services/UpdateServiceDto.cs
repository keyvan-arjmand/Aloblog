using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aloblog.Application.Dtos.Services
{
    public class UpdateServiceDto
    {
        public string Title { get; set; }
        public string Alt { get; set; }
        public string Description { get; set; }
        public IFormFile? ImageUrl { get; set; }
    }
}
