using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Filters;
using Abp.Application.Services;
using Abp.Configuration.Startup;
using Abp.IO;
using Abp.Modules;
using Abp.WebApi;
using Swashbuckle.Application;
using Swashbuckle.Swagger;
using YT.MultiTenancy;

namespace YT.WebApi
{
    /// <summary>
    /// Web API layer of the application.
    /// </summary>
    [DependsOn(typeof(AbpWebApiModule), typeof(YtApplicationModule))]
    public class YtWebApiModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            //Automatically creates Web API controllers for all application services of the application
            Configuration.Modules.AbpWebApi().DynamicApiControllerBuilder
                .ForAll<IApplicationService>(typeof(YtApplicationModule).Assembly, "app")
                .Build();
            Configuration.Modules.AbpWebApi().DynamicApiControllerBuilder
          .For<ITenantAppService>("app/tenant").WithApiExplorer(false).Build();
            Configuration.Modules.AbpWebApi().HttpConfiguration.Filters.Add(new HostAuthenticationFilter("Bearer"));
            ConfigureSwaggerUi(); //Remove this line to disable swagger UI.
        }

        private void ConfigureSwaggerUi()
        {
            Configuration.Modules.AbpWebApi().HttpConfiguration
                .EnableSwagger(c =>
                {
                    c.SingleApiVersion("v1", "元图管廊api接口");
                    c.IncludeXmlComments(GetXmlCommentsPath());
                    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                    //
                    c.OperationFilter<HttpHeaderFilter>();
                })
                .EnableSwaggerUi(c =>
                {
                    c.InjectJavaScript(Assembly.GetAssembly(typeof(YtWebApiModule)), "YT.WebApi.Scripts.Swagger-Custom.js");
                });
        }
        private static string GetXmlCommentsPath()
        {
            return $"{System.AppDomain.CurrentDomain.BaseDirectory}/bin/YT.Application.XML";
        }
        public override void PostInitialize()
        {
            var server = HttpContext.Current.Server;
            var appFolders = IocManager.Resolve<AppFolders>();

            appFolders.ImageFolder = server.MapPath("~/Files/Images");
            appFolders.TempFolder = server.MapPath("~/Files/Temp");
            appFolders.LogsFolder = server.MapPath("~/App_Data/Logs");
            appFolders.TempleteFolder = server.MapPath("~/Files/Templete");

            try
            {
                DirectoryHelper.CreateIfNotExists(appFolders.TempFolder);
                DirectoryHelper.CreateIfNotExists(appFolders.ImageFolder);
                DirectoryHelper.CreateIfNotExists(appFolders.TempleteFolder);
            }
            catch
            {
                // ignored
            }
        }

    }
    public class HttpHeaderFilter : IOperationFilter
    {
        public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)

        {
            if (operation.parameters == null)
                operation.parameters = new List<Parameter>();
            var filterPipeline = apiDescription.ActionDescriptor.GetFilterPipeline(); //判断是否添加权限过滤器
            var isAuthorized = filterPipeline.Select(filterInfo => filterInfo.Instance).Any(filter => filter is IAuthorizationFilter); //判断是否允许匿名方法 
            var allowAnonymous = apiDescription.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any();
            if (isAuthorized && !allowAnonymous)
            {
                operation.parameters.Add(new Parameter
                {
                    name = "Authorization",
                    @in = "header",
                    description = "Token",
                    required = false,
                    type = "string"
                });
            }
        }
    }
}
