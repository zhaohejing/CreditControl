using System.ComponentModel.DataAnnotations.Schema;
using Abp.Authorization.Roles;
using Abp.Domain.Entities;
using YT.Managers.Users;

namespace YT.Managers.Roles
{
    /// <summary>
    /// ��ɫ��Ϣ��
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
        /// �Ƿ�����
        /// </summary>
        public bool IsActive { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        public string Description { get; set; }
    }
}
