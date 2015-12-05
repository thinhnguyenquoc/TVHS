using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TVHS.Entities.Mapping
{
    public class CategoryMap : EntityTypeConfiguration<Category>
    {
        public CategoryMap()
        {
            this.ToTable("Category");
            this.HasKey(x => x.Id);
            this.Property(x => x.Name).HasColumnName("Name");
            this.Property(x => x.ParentId).HasColumnName("ParentId");
        }
    }
}
