using TVHS.Entities;
using TVHS.Entities.Mapping;
using TVHS.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TVHS.Repositories
{
    public class TVHSContext : DbContext
    {
        static TVHSContext()
        {
            Database.SetInitializer<TVHSContext>(null);
        }

        public TVHSContext()
            : base("Name=TVHSConnectionString")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<Level> Levels { get; set; }
        public DbSet<Program> Programs { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<TimeSetting> TimeSettings { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<TimeFrame> TimeFrames { get; set; }
        public DbSet<Category> Categorys { get; set; }
        public DbSet<Cycle> Cycles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new LevelMap());
            modelBuilder.Configurations.Add(new ProgramMap());
            modelBuilder.Configurations.Add(new SaleMap());
            modelBuilder.Configurations.Add(new ScheduleMap());
            modelBuilder.Configurations.Add(new TimeSettingMap());
            modelBuilder.Configurations.Add(new ProductMap());
            modelBuilder.Configurations.Add(new TimeFrameMap());
            modelBuilder.Configurations.Add(new CategoryMap());
            modelBuilder.Configurations.Add(new CycleMap());
        }
    }
}
