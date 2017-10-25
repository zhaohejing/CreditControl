using System;
using Abp.Application.Services.Dto;
using Abp.Authorization.Users;
using Abp.AutoMapper;

namespace YT.Authorization.Users.Dto
{/// <summary>
/// �û���¼��Ϣdto
/// </summary>

    [AutoMap(typeof(UserLoginAttempt))]
    public class UserLoginAttemptDto
    {/// <summary>
    /// �⻧��
    /// </summary>
        public string TenancyName { get; set; }
        /// <summary>
        /// �û�
        /// </summary>
        public string UserNameOrEmail { get; set; }
        /// <summary>
        /// ip
        /// </summary>
        public string ClientIpAddress { get; set; }
        /// <summary>
        /// �ͻ�����
        /// </summary>
        public string ClientName { get; set; }
        /// <summary>
        /// �������Ϣ
        /// </summary>
        public string BrowserInfo { get; set; }
        /// <summary>
        /// ���
        /// </summary>
        public string Result { get; set; }
        /// <summary>
        /// ʱ��
        /// </summary>
        public DateTime CreationTime { get; set; }
    }
}
