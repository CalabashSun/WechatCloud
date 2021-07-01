using Autofac;
using WechatCloud.Core.Caching;
using WechatCloud.Core.Configuration;
using WechatCloud.Core.Helper;
using WechatCloud.Core.Infrastructure;
using WechatCloud.Core.Infrastructure.DependencyManagement;
using WechatCloud.Core.Redis;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;

namespace WechatCloud.Web.Framework.Infrastructure
{
    /// <summary>
    /// Dependency registrar
    /// </summary>
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
            //file provider
            builder.RegisterType<ProjectFileProvider>().As<IProjectFileProvider>().InstancePerLifetimeScope();
            builder.RegisterType<WebHelper>().As<IWebHelper>().InstancePerLifetimeScope();
            builder.RegisterType<RedisConnectionWrapper>()
                    .As<ILocker>()
                    .As<IRedisConnectionWrapper>()
                    .SingleInstance();
            builder.RegisterType<RedisCacheManager>().As<IStaticCacheManager>().InstancePerLifetimeScope();

            builder.RegisterType<ActionContextAccessor>().As<IActionContextAccessor>().InstancePerLifetimeScope();
            
        }


        /// <summary>
        /// Gets order of this dependency registrar implementation
        /// </summary>
        public int Order => 0;
    }
}
