using Microsoft.EntityFrameworkCore;

namespace Autofarm.Сommon.DataBase
{
    public class GameContext : DbContext
    {
        public DbSet<BaseGameInfo> gamesInfo => Set<BaseGameInfo>();
        public DbSet<BaseHeader> headers => Set<BaseHeader>();
        public DbSet<BaseToken> tokens => Set<BaseToken>();
        public DbSet<BaseUrl> urls => Set<BaseUrl>();

        public GameContext()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=autofarmApp.db");
        }


    }
}
