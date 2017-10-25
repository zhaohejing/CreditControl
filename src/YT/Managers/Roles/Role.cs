using System.ComponentModel.DataAnnotations.Schema;
using Abp.Authorization.Roles;
using Abp.Domain.Entities;
using YT.Managers.Users;

namespace YT.Managers.Roles
{
    /// <summary>
    /// 角色信息表
    /// </summary>
    [Table("milk_roles")]
    public class Role : AbpRole<User>, IPassivable
    {
        //Can add application specific role properties here

        public Role()
        {

        }

        public Role(int? tenantId, string displayName)
            : base(tenantId, displayName)
        {

        }

        public Role(int? tenantId, string name, string displayName)
            : base(tenantId, name, displayName)
        {

        }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsActive { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
    }
}
