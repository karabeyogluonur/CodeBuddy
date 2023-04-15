using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CB.Application.Models.Admin.Features
{
    public class TalentUpdateModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Active { get; set; }
        public IFormFile ImageFile { get; set; }
        public bool ChangeImage { get; set; }
    }
}
