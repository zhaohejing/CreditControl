using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YT.Navigations.Dtos
{
    /// <summary>
    /// 菜单移动input
    /// </summary>
  public  class MoveMenuInput
    {
        /// <summary>
        /// 菜单id
        /// </summary>
        public int MenuId { get; set; }
        /// <summary>
        /// 父id
        /// </summary>
        public int? ParentId { get; set; }
    }
}
