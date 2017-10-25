using System;
using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using YT.Managers.Users;

namespace YT.Authorization.Users.Dto
{/// <summary>
 /// 用户列表dto
 /// </summary>
    [AutoMapFrom(typeof(User))]
    public class UserListDto : EntityDto<long>, IPassivable, IHasCreationTime
    {/// <summary>
     /// 姓名
     /// </summary>
        public string Name { get; set; }
  
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
     
        /// <summary>
        /// 手机
        /// </summary>
        public string PhoneNumber { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        public Guid? ProfilePictureId { get; set; }
 
        /// <summary>
        /// 角色集合
        /// </summary>
        public List<UserListRoleDto> Roles { get; set; }
        /// <summary>
        /// 最后登录时间
        /// </summary>
        public DateTime? LastLoginTime { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsActive { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreationTime { get; set; }
        /// <summary>
        /// 用户角色dto
        /// </summary>
        [AutoMapFrom(typeof(UserRole))]
        public class UserListRoleDto
        {/// <summary>
         /// 角色id
         /// </summary>
            public int RoleId { get; set; }
            /// <summary>
            /// 角色显示名
            /// </summary>
            public string RoleName { get; set; }
        }
    }
}