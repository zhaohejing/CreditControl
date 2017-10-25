using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YT.Authorizations;
using YT.Managers.Roles;
using YT.Managers.Users;
using YT.Navigations;

namespace YT
{
    /// <summary>
    /// 菜单定义上下文
    /// </summary>
  public  class MenuDefinitionProviderContext
    {
        public IMenuDefinitionManager Manager { get; private set; }

        internal MenuDefinitionProviderContext(IMenuDefinitionManager manager)
        {
            Manager = manager;
        }
    }
    /// <summary>
    /// 权限定义上下文
    /// </summary>
    public class PermissionDefinitionProviderContext
    {
        public IPermissionDefinitionManager Manager { get; private set; }

        internal PermissionDefinitionProviderContext(IPermissionDefinitionManager manager)
        {
            Manager = manager;
        }
    }
    /// <summary>
    /// 角色定义上下文
    /// </summary>
    public class RoleDefinitionProviderContext
    {
        public IRoleDefinitionManager Manager { get; private set; }

        internal RoleDefinitionProviderContext(IRoleDefinitionManager manager)
        {
            Manager = manager;
        }
    }
    /// <summary>
    /// 用户定义上下文
    /// </summary>
    public class UserDefinitionProviderContext
    {
        public IUserDefinitionManager Manager { get; private set; }

        internal UserDefinitionProviderContext(IUserDefinitionManager manager)
        {
            Manager = manager;
        }
    }
}
