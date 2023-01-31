using Microsoft.EntityFrameworkCore;

namespace BrowlingGame.DAL.Context
{
    public class BrowlingContext: DbContext
    {
        public BrowlingContext(DbContextOptions<BrowlingContext> options): base(options) { }
        public DbSet<Game> Games { get; set;}
        public DbSet<Frame> Frames { get; set;}
        public DbSet<Player> Players { get; set; }
    }
}
