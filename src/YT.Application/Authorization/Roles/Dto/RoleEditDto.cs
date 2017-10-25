using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;
using YT.Managers.Roles;

namespace YT.Authorization.Roles.Dto
{/// <summary>
 /// 
 /// </summary>
    [AutoMap(typeof(Role))]
    public class RoleEditDto
    {/// <summary>
     /// key
     /// </summary>
        public int? Id { get; set; }
        /// <summary>
        /// ��ʾ��
        /// </summary>
        [Required]
        public string DisplayName { get; set; }
        /// <summary>
        /// �Ƿ�Ĭ��
        /// </summary>
        public bool IsDefault { get; set; }
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