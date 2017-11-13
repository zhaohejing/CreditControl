using System.Collections.Generic;

namespace YT.CustomerPreferencePrices.Dtos
{
    /// <summary>
    /// 用户折扣价格设置新增和编辑时用Dto
    /// </summary>

    public class ModifyCustomerPreferencePriceInput
    {
        /// <summary>
        /// 用户折扣价格设置编辑Dto
        /// </summary>
        public List<CustomerPreferencePriceEditDto> CustomerPrices{ get; set; }

    }
}
