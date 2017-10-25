using System.Data.Entity;
using System.Reflection;
using Abp.Modules;
using Abp.Zero.EntityFramework;
using YT.EntityFramework;

namespace YT
{
    /// <summary>
    /// ef 核心初始化
    /// </summary>
    [DependsOn(typeof(AbpZeroEntityFrameworkModule), typeof(YtCoreModule),typeof(YtModel))]
    public class YtDataModule : AbpModule
    {
        /// <summary>
        /// 预热
        /// </summary>
        public override void PreInitialize()
        {
            //创建db
            Database.SetInitializer(new CreateDatabaseIfNotExists<MilkDbContext>());

            //web.config (or app.config for non-web projects) file should contain a connection string named "Default".
            Configuration.DefaultNameOrConnectionString = "Default";
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
