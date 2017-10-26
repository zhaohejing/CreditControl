using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace YT.Models
{
    /// <summary>
    /// 客户信息
    /// </summary>
    [Table("customer")]
   public class Customer:CreationAuditedEntity,ISoftDelete,IPassivable
    {
        public string CompanyName { get; set; }
        public string Provence { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        public string Contact { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string Mobile { get; set; }

        public string Email { get; set; }
        public string Account { get; set; }

        public string Password { get; set; }
        /// <summary>
        /// 营业执照
        /// </summary>
        public Guid? License { get; set; }
        /// <summary>
        /// 身份证正面
        /// </summary>
        public Guid? FaceIdentityCard { get; set; }
        /// <summary>
        /// 身份证背面
        /// </summary>
        public Guid? BackIdentityCard { get; set; }

        /// <summary>
        /// 余额
        /// </summary>
        public int Balance { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public bool? State { get; set; }

        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
    }
}
