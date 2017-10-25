using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Features;
using Abp.Authorization;
using Abp.Localization;
using Abp.MultiTenancy;
using Castle.Components.DictionaryAdapter;

namespace YT.Authorizations
{
    public class PermissionDefinition
    {
        public PermissionDefinition() { }
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="name"></param>
        /// <param name="displayName"></param>
        /// <param name="description"></param>
        /// <param name="type"></param>
        public PermissionDefinition(string name, 
            string displayName = null,
            string description = null,
            PermissionType type = PermissionType.Common)
        {
            Name = name;
            PermissionType = type;
            DisplayName = displayName;
            Description = description;
            Childs = new List<PermissionDefinition>();
        }

       /// <summary>
       /// 子权限
       /// </summary>
        public List<PermissionDefinition> Childs { get; set; }
       /// <summary>
       /// 描述
       /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 显示名
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 权限类型
        /// </summary>
        public PermissionType PermissionType { get; set; }
        /// <summary>
        /// 唯一名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 父id
        /// </summary>
        public int? ParentId { get; set; }
        /// <summary>
        /// 是否系统
        /// </summary>
        public bool IsStatic { get; set; }
        /// <summary>
        /// 是否激活
        /// </summary>
        public bool IsActive { get; set; }

    }
    /// <summary>
    /// 权限类型
    /// </summary>
    public enum PermissionType
    {
        /// <summary>
        /// 普通
        /// </summary>
        Common = 0,
        /// <summary>
        /// 管理权限
        /// </summary>
        Control = 1,
        /// <summary>
        /// 业务权限
        /// </summary>
        Operation = 2
    }
}
