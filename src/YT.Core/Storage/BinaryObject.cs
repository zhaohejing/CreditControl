using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp;
using Abp.Domain.Entities;

namespace YT.Storage
{
    /// <summary>
    /// 文件存储
    /// </summary>
    [Table("yt_objectstorage")]
    public sealed class BinaryObject : Entity<Guid>
    {
        /// <summary>
        /// 文件路径
        /// </summary>
        [Required]
        public string Url { get; set; }
        /// <summary>
        /// 文件尺寸
        /// </summary>
        public  int? Size { get; set; }
        /// <summary>
        /// 后缀名
        /// </summary>
        public  string Suffix { get; set; }


        public BinaryObject()
        {
            Id = SequentialGuidGenerator.Instance.Create();
        }

        public BinaryObject(Guid id, string url)
        {
            Id = id;
            Url = url;
        }

        public BinaryObject(string url)
            : this()
        {
            Url = url;
        }
    }
}
