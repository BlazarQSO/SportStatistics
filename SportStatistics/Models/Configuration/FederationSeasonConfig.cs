using System.Data.Entity.ModelConfiguration;

namespace SportStatistics.Models.Configuration
{
    public class FederationSeasonConfig : EntityTypeConfiguration<FederationSeason>
    {
        public FederationSeasonConfig()
        {
            Property(p => p.TournamentString).HasColumnName("Tournament");
            Ignore(p => p.Tournament);
            Property(p => p.NameTournament).IsRequired().HasMaxLength(50);
        }
    }
}