using Abp.Runtime.Validation;
using YT.Dto;

namespace YT.Authorization.Users.Dto
{/// <summary>
/// 获取角色input
/// </summary>
    public class GetUsersInput : PagedAndSortedInputDto, IShouldNormalize
    {
        /// <summary>
        /// 过滤条件
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 权限过滤
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 角色过滤
        /// </summary>
        public int? Role { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public void Normalize()
        {
            if (string.IsNullOrEmpty(Sorting))
            {
                Sorting = "Name";
            }
        }
    }
}