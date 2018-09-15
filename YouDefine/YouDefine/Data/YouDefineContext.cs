namespace YouDefine.Data
{
    using Microsoft.EntityFrameworkCore;
    using YouDefine.Models;

    /// <summary>
    /// YouDefineContext - Database Context with DbSet entities
    /// </summary>
    public class YouDefineContext : DbContext
    {
        public YouDefineContext(DbContextOptions<YouDefineContext> options)
            : base(options)
        {
        }

        public DbSet<Idea> Ideas { get; set; }

        public DbSet<Definition> Definitions { get; set; }

        public DbSet<Bug> Bugs { get; set; }

    }
}
