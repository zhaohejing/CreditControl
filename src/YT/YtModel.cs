using System.Configuration;
using System.Reflection;
using Abp;
using Abp.AutoMapper;
using Abp.Configuration.Startup;
using Abp.Modules;
using Abp.Runtime.Caching.Redis;
using Castle.MicroKernel.Registration;
using YT.Authorizations;
using YT.Authorizations.PermissionDefault;
using YT.Authorizations.Startup;
using YT.Configuration;
using YT.Handlers;
using YT.Managers.Roles;
using YT.Managers.Roles.RoleDefaults;
using YT.Managers.Roles.Startup;
using YT.Managers.Users;
using YT.Managers.Users.Startup;
using YT.Managers.Users.UserDefaults;
using YT.Navigations;
using YT.Navigations.MenuDefault;
using YT.Navigations.Startup;

namespace YT
{
    [DependsOn(typeof(AbpKernelModule), typeof(AbpAutoMapperModule), typeof(AbpRedisCacheModule))]
    public class YtModel:AbpModule
    {
        public override void PreInitialize()
        {
            IocManager.Register<IMenuConfiguration, MenuConfiguration>();
            IocManager.Register<IRoleConfiguration, RoleConfiguration>();
            IocManager.Register<IUserConfiguration, UserConfiguration>();
            IocManager.Register<IPermissionConfiguration, PermissionConfiguration>();
            IocManager.Register<IModuleConfig, ModuleConfig>();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
            //添加设置项 默认属性
          //  Configuration.Settings.Providers.Add<YtSettingProvider>();

              Configuration.Modules.MyModule().Menus.Providers.Add(typeof(AdminMenuProvider));
              Configuration.Modules.MyModule().Permissions.Providers.Add(typeof(AdminPermissionProvider));
              Configuration.Modules.MyModule().Roles.Providers.Add(typeof(AdminRoleProvider));
              Configuration.Modules.MyModule().Users.Providers.Add(typeof(AdminUserProvider));
            //组件注册 
            IocManager.IocContainer.Register(Component.For(typeof(ILevelEntityHandler<>))
                .ImplementedBy(typeof(LevelEntityHandler<>)));

        }

        public override void PostInitialize()
        {
            var isInit = ConfigurationManager.AppSettings["IsInit"];
            if (!isInit.Equals("true")) return;
            //初始化
            IocManager.Resolve<IMenuDefinitionManager>().Initialize();
            IocManager.Resolve<IPermissionDefinitionManager>().Initialize();
            IocManager.Resolve<IRoleDefinitionManager>().Initialize();
            IocManager.Resolve<IUserDefinitionManager>().Initialize();
        }

    }
}
