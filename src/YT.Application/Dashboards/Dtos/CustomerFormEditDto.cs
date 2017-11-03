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
        /// 品牌负责人
        /// </summary>
        public string BrandsPerson { get; set; }
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
        /// 近三年描述
        /// </summary>
        public string MajorSecret { get; set; }
        /// <summary>
        /// 营业执照
        /// </summary>
        public Guid? License { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string LicenseUrl { get; set; }
        /// <summary>
        /// 身份证
        /// </summary>
        public Guid? IdentityCard { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string IdentityCardUrl { get; set; }
        /// <summary>
        /// 许可正
        /// </summary>
        public Guid? PermitCard { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string PermitCardUrl { get; set; }
        /// <summary>
        /// 公司logo
        /// </summary>
        public Guid? CompanyLogo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string CompanyLogoUrl { get; set; }
        /// <summary>
        /// 公司概述
        /// </summary>
        public string CompanyOverview { get; set; }
        /// <summary>
        /// 公司发展历程
        /// </summary>
        public string CompanyHistory { get; set; }
        /// <summary>
        /// 领导人履历
        /// </summary>
        public string LeadershipResume { get; set; }
       
        /// <summary>
        /// 公司产品
        /// </summary>
        public string CompanyProduct { get; set; }
        /// <summary>
        /// 相关专利
        /// </summary>
        public string RelevantPatent { get; set; }
        /// <summary>
        /// 公司个人荣誉
        /// </summary>
        public string Companyhonor { get; set; }
        /// <summary>
        /// 公益事业
        /// </summary>
        public string PublicWelfareUndertakings { get; set; }
        /// <summary>
        /// 其他说名
        /// </summary>
        public string Other { get; set; }

    }
}
