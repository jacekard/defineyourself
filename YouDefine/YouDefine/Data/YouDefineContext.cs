using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using YouDefine.Models;

namespace YouDefine.Models
{
    public class YouDefineContext : DbContext
    {
        public YouDefineContext (DbContextOptions<YouDefineContext> options)
            : base(options)
        {
        }

        public DbSet<YouDefine.Models.Definition> Definition { get; set; }

        public DbSet<YouDefine.Models.Author> Author { get; set; }

        public DbSet<YouDefine.Models.Idea> Idea { get; set; }
    }
}
