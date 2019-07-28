using System.Data.Entity;
using SportStatistics.Models.Configuration;

namespace SportStatistics.Models
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext()
            : base("name=DatabaseContext")
        {
        }

        public DbSet<Player> Players { get; set; }
        public DbSet<PlayerSeason> PlayerSeasons { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<TeamSeason> TeamSeasons { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<FederationSeason> FederationSeasons { get; set; }
        public DbSet<SportFederation> SportFederations { get; set; }
        public DbSet<Sport> Sports { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {            
            //modelBuilder.Configurations.Add(new PlayerConfig());
            //modelBuilder.Entity<Player>();
            //modelBuilder.Configurations.Add(new PlayerSeasonConfig());
            //modelBuilder.Entity<PlayerSeason>();   
            //modelBuilder.Configurations.Add(new MatchConfig());
            //modelBuilder.Entity<Match>();
            //modelBuilder.Configurations.Add(new TeamConfig());
            //modelBuilder.Entity<Team>();
            //modelBuilder.Configurations.Add(new TeamSeasonConfig());
            //modelBuilder.Entity<TeamSeason>();
            //modelBuilder.Configurations.Add(new FederationSeasonConfig());
            //modelBuilder.Entity<FederationSeason>();
            //
            //base.OnModelCreating(modelBuilder);
        }
    }
}