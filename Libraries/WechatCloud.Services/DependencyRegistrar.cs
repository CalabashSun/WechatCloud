using Autofac;
using WechatCloud.Core.Configuration;
using WechatCloud.Core.DbContext;
using WechatCloud.Core.Infrastructure;
using WechatCloud.Core.Infrastructure.DependencyManagement;
using WechatCloud.Services.Repositorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WechatCloud.Services.ModelServices;

namespace WechatCloud.Services
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        /// <summary>
        /// Register services and interfaces
        /// </summary>
        /// <param name="builder">Container builder</param>
        /// <param name="typeFinder">Type finder</param>
        /// <param name="appSettings">App settings</param>
        public virtual void Register(ContainerBuilder builder, ITypeFinder typeFinder, AppSettings appSettings)
        {
            //services
            builder.RegisterType<DapperDbContext>().As<IDbContext>().InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();


            builder.RegisterType<T_TOKENServices>().As<IT_TOKENServices>().InstancePerLifetimeScope();
            builder.RegisterType<T_FoodPicServices>().As<IT_FoodPicServices>().InstancePerLifetimeScope();
            builder.RegisterType<T_ShopCPServices>().As<IT_ShopCPServices>().InstancePerLifetimeScope();
            builder.RegisterType<T_ShopDocIdServices>().As<IT_ShopDocIdServices>().InstancePerLifetimeScope();
        }


        /// <summary>
        /// Gets order of this dependency registrar implementation
        /// </summary>
        public int Order => 0;
    }
}
