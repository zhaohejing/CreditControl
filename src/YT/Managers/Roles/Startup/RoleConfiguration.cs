using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Collections;
using YT.Authorizations;

namespace YT.Managers.Roles.Startup
{
   public class RoleConfiguration: IRoleConfiguration
    {
        public ITypeList<RoleProvider> Providers { get; }

        public RoleConfiguration()
        {
            Providers = new TypeList<RoleProvider>();
        }
    }
    /// <summary>
    /// 菜单配置
    /// </summary>
    public interface IRoleConfiguration
    {
        /// <summary>
        /// 菜单数据源
        /// </summary>
        ITypeList<RoleProvider> Providers { get; }
    }
}
