using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TVHS.Entities.Mapping
{
    public class SaleMap : EntityTypeConfiguration<Sale>
    {
        public SaleMap()
        {
            this.ToTable("Sale");
            this.HasKey(x => x.Id);
            this.Property(x => x.ProductCode).HasColumnName("ProductCode");
            this.Property(x => x.Quantity).HasColumnName("Quantity");
            this.Property(x => x.Date).HasColumnName("Date");
        }
    }
}
