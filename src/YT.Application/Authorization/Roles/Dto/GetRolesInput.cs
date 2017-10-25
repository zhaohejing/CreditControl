using Abp.Application.Services.Dto;
using Abp.Runtime.Validation;
using YT.Dto;

namespace YT.Authorization.Roles.Dto
{/// <summary>
/// 获取角色input
/// </summary>
    public class GetRolesInput : PagedAndSortedInputDto, IShouldNormalize
    {/// <summary>
     /// 权限过滤条件
     /// </summary>
        public string Permission { get; set; }
        /// <summary>
        /// 过滤条件
        /// </summary>
        public string Filter { get; set; }
        /// <summary>
        /// 初始化
        /// </summary>
        public void Normalize()
        {
            if (string.IsNullOrEmpty(Sorting))
            {
                Sorting = "Id";
            }
        }
    }
}
