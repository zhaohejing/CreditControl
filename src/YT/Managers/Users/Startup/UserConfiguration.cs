using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Collections;
using YT.Authorizations;

namespace YT.Managers.Users.Startup
{
   public class UserConfiguration: IUserConfiguration
    {
        public ITypeList<UserProvider> Providers { get;  }

        public UserConfiguration()
        {
            Providers = new TypeList<UserProvider>();
        }
    }
    /// <summary>
    /// 菜单配置
    /// </summary>
    public interface IUserConfiguration
    {
        /// <summary>
        /// 菜单数据源
        /// </summary>
        ITypeList<UserProvider> Providers { get; }
    }
}
