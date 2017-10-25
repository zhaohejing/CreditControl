
using Abp.Owin;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using YT.Web;
using YT.WebApi.Controllers;
using Owin;

[assembly: OwinStartup(typeof(Startup))]

namespace YT.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCors(CorsOptions.AllowAll);
            app.RegisterDataProtectionProvider();
            app.UseAbp();

            app.UseOAuthBearerAuthentication(AccountController.OAuthBearerOptions);
            
            //使用cookie认证
            //app.UseCookieAuthentication(new CookieAuthenticationOptions
            //{
            //    AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
            //    LoginPath = new PathString("/Account/Login")
            //});
            //使用第三方拓展登陆
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            //You can remove these lines if you don't like to use two factor auth (while it has no problem if you don't remove)
          //  app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));
          //  app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);

           

            app.MapSignalR();

            //配置实用hangfire
            //app.UseHangfireDashboard("/hangfire", new DashboardOptions
            //{
            //    Authorization = new[] { new AbpHangfireAuthorizationFilter(AppPermissions.Pages_Administration_HangfireDashboard) }
            //});
        }

    }
}