using System.Data.Entity.ModelConfiguration;

namespace WebApplication1.Models.Configuration
{
    public class TeamConfig : EntityTypeConfiguration<Team>
    {
        public TeamConfig()
        {   
            Property(p => p.Name).HasMaxLength(30).IsRequired();
            Property(p => p.Country).HasMaxLength(50).IsRequired();
            Property(p => p.City).HasMaxLength(30).IsRequired();
            Property(p => p.NameStadium).HasMaxLength(30).IsRequired();
        }
    }
}