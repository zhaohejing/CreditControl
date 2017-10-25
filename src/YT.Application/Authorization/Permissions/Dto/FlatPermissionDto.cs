using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using YT.Authorizations;

namespace YT.Authorization.Permissions.Dto
{
    /// <summary>
    /// Ȩ��dto
    /// </summary>
    [AutoMapFrom(typeof(YtPermission))]
    public class FlatPermissionDto:EntityDto
    { 
     
        
       /// <summary>
       /// ��ʾ��
       /// </summary>
        public string DisplayName { get; set; }
        /// <summary>
        /// Ψһ��
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// �ϼ�����
        /// </summary>
        public virtual string ParentName { get; set; }
        /// <summary>
        /// ����id
        /// </summary>
        public int? ParentId { get; set; }
        /// <summary>
        /// level
        /// </summary>
        public string LevelCode { get; set; }
   
    }
}