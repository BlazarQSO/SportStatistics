using System.Data.Entity.ModelConfiguration;

namespace WebApplication1.Models.Configuration
{
    public class TeamSeasonConfig : EntityTypeConfiguration<TeamSeason>
    {
        public TeamSeasonConfig()
        {
            Property(p => p.TournamentString).HasColumnName("Tournament");
            Ignore(p => p.Tournament);
            Property(p => p.Point).IsRequired();
            Property(p => p.Lose).IsRequired();
            Property(p => p.Win).IsRequired();
            Property(p => p.Draw).IsRequired();
            Property(p => p.Goals).IsRequired();
            Property(p => p.GoalAgainst).IsRequired();
            Property(p => p.HomePoint).IsRequired();
            Property(p => p.HomeWin).IsRequired();
            Property(p => p.HomeLose).IsRequired();
            Property(p => p.HomeDraw).IsRequired();
            Property(p => p.HomeGoals).IsRequired();
            Property(p => p.HomeGoalAgainst).IsRequired();            
        }
    }
}