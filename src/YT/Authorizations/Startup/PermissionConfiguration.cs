using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Collections;
using YT.Navigations;

namespace YT.Authorizations.Startup
{
   public class PermissionConfiguration: IPermissionConfiguration
    {
        public ITypeList<PermissionProvider> Providers { get;  }

        public PermissionConfiguration()
        {
            Providers = new TypeList<PermissionProvider>();
        }
    }
    /// <summary>
    /// 菜单配置
    /// </summary>
    public interface IPermissionConfiguration
    {
        /// <summary>
        /// 菜单数据源
        /// </summary>
        ITypeList<PermissionProvider> Providers { get; }
    }
}
