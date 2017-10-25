using System.Collections.Generic;
using YT.Dto;

namespace YT
{
    /// <summary>
    /// Ӧ�ò㳣������
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
        /// ��̬Ȩ������
        /// </summary>
        public static List<DictionaryDto> StaticPermissions =>

            new List<DictionaryDto>()
            {
                new DictionaryDto("create","����"),
                new DictionaryDto("edit","�༭"),
                new DictionaryDto("delete","ɾ��"),
                new DictionaryDto("chart","ͼ��"),
                new DictionaryDto("auth","��Ȩ"),
            };
   
    }
    /// <summary>
    /// ���涨��
    /// </summary>
    public class CacheName
    {
        /// <summary>
        /// �˵�Ȩ��
        /// </summary>
        public const string MenuCache = "Milk.Cache.Menu";
        /// <summary>
        /// ΢��token
        /// </summary>
        public const string WeChatToken = "Milk.WeChat.ACCESS_TOKEN";
        /// <summary>
        /// Ȩ��const
        /// </summary>
        public const string PermissionCache = "Milk.Cache.Permission";
    }
}
