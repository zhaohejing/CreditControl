using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using YT.Handlers;

namespace YT.Navigations
{
    /// <summary>
    /// 菜单
    /// </summary>
    [Table("milk_menu")]
    public class Menu :Entity,ITreeLevel
    {
        /// <summary>
        /// 菜单名称(唯一)
        /// </summary>
        [Required,MaxLength(120)]
        public string Name { get; set; }
        /// <summary>
        /// 菜单显示名称
        /// </summary>
        [MaxLength(120)]
        public string DisplayName { get; set; }
        /// <summary>
        /// 元数据
        /// </summary>
        public string MetaData { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }
        /// <summary>
        /// 链接
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 是否需要验证
        /// </summary>
        public bool RequiresAuthentication { get; set; }
        /// <summary>
        /// 需要的权限名称
        /// </summary>
        public string RequiredPermissions { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }
        /// <summary>
        /// 是否系统
        /// </summary>
        public bool IsStatic { get; set; }
        /// <summary>
        /// 是否激活
        /// </summary>
        public bool IsActive { get; set; }
        /// <summary>
        /// 上级菜单
        /// </summary>
        public virtual Menu Parent { get; set; }
        /// <summary>
        /// 上级菜单
        /// </summary>
        public int? ParentId { get; set; }
        /// <summary>
        /// 编码
        /// </summary>
        public string  LevelCode { get; set; }
        /// <summary>
        /// 下级
        /// </summary>
        [ForeignKey("ParentId")]
        public  virtual  ICollection<Menu> Childs { get; set; }
    }
}
