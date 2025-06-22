using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aloblog.Application.Dtos.DesignTree
{
    public class InsertDesignTreeDto
    {
        public IFormFile ImageUrl { get; set; }
        public string Label { get; set; }
        public string? Alt { get; set; }
        public List<string> Items { get; set; } = new();
    }

}
