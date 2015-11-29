using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TVHS.Entities.Mapping
{
    public class TimeSettingMap : EntityTypeConfiguration<TimeSetting>
    {
        public TimeSettingMap()
        {
            this.ToTable("TimeSetting");
            this.HasKey(x => x.Id);
            this.Property(x => x.Time).HasColumnName("Time");
        }
    }
}
