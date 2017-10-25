using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YT.Dto
{
 
    /// <summary>
    /// 字典dto
    /// </summary>
    public class DictionaryDto
    {
        /// <summary>
        /// 
        /// </summary>
        public DictionaryDto() { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="dis"></param>
        public DictionaryDto(string name, string dis)
        {
            Name = name;
            DisplayName = dis;
        }

        /// <summary>
        /// 是否选中
        /// </summary>
        public bool IsCheck { get; set; } = false;
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 显示名
        /// </summary>
        public string DisplayName { get; set; }
    }
}
