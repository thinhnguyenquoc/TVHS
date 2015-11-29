using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TVHS.Entities.Mapping
{
    public class ProgramMap : EntityTypeConfiguration<Program>
    {
        public ProgramMap()
        {
            this.ToTable("Program");
            this.HasKey(x => x.Id);
            this.Property(x => x.Name).HasColumnName("Name");
            this.Property(x => x.Note).HasColumnName("Note");
            this.Property(x => x.Price).HasColumnName("Price");
            this.Property(x => x.Category).HasColumnName("Category");
            this.Property(x => x.ProgramCode).HasColumnName("ProgramCode");
            this.Property(x => x.Duration).HasColumnName("Duration");
            this.Property(x => x.ProductId).HasColumnName("ProductId");
        }
    }
}
