using System.Data.Entity.ModelConfiguration;

namespace SportStatistics.Models.Configuration
{
    public class PlayerSeasonConfig : EntityTypeConfiguration<PlayerSeason>
    {
        public PlayerSeasonConfig()
        {
            Property(p => p.TournamentString).HasColumnName("Tournament");
            Ignore(p => p.Tournament);
            Property(p => p.GamedMatches).IsRequired();
            Property(p => p.Goals).IsRequired();
            Property(p => p.Assists).IsRequired();            
        }
    }
}