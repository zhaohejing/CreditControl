using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;

namespace YT.Handlers
{
   public interface ITreeLevel:IEntity
    {
        /// <summary>
        /// 上级菜单
        /// </summary>
         int? ParentId { get; set; }
        /// <summary>
        /// 编码
        /// </summary>
         string LevelCode { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        int Sort { get; set; }
    }
}
