using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.AutoMapper;
using YT.Models;

namespace YT.Dashboards.Dtos
{
    /// <summary>
    /// form表单
    /// </summary>
    [AutoMap(typeof(CustomerForm))]
  public  class CustomerFormEditDto
    {
        /// <summary>
        /// key
        /// </summary>
        public int? Id { get; set; }
        /// <summary>
        /// 客户id
        /// </summary>
        public int CustomerId { get; set; }
        /// <summary>
        /// 订单id
        /// </summary>
        public int OrderId { get; set; }
        /// <summary>
        /// 企业名
        /// </summary>
        public string CompanyName { get; set; }
        /// <summary>
        /// 行业
        /// </summary>
        public string Industry { get; set; }
        /// <summary>
        /// 品牌
        /// </summary>
        public string Brands { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 法人
        /// </summary>
        public string LegalPerson { get; set; }
        /// <summary>
        /// 法人手机
        /// </summary>
        public string LegalMobile { get; set; }
        /// <summary>
        /// 显示路径
        /// </summary>
        public string LicenseUrl { get; set; }

        /// <summary>
        /// 显示路径
        /// </summary>
        public string TopIdCardUrl { get; set; }
     
        /// <summary>
        /// 显示路径
        /// </summary>
        public string BottomIdCardUrl { get; set; }
        /// <summary>
        /// 品牌负责人
        /// </summary>
        public string BrandsPerson { get; set; }
        /// <summary>
        /// 营业执照
        /// </summary>
        public Guid? License { get; set; }
        /// <summary>
        /// 身份证正面
        /// </summary>
        public Guid? TopIdCard { get; set; }
        /// <summary>
        /// 背面
        /// </summary>
        public Guid? BottomIdCard { get; set; }
        /// <summary>
        /// 品牌负责人手机
        /// </summary>
        public string BrandsMobile { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 邮编
        /// </summary>
        public string PostNum { get; set; }
        /// <summary>
        /// 图片集合
        /// </summary>
        public virtual ICollection<FormProfileDto> FormProfiles { get; set; }
    }
    /// <summary>
    /// 附件dto
    /// </summary>
    [AutoMap(typeof(FormProfile))]
    public class FormProfileDto
    {
        /// <summary>
        /// 附件信息
        /// </summary>
        public Guid ProfileId { get; set; }
        /// <summary>
        /// 路径
        /// </summary>
        public string ProfileUrl { get; set; }
        /// <summary>
        /// url
        /// </summary>
        public string ProfileName { get; set; }
        /// <summary>
        /// 文件类型
        /// </summary>
        public ProfileType ProfileType { get; set; }
    }
}
