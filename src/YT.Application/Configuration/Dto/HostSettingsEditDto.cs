using System.ComponentModel.DataAnnotations;
using Abp.Authorization;

namespace YT.Configuration.Dto
{/// <summary>
/// 
/// </summary>
    public class SettingsEditDto
    {
        /// <summary>
        /// Ȩ������
        /// </summary>
        public PermissionSettingsEditDto Permission { get; set; }
    }
}