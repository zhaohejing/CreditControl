using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YT.Models
{
    /// <summary>
    /// 全局静态类
    /// </summary>
    public static class MilkConsts
    {
        public static List<ChargeType> ChargeTypes =>   new List<ChargeType>()
            {
                new ChargeType(1,10000,10000),
                new ChargeType(2,20000,21000),
                new ChargeType(3,30000,32000),
                new ChargeType(4,50000,54000),
                new ChargeType(5,100000,110000),
            };
}

    /// <summary>
    /// 性别
    /// </summary>
   public enum Gender
    {
        /// <summary>
        /// 男
        /// </summary>
        Male=1,
        /// <summary>
        /// 女
        /// </summary>
        Female =0
    }
    /// <summary>
    /// 充值类型
    /// </summary>

    public enum ReChargeType
    {
        /// <summary>
        /// 充值卡
        /// </summary>
        Card=1,
        /// <summary>
        /// 微信
        /// </summary>
        WeChat =2
    }
    /// <summary>
    /// 订单状态
    /// </summary>
    public enum OrderState
    {
        /// <summary>
        /// 预定
        /// </summary>
        Predetermined=1,
        /// <summary>
        /// 已取货
        /// </summary>
        HadTake=2
    }
}
