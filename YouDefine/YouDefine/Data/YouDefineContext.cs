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

        public DbSet<Idea> Ideas { get; set; }

        public DbSet<Definition> Definitions { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Idea>()
        //        .HasMany(c => c.Definitions)
        //        .WithOne(e => e.Idea)
        //        .OnDelete(DeleteBehavior.Cascade);
        //}
    }
}
