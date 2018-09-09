using Microsoft.EntityFrameworkCore;

namespace YouDefine.Data
{
    public class YouDefineContext : DbContext
    {
        public YouDefineContext (DbContextOptions<YouDefineContext> options)
            : base(options)
        {
        }

        public DbSet<Idea> Ideas { get; set; }

        public DbSet<Definition> Definitions { get; set; }
    }
}
