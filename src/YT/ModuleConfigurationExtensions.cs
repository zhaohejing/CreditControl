using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Configuration.Startup;

namespace YT
{
   public static class ModuleConfigurationExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="moduleConfigurations"></param>
        /// <returns></returns>
        public static ModuleConfig MyModule(this IModuleConfigurations moduleConfigurations)
        {
            return moduleConfigurations.AbpConfiguration.GetOrCreate("ModuleConfig",
                () => moduleConfigurations.AbpConfiguration.IocManager.Resolve<ModuleConfig>());
        }
    }
}
