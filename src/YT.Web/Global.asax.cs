using System;
using System.Globalization;
using System.Web;
using Abp.Castle.Logging.Log4Net;
using Abp.Configuration;
using Abp.Localization;
using Abp.Logging;
using Abp.Timing;
using Abp.Extensions;
using Abp.Web;
using Castle.Facilities.Logging;

namespace YT.Web
{
    public class MvcApplication : AbpWebApplication<AbpZeroTemplateWebModule>
    {
        protected override void Application_Start(object sender, EventArgs e)
        {
            //Use UTC clock. Remove this to use local time for your application.
            Clock.Provider = ClockProviders.Local;

            //Log4Net configuration
            AbpBootstrapper.IocManager.IocContainer
                .AddFacility<LoggingFacility>(f => f.UseAbpLog4Net()
                    .WithConfig("log4net.config")
                );

            base.Application_Start(sender, e);
        }

        protected override void Session_Start(object sender, EventArgs e)
        {
           // RestoreUserLanguage();
            base.Session_Start(sender, e);
        }


        /* Preventing client side cache */
        private static readonly DateTime CacheExpireDate = new DateTime(2000, 1, 1);
        protected override void Application_BeginRequest(object sender, EventArgs e)
        {
            base.Application_BeginRequest(sender, e);
            DisableClientCache();
        }

        private void DisableClientCache()
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(CacheExpireDate);
            Response.Cache.SetNoStore();
        }
    }
}
