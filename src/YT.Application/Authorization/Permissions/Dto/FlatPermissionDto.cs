using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using YT.Authorizations;

namespace YT.Authorization.Permissions.Dto
{
    /// <summary>
    /// 权限dto
    /// </summary>
    [AutoMapFrom(typeof(YtPermission))]
    public class FlatPermissionDto:EntityDto
    { 
     
        
       /// <summary>
       /// 显示名
       /// </summary>
        public string DisplayName { get; set; }
        /// <summary>
        /// 唯一名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 上级名称
        /// </summary>
        public virtual string ParentName { get; set; }
        /// <summary>
        /// 父级id
        /// </summary>
        public int? ParentId { get; set; }
        /// <summary>
        /// level
        /// </summary>
        public string LevelCode { get; set; }
   
    }
}