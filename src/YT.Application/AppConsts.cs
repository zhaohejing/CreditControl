using System.Collections.Generic;
using YT.Dto;

namespace YT
{
    /// <summary>
    /// 应用层常量定义
    /// </summary>
    public class AppConsts
    {
        /// <summary>
        /// Default page size for paged requests.
        /// </summary>
        public const int DefaultPageSize = 10;

        /// <summary>
        /// Maximum allowed page size for paged requests.
        /// </summary>
        public const int MaxPageSize = 1000;
        /// <summary>
        /// 静态权限类型
        /// </summary>
        public static List<DictionaryDto> StaticPermissions =>

            new List<DictionaryDto>()
            {
                new DictionaryDto("create","新增"),
                new DictionaryDto("edit","编辑"),
                new DictionaryDto("delete","删除"),
                new DictionaryDto("chart","图表"),
                new DictionaryDto("auth","授权"),
            };
   
    }
    /// <summary>
    /// 缓存定义
    /// </summary>
    public class CacheName
    {
        /// <summary>
        /// 菜单权限
        /// </summary>
        public const string MenuCache = "Milk.Cache.Menu";
        /// <summary>
        /// 微信token
        /// </summary>
        public const string WeChatToken = "Milk.WeChat.ACCESS_TOKEN";
        /// <summary>
        /// 权限const
        /// </summary>
        public const string PermissionCache = "Milk.Cache.Permission";
    }
}
