using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YT.Managers.Roles
{
  public  class RoleDefinition
    {
        public RoleDefinition() { }

        public RoleDefinition(string name, string displayName, bool def = false, bool isstatic = false)
        {
            Name = name;
            DisplayName = displayName;
            IsDefault = def;
            IsStatic = isstatic;
        }
        /// <summary>
        /// 角色唯一名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 角色显示名称
        /// </summary>
        public string DisplayName { get; set; }
        /// <summary>
        /// 是否默认角色
        /// </summary>
        public  bool IsDefault { get; set; }
  

        /// <summary>
        /// 是否静态角色
        /// </summary>
        public bool IsStatic { get; set; }
    }
}
