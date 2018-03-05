using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TF.BasketballStats.Database
{
    public class DesignDB : IDesignTimeDbContextFactory<DatabaseContext>
    {
        public DatabaseContext CreateDbContext(string[] args)
        {
            return new DatabaseContext(new DbContextOptionsBuilder<DatabaseContext>().UseSqlServer("Server=tcp:coinstore.database.windows.net,1433;Initial Catalog=kando;Persist Security Info=False;User ID=serionist;Password=David4ever;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;").Options);
        }
    }
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Club> Clubs { get; set; }
        public DbSet<GameEvent> GameEvents { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<Player> Players { get; set; }
    }
}
