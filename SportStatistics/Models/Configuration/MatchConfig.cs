using System.Data.Entity.ModelConfiguration;

namespace WebApplication1.Models.Configuration
{
    public class MatchConfig : EntityTypeConfiguration<Match>
    {
        public MatchConfig()
        {
            Property(p => p.TournamentString).HasColumnName("Tournament");
            Ignore(p => p.Tournament);
            Property(p => p.TournamentString).HasColumnName("HomeTeamResult");
            Ignore(p => p.HomeTeamResult);
            Property(p => p.TournamentString).HasColumnName("AwayTeamResult");
            Ignore(p => p.AwayTeamResult);
            Property(p => p.TournamentString).HasColumnName("NameSport");
            Ignore(p => p.NameSport);
            Property(p => p.Country).IsRequired().HasMaxLength(50);
            Property(p => p.Tour).IsRequired().HasMaxLength(30);
            Property(p => p.Date).IsRequired();
            Property(p => p.NameStadium).IsRequired().HasMaxLength(30);
            Property(p => p.HomeTeam).IsRequired().HasMaxLength(30);
            Property(p => p.AwayTeam).IsRequired().HasMaxLength(30);
            Property(p => p.HomeTeamGoal).IsRequired();
            Property(p => p.AwayTeamGoal).IsRequired();            
        }
    }
}