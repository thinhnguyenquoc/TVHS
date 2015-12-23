using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TVHS.Entities.Mapping
{
    public class CycleMap : EntityTypeConfiguration<Cycle>
    {
        public CycleMap()
        {
            this.ToTable("Cycle");
            this.HasKey(x => x.Id);
            this.Property(x => x.Begin).HasColumnName("Begin");
            this.Property(x => x.End).HasColumnName("End");
        }
    }
}
