using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TVHS.Entities.Mapping
{
    public class LevelMap : EntityTypeConfiguration<Level>
    {
        public LevelMap()
        {
            this.ToTable("Level");
            this.HasKey(x => x.Id);
            this.Property(x => x.Name).HasColumnName("Name");
            this.Property(x => x.Min).HasColumnName("Min");
            this.Property(x => x.Max).HasColumnName("Max");
        }
    }
}
