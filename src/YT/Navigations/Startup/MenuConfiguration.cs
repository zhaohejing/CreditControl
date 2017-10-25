using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Collections;

namespace YT.Navigations.Startup
{
    internal class MenuConfiguration : IMenuConfiguration
    {
        public ITypeList<MenuProvider> Providers { get; private set; }

        public MenuConfiguration()
        {
            Providers = new TypeList<MenuProvider>();
        }
    }
    /// <summary>
    /// 菜单配置
    /// </summary>
    public interface IMenuConfiguration
    {
        /// <summary>
        /// 菜单数据源
        /// </summary>
        ITypeList<MenuProvider> Providers { get; }
    }
}
