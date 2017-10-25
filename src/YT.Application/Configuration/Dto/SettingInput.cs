using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.Configuration;
using Castle.Windsor.Configuration.Interpreters.XmlProcessor.ElementProcessors;

namespace YT.Configuration.Dto
{
    /// <summary>
    /// 添加配置项
    /// </summary>
    [AutoMap(typeof(Setting))]
    public class SettingInput
    {
        /// <summary>
        /// 配置名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 用户id
        /// </summary>
        public long? UserId { get; set; }
        /// <summary>
        /// 设置值
        /// </summary>
        public virtual string Value { get; set; }
        /// <summary>
        /// key
        /// </summary>
        public int? Id { get; set; }

    }
}
