using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using Abp.Organizations;

namespace YT.Organizations
{
    /// <summary>
    /// 组织机构
    /// </summary>
  public   class Organization: OrganizationUnit,IPassivable
    {
        public Organization() { }

        public Organization(int? tenantId, string displayName, long? parentId = null)
            : base(tenantId, displayName, parentId)
        {
            
        }
        /// <summary>
        /// 组织机构类型
        /// </summary>
        public OrganizationType Type { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }
        /// <summary>
        /// 是否系统
        /// </summary>
        public bool IsStatic { get; set; }
        /// <summary>
        /// 启用禁用
        /// </summary>
        public bool IsActive { get; set; }
    }
    
}
