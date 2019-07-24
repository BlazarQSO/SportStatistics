using System.Data.Entity.ModelConfiguration;

namespace WebApplication1.Models.Configuration
{
    public class PlayerConfig : EntityTypeConfiguration<Player>
    {
        public PlayerConfig()
        {
            Property(p => p.PositionString).HasColumnName("Position");
            Ignore(p => p.Position);            
            Property(p => p.Surname).IsRequired().HasMaxLength(30);
            Property(p => p.Name).IsRequired().HasMaxLength(20);
            Property(p => p.Birthday).IsRequired();
            Property(p => p.Nationality).IsRequired().HasMaxLength(20);
            Property(p => p.Weight).IsRequired();
            Property(p => p.Height).IsRequired();
        }
    }
}