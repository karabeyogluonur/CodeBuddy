using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CB.Data.Contexts
{
    public class CBDbContext : DbContext
    {
        public CBDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
