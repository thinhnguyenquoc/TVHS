using Autofac;
using TVHS.Repositories;
using TVHS.Repositories.Interfaces;
using TVHS.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;


namespace TVHS.Services
{
    public class AutofacMapper : Module
    {
        private string connStr;
        public AutofacMapper(string connString)
        {
            this.connStr = connString;
        }
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new TVHSContext()).As<TVHSContext>().InstancePerRequest();
            builder.Register(c => log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType)).As<ILog>().InstancePerRequest();
            // repository
            builder.RegisterType<LevelRepository>().As<ILevelRepository>().InstancePerRequest();
            builder.RegisterType<ProgramRepository>().As<IProgramRepository>().InstancePerRequest();
            builder.RegisterType<SaleRepository>().As<ISaleRepository>().InstancePerRequest();
            builder.RegisterType<ScheduleRepository>().As<IScheduleRepository>().InstancePerRequest();
            builder.RegisterType<TimeSettingRepository>().As<ITimeSettingRepository>().InstancePerRequest();
            builder.RegisterType<ProductRepository>().As<IProductRepository>().InstancePerRequest();
            builder.RegisterType<CategoryRepository>().As<ICategoryRepository>().InstancePerRequest();
            // service
            builder.RegisterType<ProductService>().As<IProductService>().InstancePerRequest();
            builder.RegisterType<ProgramService>().As<IProgramService>().InstancePerRequest();
            builder.RegisterType<ScheduleService>().As<IScheduleService>().InstancePerRequest();
            builder.RegisterType<SaleService>().As<ISaleService>().InstancePerRequest();
            builder.RegisterType<Helper>().As<IHelper>().InstancePerRequest();
            builder.RegisterType<PredictionService>().As<IPredictionService>().InstancePerRequest();
            builder.RegisterType<MakeScheduleService>().As<IMakeScheduleService>().InstancePerRequest();
            base.Load(builder);
        }
    }
}
