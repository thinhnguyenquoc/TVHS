using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TVHS.Entities.Mapping
{
    public class TimeFrameMap : EntityTypeConfiguration<TimeFrame>
    {
        public TimeFrameMap()
        {
            this.ToTable("TimeFrame");
            this.HasKey(x => x.Id);
            this.Property(x => x.Name).HasColumnName("Name");
            this.Property(x => x.Begin).HasColumnName("Begin");
            this.Property(x => x.End).HasColumnName("End");
            this.Property(x => x.Type).HasColumnName("Type");
        }
    }
}
