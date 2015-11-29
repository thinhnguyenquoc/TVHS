using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TVHS.Entities.Mapping
{
    public class ScheduleMap : EntityTypeConfiguration<Schedule>
    {
        public ScheduleMap()
        {
            this.ToTable("Schedule");
            this.HasKey(x => x.Id);
            this.Property(x => x.ProgramCode).HasColumnName("ProgramCode");
            this.Property(x => x.Date).HasColumnName("Date");
        }
    }
}
