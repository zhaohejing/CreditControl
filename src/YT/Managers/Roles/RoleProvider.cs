using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Dependency;
using YT.Navigations;

namespace YT.Managers.Roles
{
   public abstract class RoleProvider : ITransientDependency
    {
        /// <summary>
        /// 获取此提供程序所提供的所有角色定义。
        /// </summary>
        /// <returns>List of settings</returns>
        public abstract IEnumerable<RoleDefinition> GetRoleDefinitions(RoleDefinitionProviderContext context);
    }
}
