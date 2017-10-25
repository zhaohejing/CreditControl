using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YT.Configuration.Dto
{
    /// <summary>
    /// 权限设置
    /// </summary>
  public  class PermissionSettingsEditDto
    {
        /// <summary>
        /// 添加
        /// </summary>
        public bool Create { get; set; } = true;

        /// <summary>
        /// 编辑
        /// </summary>
        public bool Edit { get; set; } = true;
        /// <summary>
        /// 删除
        /// </summary>
        public bool Delete { get; set; } = true;
        /// <summary>
        /// 分配
        /// </summary>
        public bool Allow { get; set; } = true;
    }
}
