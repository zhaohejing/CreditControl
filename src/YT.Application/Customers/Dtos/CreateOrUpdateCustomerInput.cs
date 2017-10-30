using Abp.Application.Services.Dto;

namespace YT.Customers.Dtos
{
    /// <summary>
    /// 客户信息新增和编辑时用Dto
    /// </summary>
    
    public class CreateOrUpdateCustomerInput  
    {
    /// <summary>
    /// 客户信息编辑Dto
    /// </summary>
		public CustomerEditDto  CustomerEditDto {get;set;}
 
    }
    /// <summary>
    /// 充值实体
    /// </summary>
    public class ChargeInput : EntityDto
    {
        /// <summary>
        /// 金额
        /// </summary>
        public int Money { get; set; }
    }
    /// <summary>
    /// 审核dto
    /// </summary>
    public class AuditInput : EntityDto
    {
        /// <summary>
        /// 状态
        /// </summary>
        public bool State { get; set; }
        /// <summary>
        /// 意见
        /// </summary>
        public string Opinion { get; set; }
    }
    /// <summary>
    /// 种植密码
    /// </summary>
    public class ResetInput : EntityDto
    {
       
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
    }
}
