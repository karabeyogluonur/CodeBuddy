using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CB.Application.Models.Admin.Features
{
    public class TalentCreateModel
    {
        public string Name { get; set; }
        public bool Active { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}
