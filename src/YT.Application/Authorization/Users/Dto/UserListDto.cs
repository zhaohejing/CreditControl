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
 /// �û��б�dto
 /// </summary>
    [AutoMapFrom(typeof(User))]
    public class UserListDto : EntityDto<long>, IPassivable, IHasCreationTime
    {/// <summary>
     /// ����
     /// </summary>
        public string Name { get; set; }
  
        /// <summary>
        /// �û���
        /// </summary>
        public string UserName { get; set; }
     
        /// <summary>
        /// �ֻ�
        /// </summary>
        public string PhoneNumber { get; set; }
        /// <summary>
        /// ͷ��
        /// </summary>
        public Guid? ProfilePictureId { get; set; }
 
        /// <summary>
        /// ��ɫ����
        /// </summary>
        public List<UserListRoleDto> Roles { get; set; }
        /// <summary>
        /// ����¼ʱ��
        /// </summary>
        public DateTime? LastLoginTime { get; set; }
        /// <summary>
        /// �Ƿ�����
        /// </summary>
        public bool IsActive { get; set; }
        /// <summary>
        /// ����ʱ��
        /// </summary>
        public DateTime CreationTime { get; set; }
        /// <summary>
        /// �û���ɫdto
        /// </summary>
        [AutoMapFrom(typeof(UserRole))]
        public class UserListRoleDto
        {/// <summary>
         /// ��ɫid
         /// </summary>
            public int RoleId { get; set; }
            /// <summary>
            /// ��ɫ��ʾ��
            /// </summary>
            public string RoleName { get; set; }
        }
    }
}