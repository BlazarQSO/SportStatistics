using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using WebApplication1.Models.Configuration;


namespace WebApplication1.Models
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
        public DbSet<SportFederation> SportFederation { get; set; }
        public DbSet<Sport> Sports { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);                        
        }
    }
}